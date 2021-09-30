using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MergeSort
{
    class Program
    {
        static void Main(string[] args)
        {
            MergeSort.SortTestOP(2000, false);
            MergeSort.SortTestOP(4000, false);
            MergeSort.SortTestOP(6000, false);
            MergeSort.SortTestOP(8000, false);
            MergeSort.SortTestOP(10000, false);

            MergeSort.SortTestD(100, false);
            MergeSort.SortTestD(200, false);
            MergeSort.SortTestD(400, false);
            MergeSort.SortTestD(600, false);
            MergeSort.SortTestD(800, false);
            MergeSort.SortTestD(1000, false);
        }
    }
}
