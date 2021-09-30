using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BucketSort.OS
{
    abstract class DataList
    {
        protected int length;
        public int Length { get { return length; } }
        public abstract double Head();
        public abstract double Next();
        public abstract void Replace(double item);
        public abstract void NextNoReturn();
        public abstract void Swap(double a, double b);
        public void Print(int n)
        {
            Console.Write(" {0:F5} ", Head());
            for (int i = 1; i < n; i++)
                Console.Write(" {0:F5} ", Next());
            Console.WriteLine();

        }
    }
}
