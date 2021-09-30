using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace HashTable
{
    class HashTableD
    {

        private string FileName = @"myhashtable.dat";
        int CurrentCollectionSize = 0;

        public HashTableD()
        { 
            if (File.Exists(FileName)) File.Delete(FileName);
                try
                {
                    using (BinaryWriter writer = new BinaryWriter(File.Open(FileName, FileMode.Create))) {}
                }
                catch (IOException ex)
                {
                    Console.WriteLine(ex.ToString());
                }
        }

        public bool DoesKeyExist(string key)
        {
            using (FileStream fileStream = new FileStream(FileName, FileMode.Open, FileAccess.ReadWrite))
            {
                try
                {
                    int index = GetHash(key);
                    if (index < 0)
                    {
                        index *= -1;
                    }
                    fileStream.Seek(index, SeekOrigin.Begin);
                    byte[] dataAsInt = new byte[4];
                    fileStream.Read(dataAsInt, 0, 4);
                    int valueFromFile = BitConverter.ToInt32(dataAsInt, 0);
                    return valueFromFile != 0;
                }
                catch (Exception e)
                {
                    return false;
                }
            }
        }

        public int GetValue(string key)
        {
            if (DoesKeyExist(key))
            {
                using (FileStream fileStream = new FileStream(FileName, FileMode.Open, FileAccess.ReadWrite))
                {
                    int index = GetHash(key);
                    if (index < 0)
                    {
                        index *= -1;
                    }
                    fileStream.Seek(index, SeekOrigin.Begin);
                    byte[] valueAsBytes = new byte[4];
                    fileStream.Read(valueAsBytes, 0, 4);
                    return BitConverter.ToInt16(valueAsBytes, 0);
                }
            }
            return 0;
        }

        private int GetHash(string key)
        {
            int hash = 0;
            hash = Hash1(key);
            hash = Hash2(key);
            return hash;
        }

        private int Hash1(string key)
        {
            return key.GetHashCode() * 7;
        }

        private int Hash2(string key)
        {
            int k = key.Length;
            char[] chars = key.ToCharArray();
            int sum = 0;
            for (int i = 0; i < k - 1; i++)
            {
                sum += chars[i] << (5 * i);
            }
            return sum;
        }

        public bool Insert(string key, int value)
        {
            if (string.IsNullOrEmpty(FileName) || DoesKeyExist(key))
            {
                return false;
            }
            using (FileStream fileStream = new FileStream(
                FileName, FileMode.Open, FileAccess.ReadWrite))
            {
                try
                {
                    int index = GetHash(key);
                    if (index < 0)
                    {
                        index *= -1;
                    }
                    fileStream.Seek(index, SeekOrigin.Begin);
                    fileStream.Write(BitConverter.GetBytes(value), 0, 4);
                    CurrentCollectionSize++;
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

    }
}
