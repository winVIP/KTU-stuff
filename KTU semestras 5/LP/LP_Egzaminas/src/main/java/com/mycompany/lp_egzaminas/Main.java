package com.mycompany.lp_egzaminas;

import akka.actor.AbstractActor;
import akka.actor.ActorSystem;
import akka.actor.Props;
import akka.actor.ActorRef;
import akka.japi.pf.ReceiveBuilder;
import akka.routing.RoundRobinPool;
import java.io.BufferedWriter;
import java.io.File;

import java.io.FileNotFoundException;
import java.io.FileReader;
import java.io.FileWriter;
import java.io.IOException;
import java.nio.file.Paths;
 
import org.json.simple.JSONArray;
import org.json.simple.JSONObject;
import org.json.simple.parser.JSONParser;
import org.json.simple.parser.ParseException;

public class Main {
    static class Rezultatu extends AbstractActor {
        static class Duomenys {
            //Masinos duomenys
            private final Car car;
            //Masinų kiekis nuskaitytas iš failo
            private final int length;
            //Pagrindinio aktoriaus adresas
            private final ActorRef pagrindinis;
            public Duomenys(Car car, int length, ActorRef pagrindinis){
                this.car = car;
                this.length = length;
                this.pagrindinis = pagrindinis;
            }
        }
        
        //Reikšmė, kuri skaičiuoja kiek žinučių buvo atsiųsta
        int receiveCount;
        File file;
        
        {
            receiveCount = 0;
            //Nustato, kas bus daroma, kai darbininkas aktorius atsius duomenis
            receive(ReceiveBuilder.match(Duomenys.class, this::onDuomenys).build());
            
            //Rezultatų failas paruošiamas, suformuojami stulpelių pavadinimai
            file = new File(Paths.get(".").toAbsolutePath().normalize().getParent().getParent().getParent().getParent().toString() + "\\IFF-72_KunickasV_E_rez.txt");
            //Jei rezultatų failas jau egzistuoja, tai jis yra ištrinamas ir sukuriamas naujas
            if(file.exists()){
                file.delete();
                try {
                    file.createNewFile();
                    FileWriter fw = new FileWriter(file, true);
                    BufferedWriter bw = new BufferedWriter(fw);
                    bw.write(String.format("%14s|%10s|%6s\n", "Marke", "Rida", "Metai"));
                    bw.close();
                } catch (Exception e) {
                }
                
            }
            //Jei failo nėra jis yra sukuriamas
            else{
                try {
                    file.createNewFile();
                    FileWriter fw = new FileWriter(file, true);
                    BufferedWriter bw = new BufferedWriter(fw);
                    bw.write(String.format("%14s|%10s|%6s\n", "Marke", "Rida", "Metai"));
                    bw.close();
                } catch (Exception e) {
                }
            }
            
        }
        
        /**
         * Įrašinėja filtruotus rezultatus į failą
         * @param duomenys iš darbininko gauti duomenys
         * @throws IOException 
         */
        public void onDuomenys(Duomenys duomenys) throws IOException{
            //Jei atsiunčiama null reikšmė, niekas nėra rašoma į failą, tik padidinamas atsiųstų žinučių kiekis
            if(duomenys.car == null){
                receiveCount++;
            }
            //Jei atsiunčiami mašinos duomenys, tai jie yra įrašomi į failą
            else{
                FileWriter fw = new FileWriter(file, true);
                BufferedWriter bw = new BufferedWriter(fw);
                bw.write(String.format(duomenys.car.toString() + "\n"));
                bw.close();
                receiveCount++;
            }
            //Tikrinama, ar atsiųstų žinučių kiekis yra lygus iš failų paimtų mašinų kiekiui,
            //jei taip, tai siunčiamas signalas pagrindiniam aktoriui, kad sistema būtų sustabdyta,
            //kadangi nebus daugiau, ka filtruoti, ar įrašinėti
            if(receiveCount == duomenys.length){
                duomenys.pagrindinis.tell(new Pagrindinis.BaigeIrasinet(), self());
            }
        }
        
        /**
         * Grąžina aktoriaus parametrai
         * @return Aktoriaus parametrai
         */
        public static Props props() {
            return Props.create(Rezultatu.class);
        }
    }
    
    static class Darbininkas extends AbstractActor {
        static class Duomenys {
            //Masinos duomenys
            private final Car car;
            //Masinų kiekis nuskaitytas iš failo
            private final int length;
            //Iš ko buvo gauta žinutė
            private final ActorRef from;
            //Rezultatų aktoriaus adresas
            private final ActorRef to;
            public Duomenys(Car car, int length, ActorRef from, ActorRef to){
                this.car = car;
                this.length = length;
                this.from = from;
                this.to = to;
            }
        }
        
        {
            //Nustato, kas bus daroma, kai pagrindinis aktorius atsius duomenis
            receive(ReceiveBuilder.match(Duomenys.class, this::onDuomenys).build());
        }
        
        /**
         * Suskaičiuoja sandauga pagal gautus mašinos duomenis ir pagal gautą suma juos filtruoja.
         * Atitinkantys duomenys yra siunčiami rezultatų aktoriui, neatitinkantys yra pakeičiami null reikšme.
         * Taip pat yra siunčiamas duomenų kiekis iš failo ir pagrindinio aktoriaus adresas.
         * @param duomenys 
         */
        public void onDuomenys(Duomenys duomenys){
            //Skaičiuojama sandauga pagal duomenis
            double number = (double)duomenys.car.getMarke().length() * (double)duomenys.car.getRida() * (double)duomenys.car.getMetai();
            //Filtruojama pagal sandaugą
            if(number > 500000000){
                //Tinkama mašina siunčiama rezultatų aktoriui su mašinų kiekiu ir pagrindinio aktoriaus adresu
                duomenys.to.tell(new Rezultatu.Duomenys(duomenys.car, duomenys.length, duomenys.from), self());
            }
            else {
                //Netinkama mašina yra pakeičiama null reikšme ir siunčiama rezultatų aktoriui su mašinų kiekiu ir pagrindinio aktoriaus adresu
                duomenys.to.tell(new Rezultatu.Duomenys(null, duomenys.length, duomenys.from), self());
            }
        }
        
        /**
         * Grąžina aktoriaus parametrai
         * @return Aktoriaus parametrai
         */
        public static Props props() {
            return Props.create(Darbininkas.class);
        }
    }
    
    /**
     * Pagrindinio aktoriaus klasė
     */
    static class Pagrindinis extends AbstractActor {
        static class BaigeIrasinet {}
        
        static class Duomenys {
            //Masinos duomenys
            private final Car car;
            //Masinų kiekis nuskaitytas iš failo
            private final int length;
            public Duomenys(Car car, int length){
                this.car = car;
                this.length = length;
            }
        }
        
        ActorRef router;
        ActorRef rezultatu;
        
        {           
            //Sukuriamas RoundRobinPool darbininkų aktoriams, tam, kad darbas būtų geriau pasidalintas
            router = getContext().actorOf(new RoundRobinPool(4).props(Darbininkas.props()), "router");
            //Sukuriamas rezultatų aktorius, kuris surašo išfiltruotus rezultatus į failą
            rezultatu = getContext().actorOf(Rezultatu.props(), "rezultatu");
            
            //Nustatoma nuo žinučių tipo naudojami metodai
            receive(ReceiveBuilder
                    //Žinutes gaunamos iš main
                    .match(Duomenys.class, this::onDuomenys)
                    //Žinutė gaunama iš rezultatų aktoriaus, kai visi rezultatai jau yra surašyti
                    .match(BaigeIrasinet.class, this::onBaigeIrasinet)
                    .build());
        }

        /**
         * Perduoda mašinų duomenis aktoriams darbininkams
         * @param duomenys mašinos duomenys, mašinų kiekis
         */
        public void onDuomenys(Duomenys duomenys){
            router.tell(new Darbininkas.Duomenys(duomenys.car, duomenys.length, self(), rezultatu), self());
            //System.out.println("Gavo");
        }
        
        /**
         * Išjungia aktorių sistemą
         * @param baigeIrasinet tuščia klasė
         */
        public void onBaigeIrasinet(BaigeIrasinet baigeIrasinet){
            System.err.println("Baige irasinet");
            getContext().system().shutdown();
            
        }
        
        /**
         * Grąžina aktoriaus parametrai
         * @return Aktoriaus parametrai
         */
        public static Props props() {
            return Props.create(Pagrindinis.class);
        }
    }
    
    
    
    /**
     * @param args the command line arguments
     */
    
    public static void main(String[] args) {
        //Nuskaitomi mašinų duomenys iš failo į šį masyvą
        Car[] cars = readAllCars();
        
        // Sukuriama aktorių sistema
        ActorSystem system = ActorSystem.create("Filtravimas");
        // Sukuriamas pagrindinis aktorius
        final ActorRef pagrindinis = system.actorOf(Pagrindinis.props(), "Pagrindinis");
        
        //Pagrindiniam aktoriui siunčiamos žinutes su mašinos duomenimis ir masyve esančių mašinų kiekiu
        for (Car car : cars) {
            pagrindinis.tell(new Pagrindinis.Duomenys(car, cars.length), ActorRef.noSender());
        }
    }
    
    /**
     * Nuskaito iš failo duomenis į masyvą
     * @return masyvas su mašinų duomenimis
     */
    private static Car[] readAllCars(){
        JSONParser jsonParser = new JSONParser();
        
        Car[] cars = new Car[28];
        
        File file = new File(Paths.get(".").toAbsolutePath().normalize().getParent().getParent().getParent().getParent().toString() + "\\IFF-72_KunickasV_E_dat_3.json");
        
        try (FileReader reader = new FileReader(file))
        {
            //Read JSON file
            Object obj = jsonParser.parse(reader);
 
            JSONArray employeeList = (JSONArray) obj;
            //System.out.println(employeeList);
             
            for (int i = 0; i < 28; i++) {
                JSONObject tmp = (JSONObject) employeeList.get(i);
                String marke = (String) tmp.get("marke"); 
                double rida = (double) tmp.get("rida");  
                long tmpMetai = (long) tmp.get("metai"); 
                int metai = Math.toIntExact(tmpMetai);
                cars[i] = new Car(marke, rida, metai);
            }
 
        } catch (FileNotFoundException e) {
            e.printStackTrace();
        } catch (IOException e) {
            e.printStackTrace();
        } catch (ParseException e) {
            e.printStackTrace();
        }
        
        return cars;
    } 
}
