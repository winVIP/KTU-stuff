using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BucketSort.OS;
using System.IO;
using System.Diagnostics;

namespace BucketSort
{
    class BucketSortOS
    {
        public static void Test_Array_List(int n, bool show)
        {
            int seed = (int)DateTime.Now.Ticks & 0x0000FFFF;

            Console.WriteLine("Duomenu kiekis: " + n);
            Stopwatch stopwatch = new Stopwatch();
            double executionTime = 0;

            MyDataArray myarray = new MyDataArray(n, seed);
            Console.WriteLine("\n ARRAY \n");
            if (show)
            {
                myarray.Print(n);
            }

            stopwatch.Start();
            BucketSortArray(myarray);
            stopwatch.Stop();
            executionTime += stopwatch.ElapsedTicks;
            if (show)
            {
                Console.WriteLine("\n SORTED ARRAY \n");
                myarray.Print(n);
            }

            Console.WriteLine("Rusiavimas uztruko: " + executionTime / 10000000 + "s");

            stopwatch.Reset();


            executionTime = 0;
            MyDataList mylist = new MyDataList(n, seed);
            Console.WriteLine("\n LIST \n");
            if (show)
            {
                mylist.Print(n);
            }

            stopwatch.Start();
            BucketSortList(mylist);
            stopwatch.Stop();
            executionTime += stopwatch.ElapsedTicks;
            if (show)
            {
                Console.WriteLine("\n SORTED LIST \n");
                mylist.Print(n);
            }

            Console.WriteLine("Rusiavimas uztruko: " + executionTime / 10000000 + "s");
            Console.WriteLine();
        }

        public static void Test_File_Array_List(int n, bool show)
        {
            int seed = (int)DateTime.Now.Ticks & 0x0000FFFF;

            Console.WriteLine("Duomenu kiekis: " + n);
            Stopwatch stopwatch = new Stopwatch();
            double executionTime = 0;

            string filename;
            filename = @"mydataarray.dat";
            MyFileArray myfilearray = new MyFileArray(filename, n, seed);
            using (myfilearray.fs = new FileStream(filename, FileMode.Open, FileAccess.ReadWrite))
            {
                Console.WriteLine("\n FILE ARRAY \n");
                if (show)
                {
                    myfilearray.Print(n);
                }

                stopwatch.Start();
                BucketSortArrayFile(myfilearray);
                stopwatch.Stop();
                executionTime += stopwatch.ElapsedTicks;
                if (show)
                {
                    Console.WriteLine("\n SORTED FILE ARRAY \n");
                    myfilearray.Print(n);
                }

            }
            Console.WriteLine("Rusiavimas uztruko: " + executionTime / 10000000 + "s");
            stopwatch.Reset();


            executionTime = 0;
            filename = @"mydatalist.dat";
            MyFileList myfilelist = new MyFileList(filename, n, seed);
            using (myfilelist.fs = new FileStream(filename, FileMode.Open, FileAccess.ReadWrite))
            {
                Console.WriteLine("\n FILE LIST \n");
                if (show)
                {
                    myfilelist.Print(n);
                }


                stopwatch.Start();
                BucketSortListFile(myfilelist);
                stopwatch.Stop();
                executionTime += stopwatch.ElapsedTicks;
                if (show)
                {
                    Console.WriteLine("\n SORTED FILE LIST \n");
                    myfilelist.Print(n);
                }

                Console.WriteLine("Rusiavimas uztruko: " + executionTime / 10000000 + "s");
                Console.WriteLine();
            }
        }

        public static DataArray BucketSortArray(DataArray items)
        {
            List<List<double>> buckets = new List<List<double>>();
            for (int i = 0; i < 10; i++)
            {
                buckets.Add(new List<double>());
            }

            for (int i = 0; i < items.Length; i++)
            {
                double val = items[i] * 10;
                int bucketNumber = (int)Math.Floor(val);
                buckets[bucketNumber].Add(items[i]);
            }

            int j = 0;
            foreach (List<double> bucket in buckets)
            {
                DataArray array = new MyDataArray(bucket.Count());
                for(int i = 0; i < bucket.Count; i++)
                {
                    array[i] = bucket.ElementAt(i);
                }

                BubbleSort(array);

                for (int i = 0; i < bucket.Count; i++)
                {
                    items[j++] = array[i];
                }
            }

            return items;
        }

        public static DataArray BucketSortArrayFile(DataArray items)
        {
            List<List<double>> buckets = new List<List<double>>();
            for (int i = 0; i < 10; i++)
            {
                buckets.Add(new List<double>());
            }

            for (int i = 0; i < items.Length; i++)
            {
                double val = items[i] * 10;
                int bucketNumber = (int)Math.Floor(val);
                buckets[bucketNumber].Add(items[i]);
            }

            int j = 0;
            int fileCount = 0;
            foreach (List<double> bucket in buckets)
            {
                if(bucket.Count == 0)
                {
                    continue;
                }
                fileCount++;
                string fileName = string.Format("@{0}.dat", fileCount);
                MyFileArray array = new MyFileArray(fileName, bucket.ToArray());
                array.fs = new FileStream(fileName, FileMode.Open, FileAccess.ReadWrite);

                BubbleSort(array);

                for (int i = 0; i < bucket.Count; i++)
                {
                    items[j++] = array[i];
                }
                array.fs.Close();
            }

            string filesToDelete = @"@*.dat";
            string[] fileList = Directory.GetFiles(Directory.GetCurrentDirectory(), filesToDelete);
            foreach (string file in fileList)
            {
                File.Delete(file);
            }

            return items;
        }

        public static DataList BucketSortList(DataList items)
        {
            List<List<double>> buckets = new List<List<double>>();
            for (int i = 0; i < 10; i++)
            {
                buckets.Add(new List<double>());
            }

            double currentData = items.Head();
            double val = currentData * 10;
            int bucketNumber = (int)Math.Floor(val);
            buckets[bucketNumber].Add(currentData);
            for (int i = 1; i < items.Length; i++)
            {
                currentData = items.Next();
                val = currentData * 10;
                bucketNumber = (int)Math.Floor(val);
                buckets[bucketNumber].Add(currentData);
            }

            int emptyCount = 0;
            for (int i = buckets.Count - 1; i >= 0; i--)
            {
                if(buckets.ElementAt(i).Count == 0)
                {
                    emptyCount++;
                }
                else
                {
                    break;
                }
            }

            items.Head();
            int replaces = 0;
            for (int i = 0; i < buckets.Count; i++)
            {
                if (buckets.ElementAt(i).Count == 0)
                {
                    continue;
                }
                DataList list = new MyDataList(buckets.ElementAt(i).ToArray());
                BubbleSort(list);

                items.Replace(list.Head());
                replaces++;
                if (replaces < items.Length)
                {
                    items.Next();
                }
                if (list.Length > 1)
                {
                    
                    for (int j = 1; j < list.Length; j++)
                    {
                        items.Replace(list.Next());
                        replaces++;
                        if (replaces >= items.Length)
                        {
                            break;
                        }
                        items.Next();
                    }
                }
            }
            return items;
        }

        public static DataList BucketSortListFile(DataList items)
        {
            List<List<double>> buckets = new List<List<double>>();
            for (int i = 0; i < 10; i++)
            {
                buckets.Add(new List<double>());
            }

            double currentData = items.Head();
            double val = currentData * 10;
            int bucketNumber = (int)Math.Floor(val);
            buckets[bucketNumber].Add(currentData);
            for (int i = 1; i < items.Length; i++)
            {
                currentData = items.Next();
                val = currentData * 10;
                bucketNumber = (int)Math.Floor(val);
                buckets[bucketNumber].Add(currentData);
            }

            int emptyCount = 0;
            for (int i = buckets.Count - 1; i >= 0; i--)
            {
                if (buckets.ElementAt(i).Count == 0)
                {
                    emptyCount++;
                }
                else
                {
                    break;
                }
            }

            int fileCount = 0;

            items.Head();
            int replaces = 0;
            for (int i = 0; i < buckets.Count; i++)
            {
                fileCount++;
                if (buckets.ElementAt(i).Count == 0)
                {
                    continue;
                }
                string fileName = string.Format("@{0}.dat", fileCount);
                MyFileList list = new MyFileList(fileName, buckets.ElementAt(i).ToArray());
                list.fs = new FileStream(fileName, FileMode.Open, FileAccess.ReadWrite);
                BubbleSort(list);

                items.Replace(list.Head());
                replaces++;
                if (replaces < items.Length)
                {
                    items.Next();
                }
                if (list.Length > 1)
                {

                    for (int j = 1; j < list.Length; j++)
                    {
                        items.Replace(list.Next());
                        replaces++;
                        if (replaces >= items.Length)
                        {
                            break;
                        }
                        items.Next();
                    }
                }
                list.fs.Close();
            }

            string filesToDelete = @"@*.dat";
            string[] fileList = Directory.GetFiles(Directory.GetCurrentDirectory(), filesToDelete);
            foreach (string file in fileList)
            {
                File.Delete(file);
            }

            return items;
        }

        public static void BubbleSort(DataArray items)
        {
            double prevdata, currentdata;
            for (int i = items.Length - 1; i >= 0; i--)
            {
                currentdata = items[0];
                for (int j = 1; j <= i; j++)
                {
                    prevdata = currentdata;
                    currentdata = items[j];
                    if (prevdata > currentdata)
                    {
                        items.Swap(j, currentdata, prevdata);
                        currentdata = prevdata;
                    }
                }
            }
        }

        public static void BubbleSort(DataList items)
        {
            double prevdata, currentdata;
            for (int i = items.Length - 1; i >= 0; i--)
            {
                currentdata = items.Head();
                for (int j = 1; j <= i; j++)
                {
                    prevdata = currentdata;
                    currentdata = items.Next();
                    if (prevdata > currentdata)
                    {
                        items.Swap(currentdata, prevdata);
                        currentdata = prevdata;
                    }
                }
            }
        }
    }
}
