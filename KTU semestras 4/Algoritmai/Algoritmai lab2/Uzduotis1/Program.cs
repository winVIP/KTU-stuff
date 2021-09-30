using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Uzduotis1
{
    class Program
    {
        static int[] arr;
        static Stopwatch stopWatch = new Stopwatch();
        static void Main(string[] args)
        {
            arr = new int[] { 40, 20, 30, 10, 30 };
            int n = arr.Length;

            int recursive;
            int dynamic;
            int parallel;

            stopWatch.Restart();
            recursive = F1(1, n - 1);
            stopWatch.Stop();
            Console.WriteLine("Recursive uztruko: " + stopWatch.ElapsedTicks);

            stopWatch.Restart();
            dynamic = F2(n);
            stopWatch.Stop();
            Console.WriteLine("Dynamic uztruko: " + stopWatch.ElapsedTicks);

            stopWatch.Restart();
            parallel = F3(n);
            stopWatch.Stop();
            Console.WriteLine("Parallel uztruko: " + stopWatch.ElapsedTicks);

            Console.WriteLine("(Recursive)Minimum number of multiplications is " + recursive);
            Console.WriteLine("(Dynamic)Minimum number of multiplications is " + dynamic);
            Console.WriteLine("(Parallel)Minimum number of multiplications is " + parallel);
        }

        static int F1(int i, int j)
        {
            if (i == j)
                return 0;

            int min = int.MaxValue;

            for (int k = i; k < j; k++)
            {
                int count = F1(i, k) + F1(k + 1, j) + arr[i - 1] * arr[k] * arr[j];

                if (count < min)
                    min = count;
            }

            return min;
        }

        static int F2(int n)
        {
            int[,] m = new int[n, n];

            int i, j, k, L, q;

            for (i = 1; i < n; i++)
                m[i, i] = 0;

            for (L = 2; L < n; L++)
            {
                for (i = 1; i < n - L + 1; i++)
                {
                    j = i + L - 1;
                    if (j == n) continue;
                    m[i, j] = int.MaxValue;
                    for (k = i; k <= j - 1; k++)
                    {
                        q = m[i, k] + m[k + 1, j] + arr[i - 1] * arr[k] * arr[j];

                        if (q < m[i, j])
                            m[i, j] = q;
                    }
                }
            }

            return m[1, n - 1];
        }

        class CustomData
        {
            public int TResult;
            public int TNum;
        }

        static int F3(int n)
        {
            Task[] tasks = new Task[n - 2];

            for (var k = 1; k < n - 1; k++)
            {
                tasks[k - 1] = Task.Factory.StartNew(
                    (Object p) =>
                    {
                        var data = p as CustomData;
                        if (data == null) return;

                        data.TResult = F1(1, data.TNum) + F1(data.TNum + 1, n - 1) + arr[0] * arr[data.TNum] * arr[n - 1];

                        //Console.WriteLine("Thread: " + Thread.CurrentThread.ManagedThreadId);
                    },
                    new CustomData() { TNum = k} );
            }                

            Task.WaitAll(tasks);

            int rezult = int.MaxValue;

            foreach(Task task in tasks)
            {
                if((task.AsyncState as CustomData).TResult < rezult)
                {
                    rezult = (task.AsyncState as CustomData).TResult;
                }
                
            }

            return rezult;
        }
    }
}
