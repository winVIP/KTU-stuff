using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Uzduotis3
{
    class Program
    {
        static Random random;
        static void Main(string[] args)
        {
            Stopwatch stopwatch = new Stopwatch();
            int n = 1000;
            int seed = (int)DateTime.Now.Ticks & 0x0000FFFF;
            Random random = new Random(seed);

            string[] valuesOP = new string[n];
            int[] keysOP = new int[n];

            for (int i = 0; i < n; i++)
            {
                valuesOP[i] = string.Format("{0}", i);
                keysOP[i] = i;
            }

            HashTableOP table = new HashTableOP();
            for (int j = 0; j < valuesOP.Length; j++)
            {
                table.Insert(keysOP[j], valuesOP[j]);
            }

            stopwatch.Restart();
            foreach (int key in keysOP)
            {
                table.GetValue(key);
            }
            stopwatch.Stop();

            Console.WriteLine("Visu nariu radimas: " + stopwatch.ElapsedTicks);


            stopwatch.Restart();
            Task[] tasks = new Task[4];
            for(int i = 0; i < n; i++)
            {
                if(i >= 0 || i < 250)
                {
                    tasks[0] = Task.Factory.StartNew(
                    (Object p) =>
                    {
                        var data = p as CustomData;
                        if (data == null) return;

                        table.GetValue(keysOP[data.TNum]);
                    },
                    new CustomData() { TNum = i });
                }
                if(i >= 250 || i < 500)
                {
                    tasks[1] = Task.Factory.StartNew(
                    (Object p) =>
                    {
                        var data = p as CustomData;
                        if (data == null) return;

                        table.GetValue(keysOP[data.TNum]);
                    },
                    new CustomData() { TNum = i });
                }
                if(i >= 500 || i < 750)
                {
                    tasks[2] = Task.Factory.StartNew(
                    (Object p) =>
                    {
                        var data = p as CustomData;
                        if (data == null) return;

                        table.GetValue(keysOP[data.TNum]);
                    },
                    new CustomData() { TNum = i });
                }
                if(i >= 750 || i < 1000)
                {
                    tasks[3] = Task.Factory.StartNew(
                    (Object p) =>
                    {
                        var data = p as CustomData;
                        if (data == null) return;

                        table.GetValue(keysOP[data.TNum]);
                    },
                    new CustomData() { TNum = i });
                }
            }
            Task.WaitAll(tasks);
            stopwatch.Stop();

            Console.WriteLine("(Parallel)Visu nariu radimas: " + stopwatch.ElapsedTicks);
        }

        class CustomData
        {
            public int TNum;
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
