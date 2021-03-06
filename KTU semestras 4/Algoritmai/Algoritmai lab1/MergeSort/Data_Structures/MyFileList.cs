using MergeSort;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MergeSort
{
    class MyFileList : DataList
    {
        int prevNode;
        int currentNode;
        int nextNode;
        public MyFileList(string filename, int n, int seed)
        {
            length = n;
            this.filename = filename;
            Random rand = new Random(seed);
            if (File.Exists(filename)) File.Delete(filename);
            try
            {
                using (BinaryWriter writer = new BinaryWriter(File.Open(filename,
                FileMode.Create)))
                {
                    writer.Write(4);
                    for (int j = 0; j < length; j++)
                    {
                        writer.Write(rand.NextDouble());
                        writer.Write((j + 1) * 12 + 4); //ilgis
                    }
                }
            }
            catch (IOException ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public MyFileList(string filename, DataList list, int beg, int end)
        {
            length = 0;
            this.filename = filename;
            list.Head();
            if (File.Exists(filename)) File.Delete(filename);
            try
            {
                using (BinaryWriter writer = new BinaryWriter(File.Open(this.filename,
                FileMode.Create)))
                {
                    writer.Write(4);
                    int j = 0;
                    for (int i = 0; i < list.Length; i++)
                    {
                        if (i >= beg && i <= end)
                        {
                            writer.Write(list.Current());
                            length++;
                            writer.Write((j + 1) * 12 + 4); //ilgis
                            j++;

                        }
                        list.Next();
                    }
                }
            }
            catch (IOException ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public MyFileList(string filename)
        {
            if (File.Exists(filename)) File.Delete(filename);
            length = 0;
            this.filename = filename;
        }

        public override void Add(double d)
        {
            fs.Seek(0, SeekOrigin.End);
            if (length == 0) fs.Write(BitConverter.GetBytes(4), 0, 4);
            Byte[] data = BitConverter.GetBytes(d);
            fs.Write(data, 0, 8);
            fs.Write(BitConverter.GetBytes((length + 1) * 12 + 4), 0, 4);
            length++;
        }

        public FileStream fs { get; set; }

        public override int Find(int index, double j)
        {
            if (index >= length)
                return -1;
            Head();
            int i = 0;
            if (index > 0)
            {
                for (i = 0; i < index; i++)
                {
                    Next();
                }
            }
            Byte[] data = new Byte[12];
            fs.Seek(currentNode, SeekOrigin.Begin);
            data = BitConverter.GetBytes(j);
            fs.Write(data, 0, 8);
            return i;
        }

        public override double Head()
        {
            Byte[] data = new Byte[12];
            fs.Seek(0, SeekOrigin.Begin);
            fs.Read(data, 0, 4);
            currentNode = BitConverter.ToInt32(data, 0);
            prevNode = -1;
            fs.Seek(currentNode, SeekOrigin.Begin);
            fs.Read(data, 0, 12);
            double result = BitConverter.ToDouble(data, 0);
            current = result;
            nextNode = BitConverter.ToInt32(data, 8);
            return result;
        }
        public override double Next()
        {
            Byte[] data = new Byte[12];
            fs.Seek(nextNode, SeekOrigin.Begin);
            fs.Read(data, 0, 12);
            prevNode = currentNode;
            currentNode = nextNode;
            double result = BitConverter.ToDouble(data, 0);
            current = -1;
            nextNode = BitConverter.ToInt32(data, 8);
            return result;
        }
        public override double Current()
        {
            Byte[] data = new Byte[12];
            fs.Seek(currentNode, SeekOrigin.Begin);
            fs.Read(data, 0, 12);
            double result = BitConverter.ToDouble(data, 0);
            current = result;
            currentNode = BitConverter.ToInt32(data, 8);
            return result;
        }
        public override void NextAndCurrent()
        {
            Next();
            Current();
        }
        public override void Swap(double a, double b)
        {
            Byte[] data;
            fs.Seek(prevNode, SeekOrigin.Begin);
            data = BitConverter.GetBytes(a);
            fs.Write(data, 0, 8);
            fs.Seek(currentNode, SeekOrigin.Begin);
            data = BitConverter.GetBytes(b);
            fs.Write(data, 0, 8);
        }

        public override double? NextInArray()
        {
            throw new NotImplementedException();
        }
    }
}