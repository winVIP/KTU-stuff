using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MergeSort
{
    abstract class DataList
    {
        protected int length;
        protected string filename;
        protected double current;
        public int Length { get { return length; } }
        public string Filename { get { return filename; } }
        public double GetCurrent { get { return current; } }
        public abstract void Add(double data);
        public abstract double? NextInArray();
        public abstract int Find(int i, double j);
        public abstract double Head();
        public abstract double Next();
        public abstract void NextAndCurrent();
        public abstract double Current();
        public abstract void Swap(double a, double b);
        public void Print()
        {
            Console.Write(" {0:F5} ", Head());
            for (int i = 1; i < length; i++)
                Console.Write(" {0:F5} ", Next());
            Console.WriteLine();
        }
        public void PrintArray()
        {
            Console.Write(" {0:F5} ", Head());
            for (int i = 1; i < length; i++)
                Console.Write(" {0:F5} ", NextInArray());
            Console.WriteLine();
        }
    }
}
