using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BucketSort.OS;
using System.IO;

namespace BucketSort
{
    class Program
    {
        static void Main(string[] args)
        {
            BucketSortOS.Test_Array_List(1000, false);
            BucketSortOS.Test_Array_List(2000, false);
            BucketSortOS.Test_Array_List(4000, false);
            BucketSortOS.Test_Array_List(6000, false);
            BucketSortOS.Test_Array_List(8000, false);
            BucketSortOS.Test_Array_List(10000, false);

            BucketSortOS.Test_File_Array_List(100, false);
            BucketSortOS.Test_File_Array_List(200, false);
            BucketSortOS.Test_File_Array_List(400, false);
            BucketSortOS.Test_File_Array_List(600, false);
            BucketSortOS.Test_File_Array_List(800, false);
            BucketSortOS.Test_File_Array_List(1000, false);
        }
    }
}
