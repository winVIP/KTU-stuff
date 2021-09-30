using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace HashTable
{
    class Program
    {
        static Random random;
        static void Main(string[] args)
        {
            Testas(200);
            Testas(400);
            Testas(600);
            Testas(800);
            Testas(1000);
        }

        static void Testas(int n)
        { 
            Console.WriteLine("Nariu kiekis: " + n);

            int seed = (int)DateTime.Now.Ticks & 0x0000FFFF;
            random = new Random(seed);

            string[] valuesOP = new string[n];
            int[] keysOP = new int[n];

            int[] valuesD = new int[n];
            string[] keysD = new string[n];

            for (int i = 0; i < n; i++)
            {
                valuesOP[i] = string.Format("{0}", i);
                keysOP[i] = i;

                keysD[i] = RandomName(10);
                valuesD[i] = i;
            }

            HashTableOPTest(valuesOP, keysOP, true, 1);
            Console.WriteLine("-------------------------------------------------------------------------------------------------------------------");
            Console.WriteLine("-------------------------------------------------------------------------------------------------------------------");
            HashTableDTest(valuesD, keysD, false, 1);
        }

        static void HashTableOPTest(string[] values, int[] keys, bool show, int count)
        {
            Stopwatch stopwatch = new Stopwatch();
            List<long> creationTime = new List<long>();
            List<long> searchingTime = new List<long>();
            for(int i = 0; i < count; i++)
            {
                Console.WriteLine("HashTableOP Bandymas" + (i + 1));
                Console.WriteLine();

                stopwatch.Start();
                HashTableOP table = new HashTableOP();
                for (int j = 0; j < values.Length; j++)
                {
                    table.Insert(keys[j], values[j]);
                }
                stopwatch.Stop();
                creationTime.Add(stopwatch.ElapsedTicks);
                Console.WriteLine("Sukurimas ir duomenu sudejimas ticks: " + stopwatch.ElapsedTicks);

                Console.WriteLine();
                stopwatch.Reset();


                if (show)
                {
                    stopwatch.Start();
                    foreach (int key in keys)
                    {
                        Console.Write(table.GetValue(key) + " ");
                    }
                    stopwatch.Stop();
                    Console.WriteLine();
                    Console.WriteLine();
                    searchingTime.Add(stopwatch.ElapsedTicks);
                    Console.WriteLine("Visu elementu radimas ir atspausdinimas ticks: " + stopwatch.ElapsedTicks);
                    stopwatch.Reset();
                }
                else
                {
                    stopwatch.Start();
                    foreach (int key in keys)
                    {
                        table.GetValue(key);
                    }
                    stopwatch.Stop();
                    Console.WriteLine();
                    Console.WriteLine();
                    searchingTime.Add(stopwatch.ElapsedTicks);
                    Console.WriteLine("Visu elementu radimas ticks: " + stopwatch.ElapsedTicks);
                    stopwatch.Reset();
                }
                Console.WriteLine("-------------------------------------------------------------------------------------------------------------------");
            }

            Console.WriteLine();

            double sumCr = 0;
            double sumSr = 0;

            foreach(long cr in creationTime)
            {
                sumCr += cr;
            }

            foreach (long sr in searchingTime)
            {
                sumSr += sr;
            }

            Console.WriteLine("Sukurimo laiko vidurkis: " + sumCr / count / 10000000 + " s");
            Console.WriteLine("Paieskos laiko vidurkis: " + sumSr / count / 10000000 + " s");

            Console.WriteLine();
        }

        static void HashTableDTest(int[] values, string[] keys, bool show, int count)
        {
            Stopwatch stopwatch = new Stopwatch();
            List<long> creationTime = new List<long>();
            List<long> searchingTime = new List<long>();
            for (int i = 0; i < count; i++)
            {
                Console.WriteLine("HashTableD Bandymas" + (i + 1));
                Console.WriteLine();

                stopwatch.Start();
                HashTableD table = new HashTableD();
                for (int j = 0; j < values.Length; j++)
                {
                    table.Insert(keys[j], values[j]);
                }
                stopwatch.Stop();
                creationTime.Add(stopwatch.ElapsedTicks);
                Console.WriteLine("Sukurimas ir duomenu sudejimas ticks: " + stopwatch.ElapsedTicks);

                Console.WriteLine();
                stopwatch.Reset();

                if (show)
                {
                    stopwatch.Start();
                    foreach (string key in keys)
                    {
                        Console.Write(table.GetValue(key) + " ");
                    }
                    stopwatch.Stop();
                    Console.WriteLine();
                    Console.WriteLine();
                    searchingTime.Add(stopwatch.ElapsedTicks);
                    Console.WriteLine("Visu elementu radimas ir atspausdinimas ticks: " + stopwatch.ElapsedTicks);
                    stopwatch.Reset();
                }
                else
                {
                    stopwatch.Start();
                    foreach (string key in keys)
                    {
                        table.GetValue(key);
                    }
                    stopwatch.Stop();
                    Console.WriteLine();
                    Console.WriteLine();
                    searchingTime.Add(stopwatch.ElapsedTicks);
                    Console.WriteLine("Visu elementu radimas ticks: " + stopwatch.ElapsedTicks);
                    stopwatch.Reset();
                }
                Console.WriteLine("-------------------------------------------------------------------------------------------------------------------");
            }

            Console.WriteLine();

            double sumCr = 0;
            double sumSr = 0;

            foreach (long cr in creationTime)
            {
                sumCr += cr;
            }

            foreach (long sr in searchingTime)
            {
                sumSr += sr;
            }

            Console.WriteLine("Sukurimo laiko vidurkis: " + sumCr / count / 10000000 + " s");
            Console.WriteLine("Paieskos laiko vidurkis: " + sumSr / count / 10000000 + " s");

            Console.WriteLine();
        }

        static string RandomName(int size)
        {
            StringBuilder builder = new StringBuilder();
            char ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
            builder.Append(ch);
            for (int i = 1; i < size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 97)));
                builder.Append(ch);
            }
            return builder.ToString();
        }
    }
}
