using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace LP_projektas
{
    class Gradients
    {
        private double[] gradients;
        private object _lock;
        private int gradCount;
        private int maxGradCount;
        private bool[] isAdded;
        private bool isNormalized;
        public Gradients(int size)
        {
            maxGradCount = size;
            gradients = new double[maxGradCount];
            _lock = new object();
            gradCount = 0;
            isAdded = new bool[maxGradCount];
        }

        public void Clear()
        {
            Monitor.Enter(_lock);
            ///Console.WriteLine("Thread: " + Thread.CurrentThread.Name + " is clearing");
            gradients = new double[maxGradCount];
            gradCount = 0;
            isAdded = new bool[maxGradCount];
            isNormalized = false;
            //Console.WriteLine("Thread: " + Thread.CurrentThread.Name + " has cleared");
            Monitor.Exit(_lock);
        }

        public double getGrad(int index)
        {
            return gradients[index];
        }
        public double[] getAllGrads()
        {
            return gradients;
        }

        public void addGrad(double grad)
        {
            Monitor.Enter(_lock);
            if (gradCount == maxGradCount)
            {
                Console.WriteLine("Cant add gradient, array is full");
                return;
            }
            gradients[gradCount] = grad;
            isAdded[gradCount] = true;
            gradCount++;
            Monitor.Exit(_lock);
        }

        public void addGrad(int index, double grad)
        {
            Monitor.Enter(_lock);
            if (isAdded[index] == true)
            {
                Console.WriteLine("Cant add gradient, the space is occupied, currentThread: " + Thread.CurrentThread.Name + " Gradient: " + gradients[index] + " Index: " + index);
                return;
            }
            gradients[index] = grad;
            isAdded[index] = true;
            gradCount++;
            Monitor.Exit(_lock);
        }

        public void changeGrad(int index, double grad)
        {
            Monitor.Enter(_lock);
            gradients[index] = grad;
            Monitor.Exit(_lock);
        }

        public int getMax()
        {
            return maxGradCount;
        }

        public int getCount()
        {
            return gradCount;
        }

        private double VectorLength(double[] vector)
        {
            double sum = 0;
            foreach (double item in vector)
            {
                sum += Math.Pow(item, 2);
            }
            return Math.Sqrt(sum);
        }

        public void NormalizeGradientVector()
        {
            double vectorLenght = VectorLength(gradients);
            double[] newVector = new double[gradients.Length];
            for (int i = 0; i < gradients.Length; i++)
            {
                newVector[i] = gradients[i] / vectorLenght;
            }
        }

        public bool IsNormalized()
        {
            return isNormalized;
        }

        
    }
}
