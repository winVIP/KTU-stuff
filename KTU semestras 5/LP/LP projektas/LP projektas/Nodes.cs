using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace LP_projektas
{
    class Nodes
    {
        private double[,] nodes;
        private object _lock;
        private int nodeCount;
        private int maxNodeCount;
        public Nodes(int size)
        {
            maxNodeCount = size;
            nodes = new double[maxNodeCount, 2];
            _lock = new object();
            nodeCount = 0;
        }

        public double[] getNode(int index)
        {
            double[] node = { nodes[index, 0], nodes[index, 1]  };
            return node;
        }
        public double[,] getAllNodes()
        {
            return nodes;
        }

        public void addNode(double x, double y)
        {
            Monitor.Enter(_lock);
            if(nodeCount == maxNodeCount)
            {
                Console.WriteLine("Cant add node, array is full");
                return;
            }
            nodes[nodeCount, 0] = x;
            nodes[nodeCount, 1] = y;
            nodeCount++;
            Monitor.Exit(_lock);
        }

        public void setAllNodes(double[,] nodes)
        {
            Monitor.Enter(_lock);
            this.nodes = nodes;
            Monitor.Exit(_lock);
        }

        public void changeNode(int index, double x, double y)
        {
            Monitor.Enter(_lock);
            nodes[index, 0] = x;
            nodes[index, 1] = y;
            Monitor.Exit(_lock);
        }

        public int getMax()
        {
            return maxNodeCount;
        }

        public int getCount()
        {
            return nodeCount;
        }
    }
}
