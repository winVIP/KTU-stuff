using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace DvigubasHesavimas
{
    class Program
    {
        static Random random;
        static void Main(string[] args)
        {
            int n = 3000;
            int initialCapacity = n * 5;
            float loadFactor = (float)0.25;
            List<string> nameList = new List<string>();
            HashTableDouble<string, string> hashTable = new HashTableDouble<string, string>(initialCapacity, loadFactor);
            int seed = (int)DateTime.Now.Ticks & 0x0000FFFF;
            random = new Random(seed);

            for (int i = 0; i < n; i++)
            {
                string s = RandomName(10);
                nameList.Add(s);
                hashTable.Put(s, s);
                //strTree.Insert(s);
            }
            Stopwatch myTimer = new Stopwatch();
            nameList.Add(RandomName(10));
            Console.WriteLine("  Hash Table \n");
            Console.WriteLine(hashTable);
            Console.WriteLine("\n Search Test \n");
            myTimer.Start();
            foreach (var s in nameList)
            {
                // Console.Write(strTree.Contains(s).ToString() + " ");
                Console.WriteLine(s + " " + hashTable.contains(s));
            }
            myTimer.Stop();
            Console.WriteLine(myTimer.ElapsedMilliseconds);
            Console.ReadKey();
        }
        static string RandomName(int size)
        {
            StringBuilder builder = new StringBuilder();
            char ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 *
            random.NextDouble() + 65)));
            builder.Append(ch);
            for (int i = 1; i < size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 *
                random.NextDouble() + 97)));
                builder.Append(ch);
            }
            return builder.ToString();
        }
    }
}
