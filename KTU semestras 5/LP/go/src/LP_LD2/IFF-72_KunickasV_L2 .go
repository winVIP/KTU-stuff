package main

import (
	"bufio"
	"encoding/json"
	"fmt"
	"io/ioutil"
	"log"
	"os"
	"sync"
	"time"
)

type Items struct {
	Items []Item `json:"items"`
}

type Item struct {
	Marke    string  `json:"marke"`
	Rida     float64 `json:"rida"`
	Metai    int     `json:"metai"`
	Sandauga float64
}

var pathData = "./IFF-72_KunickasV_L1_dat_3.json"
var pathRez = "./IFF-72_KunickasV_L1_rez3.txt"

func main() {
	var items = Skaitymas()
	SukurtiRezultatuFaila()
	RasytiPradiniusDuomenis(items.Items)
	var synchronizer = sync.WaitGroup{}

	var thread int
	thread = 4

	var chDat = make(chan Item)
	var chRes = make(chan Item)
	var chMain = make(chan Item)
	var chCal = make(chan Item)
	var chGet = make(chan bool)
	var chSend = make(chan bool)

	synchronizer.Add(2 + thread)
	go DuomenuPerdavimas(chDat, chSend, chCal, chGet, &synchronizer, len(items.Items)/2)

	for i := 0; i < thread; i++ {
		go Skaiciavimas(chCal, chRes, chSend, &synchronizer, i)
	}
	go RezultatuPerdavimas(chRes, chMain, chSend, &synchronizer, thread, len(items.Items))

	go func() { chGet <- true }()
	for i := 0; i < len(items.Items); {
		select {
		case <-chGet:
			chDat <- items.Items[i]
			i++
		default:
		}
	}
	select {
	case <-chGet:
		chDat <- Item{}
	default:
	}

	go RasytiRezultatus(chMain, &synchronizer)
	synchronizer.Wait()
}

func DuomenuPerdavimas(chDat <-chan Item, chSend <-chan bool, chCal chan<- Item, chGet chan bool, synchronizer *sync.WaitGroup, size int) {
	defer synchronizer.Done()

	var done = false
	privateItems := make([]Item, size)
	var i = 0

	for !done || i > 0 {
		time.Sleep(1 * time.Nanosecond)

		select {
		case x, ok := <-chDat:
			if !ok || x.Marke == "" {
				done = true
			} else {
				privateItems[i] = x
				i++
			}
		default:
		}

		if i < size && !done {
			select {
			case <-chGet:
			default:
				chGet <- true
			}
		}

		if i > 0 && privateItems[i-1].Marke != "" {
			select {
			case <-chSend:
				i--
				chCal <- privateItems[i]
			default:
			}
		}
	}
	close(chCal)
}

func Skaiciavimas(chCal <-chan Item, chRes chan<- Item, chSend chan bool, synchronizer *sync.WaitGroup, index int) {
	defer synchronizer.Done()

	if index == 0 {
		chSend <- true
	}
	for {
		select {
		case x, ok := <-chCal:
			if !ok || x.Marke == "" {
				chRes <- Item{}
				return
			}

			x.Sandauga = float64(len(x.Marke)) * float64(x.Metai) * x.Rida
			if x.Sandauga > 500000000 {
				chRes <- x
			}
			chSend <- true
		default:
		}
	}
}

func RezultatuPerdavimas(chRes <-chan Item, chMain chan<- Item, chSend chan bool, synchronizer *sync.WaitGroup, thread int, size int) {
	defer synchronizer.Done()
	privateItems := make([]Item, size)
	var i = 0

	var done bool
	done = false
	for !done {
		if !done {
			select {
			case x := <-chRes:
				if x.Marke == "" {
					thread--
					if thread == 1 {
						time.Sleep(10 * time.Nanosecond)
						done = true
					}
				} else {
					for j := 0; j < i; j++ {
						if x.Sandauga > privateItems[j].Sandauga {
							for k := i - 1; k >= j; k-- {
								privateItems[k+1] = privateItems[k]
							}
							privateItems[j] = x
							break
						} else if j == i-1 {
							privateItems[i] = x
						}
					}
					if i == 0 {
						privateItems[0] = x
					}
					i++
				}
			default:
			}
		}
	}
	i = 0
	for i < size && privateItems[i].Marke != "" {
		chMain <- privateItems[i]
		i++

	}
	close(chMain)
}

func Skaitymas() Items {

	jsonFile, err := os.Open(pathData)
	if err != nil {
		fmt.Println(err)
	}
	defer jsonFile.Close()
	byteValue, _ := ioutil.ReadAll(jsonFile)
	var items Items
	json.Unmarshal(byteValue, &items)
	return items
}

func RasytiRezultatus(channel chan Item, synchronizer *sync.WaitGroup) {
	defer synchronizer.Done()

	file, err := os.OpenFile(pathRez, os.O_APPEND|os.O_CREATE|os.O_WRONLY, 0644)

	if err != nil {
		log.Fatalf("failed creating file: %s", err)
	}

	datawriter := bufio.NewWriter(file)
	s := fmt.Sprintf("|   Marke   | Rida |  Metai  | Sandauga |\n----------------------------------------------\n")
	_, _ = datawriter.WriteString(s)
	var i int
	i = 0

	var done = false
	for !done {
		select {
		case x, ok := <-channel:
			if (!ok || x == Item{}) {
				done = true
				//close(channel)
				break
			}
			s := fmt.Sprintf("|%11s| %9.2f|%7d|%13.2f|\n", x.Marke, x.Rida, x.Metai, x.Sandauga)
			_, _ = datawriter.WriteString(s)
			i++
		default:
		}
	}

	s = fmt.Sprintf("%4d \n\n", i)
	_, _ = datawriter.WriteString(s)
	datawriter.Flush()
	file.Close()
}

func SukurtiRezultatuFaila() {
	if os.Remove(pathRez) != nil {
		fmt.Println("File is not deleted")
	}
}

func RasytiPradiniusDuomenis(items []Item) {

	file, err := os.OpenFile(pathRez, os.O_APPEND|os.O_CREATE|os.O_WRONLY, 0644)

	if err != nil {
		log.Fatalf("failed creating file: %s", err)
	}

	datawriter := bufio.NewWriter(file)
	s := fmt.Sprintf("|   Marke   | Rida |  Metai  | \n---------------------------------\n")
	_, _ = datawriter.WriteString(s)
	var ii int
	for i := 0; i < len(items); i++ {
		if (items[i] == Item{}) {
			break
		}
		s := fmt.Sprintf("|%11s| %9.2f|%7d|\n", items[i].Marke, items[i].Rida, items[i].Metai)
		_, _ = datawriter.WriteString(s)
		ii++
	}
	s = fmt.Sprintf("%4d \n\n", ii)
	_, _ = datawriter.WriteString(s)
	datawriter.Flush()
	file.Close()
}
