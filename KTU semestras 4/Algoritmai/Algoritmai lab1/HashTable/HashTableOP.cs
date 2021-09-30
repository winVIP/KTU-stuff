using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HashTable
{
    class HashTableOP
    {
        class Entry
        {
            int key;
            string value;
            public Entry(int key, string value)
            {
                this.key = key;
                this.value = value;
            }
            public int getkey()
            {
                return key;
            }
            public string getdata()
            {
                return value;
            }
        }
        const int tableSize = 1000;
        Entry[] table;

        public HashTableOP()
        {
            table = new Entry[tableSize];
            for (int i = 0; i < tableSize; i++)
            {
                table[i] = null;
            }
        }

        public string GetValue(int key)
        {
            int hash = key % tableSize;
            while (table[hash] != null && table[hash].getkey() != key)
            {
                hash = (hash + 1) % tableSize;
            }
            if (table[hash] == null)
            {
                return "nothing found!";
            }
            else
            {
                return table[hash].getdata();
            }
        }

        private bool IsSpace()
        {
            bool isOpen = false;
            for (int i = 0; i < tableSize; i++)
            {
                if (table[i] == null)
                {
                    isOpen = true;
                }
            }
            return isOpen;
        }

        public void Insert(int key, string value)
        {
            if (!IsSpace())//if no open spaces available
            {
                Console.WriteLine("table is at full capacity!");
                return;
            }

            //double probing method
            int hashVal = Hash1(key);
            int stepSize = Hash2(key);

            while (table[hashVal] != null && table[hashVal].getkey() != key)
            {
                hashVal = (hashVal + stepSize * Hash2(key)) % tableSize;
            }
            table[hashVal] = new Entry(key, value);
            return;
        }

        private int Hash1(int key)
        {
            return key % tableSize;
        }

        private int Hash2(int key)
        {
            return 5 - key % 5;
        }
    }
}

