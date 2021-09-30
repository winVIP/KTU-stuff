using System;
using System.Threading;
using System.IO;
using Newtonsoft.Json;


namespace Lab1a_LP
{
    class Program
    {
        static Automobiliai automobiliai;
        static Automobiliai rezultatai;
        static Automobilis[] autos;
        static void Main(string[] args)
        {
            autos = Skaitymas(3);

            automobiliai = new Automobiliai(autos.Length / 2);

            rezultatai = new Automobiliai(autos.Length);

            Thread[] threads = new Thread[autos.Length / 4];

            object kiekis = autos.Length / threads.Length;

            for (int i = 0; i < threads.Length; i++)
            {
                threads[i] = new Thread(new ParameterizedThreadStart(SkaiciavimasIrFiltravimas));
            }

            for (int i = 0; i < threads.Length; i++)
            {
                threads[i].Start(kiekis);
            }

            foreach (Automobilis auto in autos)
            {
                automobiliai.insert(auto);
            }
            

            for(int i = 0; i < threads.Length; i++)
            {
                threads[i].Join();
            }

            RasytiRezultatus("IFF-72_KunickasV_L1_dat_3.json");
        }

        static Automobilis[] Skaitymas(int kuris)
        {
            string json = File.ReadAllText(String.Format(@"C:\Users\vyten\Desktop\Lab1a LP\IFF-72_KunickasV_L1_dat_{0}.json", kuris));
            return JsonConvert.DeserializeObject<Automobilis[]>(json);
        }

        static void RasytiRezultatus(string komentaras)
        {
            string tekstas = null;

            tekstas = String.Format("{4}\nPradiniai duomenys\n{0,13}|{1,13:00.0}|{2,13}|{3,20}\n", "Marke", "Rida", "Metai", "Sandauga", komentaras);
            for(int i = 0; i < 65; i++)
            {
                tekstas = tekstas + "-";
            }
            tekstas = tekstas + "\n";
                
            for(int i = 0; i < autos.Length; i++)
            {
                tekstas = tekstas + String.Format("{0,13}|{1,13:00.0}|{2,13}|{3,23:00.0}\n", autos[i].getMarke(), autos[i].getRida(), autos[i].getMetai(), autos[i].getSandauga());
            }
            tekstas = tekstas + "\n";

            tekstas = tekstas + String.Format("Rezultatai\n{0,13}|{1,13:00.0}|{2,13}|{3,20}\n", "Marke", "Rida", "Metai", "Sandauga");
            for (int i = 0; i < 65; i++)
            {
                tekstas = tekstas + "-";
            }
            tekstas = tekstas + "\n";

            int currentMemberCount = rezultatai.getMemberCount();

            for (int i = 0; i < currentMemberCount; i++)
            {
                Automobilis automobilis = rezultatai.getAuto();
                tekstas = tekstas + String.Format("{0,13}|{1,13:00.0}|{2,13}|{3,23:00.0}\n", automobilis.getMarke(), automobilis.getRida(), automobilis.getMetai(), automobilis.getSandauga());
            }
            tekstas = tekstas + "\n";

            File.AppendAllText(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent + @"\IFF-72_KunickasV_L1_rez.txt", tekstas);
        }

        static void SkaiciavimasIrFiltravimas(object visiReziai)
        {
            int reziai = (int)visiReziai;

            for (int i = 0; i < reziai; i++)
            {
                Automobilis automobilis = automobiliai.getAuto();

                double sandauga = automobilis.getMarke().Length * automobilis.getRida() * automobilis.getMetai();

                automobilis.insertSandauga(sandauga);

                if (sandauga > 500000000)
                {
                    rezultatai.insert(automobilis);
                }
            }
        }
    }
}
