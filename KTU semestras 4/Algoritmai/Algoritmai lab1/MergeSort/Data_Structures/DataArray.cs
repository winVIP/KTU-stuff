using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MergeSort
{
    abstract class DataArray
    {
        protected int length;
        public int Length
        {
            get
            {
                return length;
            }
        }
        public abstract double this[int index] { get; }
        public abstract void Swap(int j, double a, double b);
        public abstract void Put(string filename, int i, double a);
        public abstract void Put(int j, double a);
        public void Print(int n)
        {
            for (int i = 0; i < n; i++)
                Console.Write(" {0:F5} ", this[i]);
            Console.WriteLine();
        }
    }
}
