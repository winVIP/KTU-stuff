using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DvigubasHesavimas
{
    class HashTableDouble<K, V>
    {
        protected Node<K, V>[] table;
        protected int size = 0;
        protected float loadFactor;
        const int maxSize = 10; //our table size
        protected int index = 0;
        protected int rehashesCounter = 0;

        public HashTableDouble(int initialCapacity, float loadFactor)
        {
            this.table = new Node<K, V>[initialCapacity];
            this.loadFactor = loadFactor;
        }

        public void Clear()
        {
            Array.Clear(table, 0, table.Length);
            size = 0;
            index = 0;
            rehashesCounter = 0;
        }

        private bool checkOpenSpace()//checks for open spaces in the table for insertion
        {
            bool isOpen = false;
            for (int i = 0; i < maxSize; i++)
            {
                if (table[i] == null)
                {
                    isOpen = true;
                }
            }
            return isOpen;
        }

        public V Put(K key, V value)
        {
            index = Hash(key);
            int hashAdded = hash2(key);

            int chng = 0;
            while (table[index] != null)
            {
                chng++;
                index += hashAdded;
                index %= table.Length;
            }
            table[index] = new Node<K, V>(key, value);
            table[index].changes = chng;
            size++;
            return value;
        }

        public void Rehash()
        {
            HashTableDouble<K, V> newTable =
                    new HashTableDouble<K, V>(this.table.Length * 2, loadFactor);
            for (int i = 1; i < this.table.Length; i++)
            {
                if (this.table[i] != null)
                {
                    newTable.Put(this.table[i].key, this.table[i].value);
                }
            }
            table = newTable.table;
            rehashesCounter++;
        }

        private int Hash(K key)
        {
            int h = key.GetHashCode();
            return Math.Abs(h) % table.Length;
        }

        private int hash2(K key)
        {
            return 7 - (Math.Abs(key.GetHashCode() % 7));
        }

        public V Get(K key)
        {
            index = Hash(key);
            for (int i = 0; i < table.Length; i++)
            {
                int pointer = (index + i) % (table.Length);
                if (table[pointer] == null)
                {
                    return default(V);
                }
                else if (table[pointer].key.Equals(key))
                {
                    Node<K, V> res = table[index];
                    return res.value;
                }
            }
            return default(V);
        }

        public V Remove(K key)
        {

            index = findNode(key);
            for (int i = 0; i < table.Length; i++)
            {
                int pointer = (index + i * i) % (table.Length);
                if (table[pointer] != null && table[pointer].key.Equals(key))
                {
                    table[pointer] = null;
                    size--;
                    break;
                }
            }

            return default(V);
        }

        private int findNode(K key)
        {

            int index = Hash(key);
            int index0 = index;
            int i = 0;
            for (int j = 0; j < table.Length; j++)
            {
                if (table[index] == null || table[index].key.Equals(key))
                {
                    return index;
                }
                i++;
                index = (index0 + i * hash2(key)) % table.Length;
            }
            return -1;

        }
        public bool contains(K key)
        {
            for (int i = 0; i < size - 1; i++)
            {
                if (table[i] != null)
                {
                    if (table[i].key.Equals(key))
                    {
                        return true;
                    }
                }
            }
            return table[findNode(key)] != null;
        }
        public override string ToString()
        {
            StringBuilder result = new StringBuilder();
            foreach (Node<K, V> node in table)
            {
                if (node != null)
                {
                    result.Append(node.ToString()).Append("\n");
                }
            }
            return result.ToString();
        }


        protected class Node<K, V>
        {
            public K key { get; set; }
            public V value { get; set; }
            private bool empty;
            public int changes;

            public Node() { }

            public Node(K key, V value)
            {
                this.key = key;
                this.value = value;
                empty = false;
                changes = 0;
            }


            public override string ToString()
            {
                return key + "=" + value;
            }
        }
    }
}
