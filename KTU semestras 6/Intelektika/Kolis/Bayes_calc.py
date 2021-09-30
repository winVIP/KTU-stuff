import codecs


def unique(list1):
    unique_list = []
    for x in list1:
        add = True
        for y in unique_list:
            if y == x:
                add = False
        if add:
            unique_list.append(x)
    return unique_list


lines = []
columns = []
f = codecs.open("duomenys.txt", "r", "utf-8")
for x in f:
    lines.append(x.split(" "))
# for x in lines:
#   print(x)

po_kablelio = int(input("Kiek skaiciu po kablelio : "))
print("Pasirinkimai: ")
for index, x in enumerate(lines[0]):
    if x != "\r\n":
        print(index, " - ", x)
stulpelis_nr = int(input("Iveskite kokio stulpelio atsakymo ieskome (NUMERIS) (pvz. 3) : "))
stulpelis = []
for index, x in enumerate(lines):
    if (index != 0):
        stulpelis.append(x[stulpelis_nr])
#print(stulpelis)
print(" ")
unikalus = unique(stulpelis)
print("Reiksmes: ", unikalus)
C = []
PCi = []
for c in unikalus:
    pci = 0
    for x in stulpelis:
        if x == c:
            pci += 1
    C.append(pci)
    pci = pci / stulpelis.__len__()
    PCi.append(round(pci, po_kablelio))
print("C:        ", C)
print("P(Ci):    ", PCi)
print(" ")

pxci = []
num = 0
pasirinkimai = []
done = False

for c in range(C.__len__()):
    #print(c)
    #print(unikalus[c])
    pxi = []
    for i in range(lines[0].__len__() - 1):
        #print(i)
        num = i
        if i != 0:
            s = []
            u = []
            for index, x in enumerate(lines):
                if index != 0:
                    s.append(x[i])
            u = unique(s)
            if not done:
                if i != stulpelis_nr:
                    print(lines[0][i])
                    for index, ix in enumerate(u):
                        print(index, " - ", ix)
                    pasirinkimai.append(int(input("Pasirinkite reiksme kuri nurodyta jusu X: ")))
                if i == stulpelis_nr:
                    pasirinkimai.append(0)
            p = 0
            #print(s)
            for indx, x in enumerate(s):
                if x == u[pasirinkimai[i-1]] and stulpelis[indx] == unikalus[c]:
                    #print(stulpelis[indx])
                    #print(u[pasirinkimas])
                    #print(unikalus[c])
                    p += 1
            pxi.append(round(p / C[c], po_kablelio))
    done = True
    pxi.pop()
    print("P(Xi|Ci): ", pxi)
    sandauga = 1
    for p in pxi:
        sandauga *= p
    pxci.append(round(sandauga,po_kablelio))
print("P(X|Ci):  ", pxci)
print(" --- Atsakymas --- ")
pxcipi = []
for x in range(pxci.__len__()):
    print("Reiksme: ", unikalus[x])
    pxcipi.append(round(pxci[x]*PCi[x],po_kablelio))
    print("P(X|Ci) * P(Ci): ", pxcipi[x])

