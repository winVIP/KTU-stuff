using System;
using System.Diagnostics;

namespace Uzduotis2
{
    class Program
    {
        static void Main(string[] args)
        {
            //int[] X = { 3, -4, -1, -3, 5, 3, -1, 6, -4 };

            int size = 40;
            int[] X = new int[size];

            Random random = new Random();

            for(int i = 0; i < size; i++)
            {
                X[i] = random.Next(-10, 10);
            }

            foreach(int smt in X)
            {
                Console.Write(smt + " ");
            }
            Console.WriteLine();

            Stopwatch stopwatch = new Stopwatch();

            stopwatch.Restart();
            int[] result = solve(X);
            stopwatch.Stop();

            Console.WriteLine("Suradimas uztruko: " + stopwatch.ElapsedTicks);

            foreach (int item in result)
            {
                Console.Write("{0}; ", item);
            }
        }

        static public int[] solve(int[] a)
        {
            int[] solution = new int[a.Length + 1];
            solution[0] = 0;

            for (int j = 1; j < solution.Length; j++)
            {
                solution[j] = Math.Max(solution[j - 1] + a[j - 1], a[j - 1]);
            }

            int result = solution[0];
            int maxIndex = 0;
            for (int j = 1; j < solution.Length; j++)
            {
                if (result < solution[j])
                {
                    result = solution[j];
                    maxIndex = j;
                }               
            }

            int lastIndex = 0;
            int sum = 0;
            for(int i = maxIndex - 1; i >= 0; i--)
            {
                sum = sum + a[i];
                if(sum == result)
                {
                    lastIndex = i;
                }
            }

            int[] answer = new int[maxIndex - lastIndex];
            int k = 0;
            for (int i = lastIndex; i <= maxIndex - 1; i++)
            {
                answer[k++] = a[i];
            }

            return answer;
        }
    }
}
