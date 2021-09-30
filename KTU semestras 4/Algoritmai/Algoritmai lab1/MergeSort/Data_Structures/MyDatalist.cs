using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MergeSort
{
    class MyDataList : DataList
    {
        class MyLinkedListNode
        {
            public MyLinkedListNode nextNode { get; set; }
            public double data { get; set; }
            public MyLinkedListNode(double data)
            {
                this.data = data;
            }
        }

        MyLinkedListNode headNode;
        MyLinkedListNode prevNode;
        MyLinkedListNode currentNode;

        public MyDataList(int n, int seed)
        {
            length = n;
            Random rand = new Random(seed);
            headNode = new MyLinkedListNode(rand.NextDouble());
            currentNode = headNode;
            for (int i = 1; i < length; i++)
            {
                prevNode = currentNode;
                currentNode.nextNode = new MyLinkedListNode(rand.NextDouble());
                currentNode = currentNode.nextNode;
            }
            currentNode.nextNode = null;
        }

        public MyDataList(DataList list, int beg, int end)
        {
            length = end - beg;
            list.Head();
            for (int i = 0; i < list.Length; i++)
            {
                if (beg == 0 && i == 0 || beg == i)
                {
                    headNode = new MyLinkedListNode(list.Current());
                    currentNode = headNode;
                }
                else if (beg < i && i < end)
                {
                    prevNode = currentNode;
                    currentNode.nextNode = new MyLinkedListNode(list.Current());
                    currentNode = currentNode.nextNode;
                }
                

                if (list.NextInArray() == null || i >= end)
                {
                    break;
                }
            }
            currentNode.nextNode = null;
        }

        public MyDataList(int n, double data)
        {
            length = n;
            headNode = new MyLinkedListNode(data);
            currentNode = headNode;
        }


        public override void Add(double data)
        {
            currentNode.nextNode = new MyLinkedListNode(data);
            currentNode = currentNode.nextNode;
        }

        public override double Head()
        {
            currentNode = headNode;
            prevNode = null;
            return currentNode.data;
        }

        public override double? NextInArray()
        {
            prevNode = currentNode;
            currentNode = currentNode.nextNode;
            if (currentNode != null)
                return currentNode.data;
            else
                return null;
        }

        public override double Current()
        {
            return currentNode.data;
        }

        public override void Swap(double a, double b)
        {
            prevNode.data = a;
            currentNode.data = b;
        }

        public override int Find(int i, double j)
        {
            throw new NotImplementedException();
        }

        public override void NextAndCurrent()
        {
            throw new NotImplementedException();
        }

        public override double Next()
        {
            throw new NotImplementedException();
        }
    }
}
