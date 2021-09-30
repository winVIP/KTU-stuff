using System;
using System.IO;
using System.Diagnostics;
using System.Threading;

namespace MergeSort
{
    class MergeSort
    {
        static int fileCount = 0;

        public static void MergeSortArray(DataArray items, int left, int right)
        {
            if (left < right)
            {
                int middle = (left + right) / 2;
                MergeSortArray(items, left, middle);
                MergeSortArray(items, middle + 1, right);
                Merge(items, left, middle, right);
            }
        }

        public static void Merge(DataArray items, int left, int middle, int right)
        {
            MyDataArray leftArray = new MyDataArray(middle - left + 1);
            MyDataArray rightArray = new MyDataArray(right - middle);

            int index = 0;
            for (int k = left; k < middle + 1; k++)
                leftArray.Put(index++, items[k]);
            index = 0;
            for (int k = middle + 1; k < right + 1; k++)
                rightArray.Put(index++, items[k]);
            int j = 0, i = 0;
            for (int k = left; k < right + 1; k++)
            {
                if (i == leftArray.Length)
                {
                    items.Put(k, rightArray[j]);
                    j++;
                }
                else if (j == rightArray.Length)
                {
                    items.Put(k, leftArray[i]);
                    i++;
                }
                else if (leftArray[i] <= rightArray[j])
                {
                    items.Put(k, leftArray[i]);
                    i++;
                }
                else
                {
                    items.Put(k, rightArray[j]);
                    j++;
                }
            }
        }

        public static void MergeSortArrayFile(DataArray items, int left, int right, int seed, string fileName)
        {
            if (left < right)
            {
                int middle = (left + right) / 2;
                MergeSortArrayFile(items, left, middle, seed, fileName);
                MergeSortArrayFile(items, middle + 1, right, seed, fileName);
                MergeFile(items, left, middle, right, seed, fileName);
            }
        }

        public static void MergeFile(DataArray items, int left, int middle, int right, int seed, string fileName)
        {
            string filename1 = "@" + (fileCount++).ToString() + ".dat";
            string filename2 = "@" + (fileCount++).ToString() + ".dat";
            MyFileArray leftArray = new MyFileArray(filename1, middle - left + 1, seed);
            MyFileArray rightArray = new MyFileArray(filename2, right - middle, seed);
            leftArray.fs = new FileStream(filename1, FileMode.Open, FileAccess.ReadWrite);
            rightArray.fs = new FileStream(filename2, FileMode.Open, FileAccess.ReadWrite);
            int index = 0;
            for (int k = left; k < middle + 1; k++)
                leftArray.Put(index++, items[k]);
            index = 0;
            for (int k = middle + 1; k < right + 1; k++)
                rightArray.Put(index++, items[k]);
            int j = 0, i = 0;
            for (int k = left; k < right + 1; k++)
            {
                if (i == leftArray.Length)
                {
                    items.Put(k, rightArray[j]);
                    j++;
                }
                else if (j == rightArray.Length)
                {
                    items.Put(k, leftArray[i]);
                    i++;
                }
                else if (leftArray[i] <= rightArray[j])
                {
                    items.Put(k, leftArray[i]);
                    i++;
                }
                else
                {
                    items.Put(k, rightArray[j]);
                    j++;
                }
            }
            leftArray.fs.Close();
            rightArray.fs.Close();
        }
    
        public static DataList MergeSortList(DataList items)
        {
            if (items.Length <= 1)
                return items;

            int middle = items.Length / 2;
            DataList leftList = new MyDataList(items, 0, middle);
            DataList rightList = new MyDataList(items, middle, items.Length);
            leftList = MergeSortList(leftList);
            rightList = MergeSortList(rightList);
            return Merge(leftList, rightList);
        }

        public static DataList Merge(DataList leftList, DataList rightList)
        {
            DataList result;
            leftList.Head();
            rightList.Head();
            int l = leftList.Length, r = rightList.Length;
            if (leftList.Head() > rightList.Head())
            {
                result = new MyDataList(leftList.Length + rightList.Length, rightList.Head());
                rightList.NextInArray();
                r--;
            }
            else
            {
                result = new MyDataList(leftList.Length + rightList.Length, leftList.Head());
                leftList.NextInArray();
                l--;
            }
            while (r + l > 0)
            {
                if (l > 0 && r > 0)
                {
                    if (leftList.Current() <= rightList.Current())
                    {
                        result.Add(leftList.Current());
                        leftList.NextInArray();
                        l--;
                    }
                    else
                    {
                        result.Add(rightList.Current());
                        rightList.NextInArray();
                        r--;
                    }
                }
                else if (l > 0)
                {
                    result.Add(leftList.Current());
                    leftList.NextInArray();
                    l--;

                }
                else if (r > 0)
                {
                    result.Add(rightList.Current());
                    rightList.NextInArray();
                    r--;
                }
            }
            return result;
        }

        public static DataList MergeSortListFile(DataList items, int start, int end)
        {
            if (start < end)
            {
                int middle = (start + end) / 2;

                MergeSortListFile(items, start, middle);
                MergeSortListFile(items, middle + 1, end);

                return MergeFile(items, start, middle, end);
            }
            return items;
        }

        public static DataList MergeFile(DataList items, int start, int middle, int end)
        {
            string filename1 = "@@" + (fileCount++).ToString() + ".dat";
            string filename2 = "@@" + (fileCount++).ToString() + ".dat";
            MyFileList leftList = new MyFileList(filename1, items, start, middle);
            MyFileList rightList = new MyFileList(filename2, items, middle + 1, end);
            leftList.fs = new FileStream(leftList.Filename, FileMode.Open, FileAccess.ReadWrite);
            rightList.fs = new FileStream(rightList.Filename, FileMode.Open, FileAccess.ReadWrite);

            int left = leftList.Length;
            int right = rightList.Length;

            items.Head();
            leftList.Head();
            rightList.Head();
            int k = start;
            while (right + left > 0)
            {
                if (left > 0 && right > 0)
                {
                    if (leftList.GetCurrent <= rightList.GetCurrent)
                    {
                        items.Find(k, leftList.GetCurrent);
                        leftList.NextAndCurrent();
                        left--;
                    }
                    else
                    {
                        items.Find(k, rightList.GetCurrent);
                        rightList.NextAndCurrent();
                        right--;
                    }
                }
                else if (left > 0)
                {
                    items.Find(k, leftList.GetCurrent);
                    leftList.NextAndCurrent();
                    left--;

                }
                else if (right > 0)
                {
                    items.Find(k, rightList.GetCurrent);
                    rightList.NextAndCurrent();
                    right--;
                }
                k++;
            }

            leftList.fs.Close();
            rightList.fs.Close();
            return items;
        }

        public static void SortTestOP(int n, bool show)
        {
            Console.WriteLine("Duomenu kiekis: " + n);

            Stopwatch stopwatch = new Stopwatch();

            double executionTime = 0;

            int seed = (int)DateTime.Now.Ticks & 0x0000FFFF;

            MyDataArray MyArray = new MyDataArray(n, seed);
            Console.WriteLine(" ARRAY ");
            if (show)
            {

                MyArray.Print(n);
            }

            stopwatch.Start();
            MergeSortArray(MyArray, 0, MyArray.Length - 1);
            stopwatch.Stop();
            executionTime += stopwatch.ElapsedTicks;
            if (show)
            {
                Console.WriteLine("\n MERGE SORTED ARRAY");
                MyArray.Print(n);
            }

            Console.WriteLine("Rusiavimas uztruko: " + executionTime / 10000000 + "s");
            Console.WriteLine();


            executionTime = 0;
            MyDataList MyList = new MyDataList(n, seed);
            Console.WriteLine("\n LIST ");
            if (show)
            {

                MyList.PrintArray();
            }

            stopwatch.Restart();
            MyList = (MyDataList)MergeSortList(MyList);
            stopwatch.Stop();
            executionTime += stopwatch.ElapsedTicks;
            if (show)
            {
                Console.WriteLine("\n MERGE SORTED LIST ");
                MyList.PrintArray();
            }

            Console.WriteLine("Rusiavimas uztruko: " + executionTime / 10000000 + "s");
            Console.WriteLine();
        }

        public static void SortTestD(int n, bool show)
        {
            Console.WriteLine("Duomenu kiekis: " + n);

            Stopwatch stopwatch = new Stopwatch();

            double executionTime = 0;

            int seed = (int)DateTime.Now.Ticks & 0x0000FFFF;

            string filename;
            filename = @"mydataarray.dat";
            MyFileArray myfilearray = new MyFileArray(filename, n, seed);
            using (myfilearray.fs = new FileStream(filename, FileMode.Open,
            FileAccess.ReadWrite))
            {
                Console.WriteLine("\n FILE ARRAY");
                if (show)
                {
                    
                    myfilearray.Print(n);
                }
                
                stopwatch.Restart();
                MergeSortArrayFile(myfilearray, 0, myfilearray.Length - 1, seed, @"temp.dat");
                stopwatch.Stop();
                executionTime += stopwatch.ElapsedTicks;

                if (show)
                {
                    Console.WriteLine("\n MERGE SORTED FILE ARRAY ");
                    myfilearray.Print(n);
                }
            }
            Console.WriteLine("Rusiavimas uztruko: " + executionTime / 10000000 + "s");
            Console.WriteLine();


            executionTime = 0;
            filename = @"mydatalist.dat";
            MyFileList myfilelist = new MyFileList(filename, n, seed);
            using (myfilelist.fs = new FileStream(filename, FileMode.Open,
            FileAccess.ReadWrite))
            {
                Console.WriteLine("\n FILE LIST");
                if (show)
                {
                    
                    myfilelist.Print();
                }
                
                stopwatch.Restart();
                MergeSortListFile(myfilelist, 0, myfilelist.Length - 1);
                stopwatch.Stop();
                executionTime += stopwatch.ElapsedTicks;

                if (show)
                {
                    Console.WriteLine("\n MERGE SORTED FILE LIST");
                    myfilelist.Print();
                }
            }
            Console.WriteLine("Rusiavimas uztruko: " + executionTime / 10000000 + "s");
            Console.WriteLine();

            string filesToDelete = @"@*.dat";
            string[] fileList = Directory.GetFiles(Directory.GetCurrentDirectory(), filesToDelete);
            foreach (string file in fileList)
            {
                File.Delete(file);
            }
        }
    }
}