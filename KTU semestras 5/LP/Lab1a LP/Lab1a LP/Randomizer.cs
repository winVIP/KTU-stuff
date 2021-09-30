using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Newtonsoft.Json;

namespace Lab1a_LP
{
    class Randomizer
    {
        static int index = 0;
        static double[] ridos = new double[28];
        static int[] metai2 = new int[28];
        static string[] markes;

        public void start()
        {
            Rand();

            Car[] cars = new Car[28];

            for (int i = 0; i < 28; i++)
            {
                cars[i] = new Car { marke = markes[i], metai = metai2[i], rida = ridos[i] };
            }
            string json = JsonConvert.SerializeObject(cars, Formatting.Indented);

            File.WriteAllText(@"C:\Users\vyten\Desktop\Lab1a LP\IFF-72_KunickasV_L1_dat_2.json", json);
        }

        public void Rand()
        {
            if (index >= 28)
            {
                return;
            }
            Random random = new Random();

            markes = new string[]{ "Bmw", "Mercedes-benz", "Audi", "Porsche", "Opel", "Volkswagen",
                                             "Ferrari", "Fiat", "Lamborghini", "Bugatti", "Peugeot", "Renault",
                                             "Citroen", "Volvo", "Koenigsegg", "Aston-Martin", "Mini", "Land Rover",
                                             "Range Rover", "Mazda", "Toyota", "Subaru", "Mitsubishi", "Suzuki",
                                             "Nissan", "Ford", "Chevrolet", "Dodge" };
            
            while(index < 28)
            {
                int metai = random.Next(1970, 2019);
                double rida = random.NextDouble() * 500000;
                double skaicius = rida * metai * markes[index].Length;
                Console.WriteLine("Sukasi");
                if(index < 14)
                {
                    if (skaicius < 500000000)
                    {
                        Console.WriteLine(index);
                        ridos[index] = rida;
                        metai2[index] = metai;
                        index++;
                        Rand();
                    }
                }
                else
                {
                    if (skaicius > 500000000)
                    {
                        Console.WriteLine(index);
                        ridos[index] = rida;
                        metai2[index] = metai;
                        index++;
                        Rand();
                    }
                }
            }
        }
    }

    class Car
    {
        public string marke;
        public double rida;
        public int metai;
    }
}
