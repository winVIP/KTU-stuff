using System;
using System.Collections.Generic;
using System.Threading;
using System.Diagnostics;
using System.IO;

namespace LP_projektas
{
    class Program
    {
        static int nodeAmount = 20;
        static Nodes nodes = new Nodes(nodeAmount);
        static Gradients gradients = new Gradients(nodeAmount * 2);
        static int threadCount = 4;
        static object _lock = new object();
        static void Main(string[] args)
        {
            List<string> laikai = new List<string>();
            Stopwatch stopwatch = new Stopwatch();

            //Uzduotis3();

            for (int i = 20; i <= 200; i = i + 20)
            {
                for (int j = 0; j < 3; j++)
                {
                    long[] tarpiniaiLaikai = new long[3];
                    if (j == 0)
                    {
                        threadCount = 1;
                    }
                    else if (j == 1)
                    {
                        threadCount = 2;
                    }
                    else
                    {
                        threadCount = 4;
                    }
                    nodeAmount = i;
                    Console.WriteLine("Su " + i + " tasku ir " + threadCount + " gijom, programa užtruko: ");
                    for (int k = 0; k < 3; k++)
                    {
                        nodes = new Nodes(nodeAmount);
                        gradients = new Gradients(nodeAmount * 2);
                        nodes.addNode(1, 2);
                        nodes.addNode(3, 2);
                        nodes.addNode(1, -2);
                        nodes.addNode(3, -2);
                        stopwatch.Restart();
                        Uzduotis3();
                        stopwatch.Stop();
                        Console.WriteLine(stopwatch.ElapsedMilliseconds);
                        tarpiniaiLaikai[k] = stopwatch.ElapsedMilliseconds;
                    }
                    long vidurkis = (tarpiniaiLaikai[0] + tarpiniaiLaikai[1] + tarpiniaiLaikai[2]) / 3;
                    laikai.Add("Su " + i + " tasku ir " + threadCount + " gijom, programa užtruko: ");
                    laikai.Add(tarpiniaiLaikai[0].ToString());
                    laikai.Add(tarpiniaiLaikai[1].ToString());
                    laikai.Add(tarpiniaiLaikai[2].ToString());
                    laikai.Add("Vidurkis:\n" + vidurkis);
                    Console.WriteLine("Vidurkis: " + vidurkis);
                }
            }

            File.WriteAllLines(@"C:\Users\vyten\Desktop\LP projektas\laikai.txt", laikai);


            //double[,] goodNodes = nodes.getAllNodes();
            //for (int i = 0; i < nodeAmount; i++)
            //{
            //    Console.WriteLine("x = {0} y = {1}", goodNodes[i, 0], goodNodes[i, 1]);
            //}
        }

        static void Uzduotis3()
        {
            double result = 0;
            double step = 0.01;
            int maxIterations = 100;
            bool[,] canWork = new bool[maxIterations, 3];

            double fValue = CostFunction(nodes.getAllNodes());
            Nodes nodesCopy = new Nodes(nodes.getMax());
            bool[,] nodesChanged = new bool[maxIterations, threadCount];
            bool[,] gradCounts = new bool[maxIterations, threadCount];

            Thread[] threads = new Thread[threadCount];
                
            for (int j = 0; j < threads.Length; j++)
            {
                threads[j] = new Thread(() =>
                {
                    for (int i = 0; i < maxIterations; i++)
                    {
                        int currentThread = int.Parse(Thread.CurrentThread.Name);
                        int from = currentThread * nodeAmount / threadCount;
                        int to = from + nodeAmount / threadCount;
                        int fromG = currentThread * nodeAmount / threadCount * 2;
                        int toG = fromG + nodeAmount / threadCount * 2;
                        int count = 0;
                        //Console.WriteLine("Current thread: " + currentThread + " Iteration: " + i + " Grad Count: " + gradients.getCount() + " FromG: " + fromG + " toG " + toG);

                        if (currentThread == 0)
                        {
                            fValue = CostFunction(nodes.getAllNodes());
                            nodesCopy = new Nodes(nodes.getMax());
                            for (int z = 0; z < nodes.getMax(); z++)
                            {
                                nodesCopy.addNode(nodes.getNode(z)[0], nodes.getNode(z)[1]);
                            }
                            canWork[i, 0] = true;
                        }
                        else
                        {
                            while(canWork[i, 0] != true)
                            {
                                if(canWork[i, 0] == true)
                                {
                                    if (currentThread == 1)
                                        //Console.WriteLine("Current thread: " + currentThread + " Iteration: " + i + " broke through first barrier");
                                    break;
                                }
                            }
                        }

                        double[] grads = Gradient(nodes.getAllNodes(), from, to);
                        for (int z = fromG; z < toG; z++)
                        {
                            gradients.addGrad(z, grads[count]);
                            count++;
                        }
                        gradCounts[i, currentThread] = true;
                        count = 0;

                        while (true)
                        {
                            //Console.WriteLine("Current thread: " + currentThread + " Iteration: " + i + " Grad Count: " + gradCounts[i, currentThread] + " FromG: " + fromG + " toG " + toG);
                            bool breaker = true;
                            for (int z = 0; z < threadCount; z++)
                            {
                                breaker = breaker && gradCounts[i, z];
                            }
                            if (breaker == true)
                            {
                                break;
                            }
                        }

                        if (currentThread == 0)
                        {
                            gradients.NormalizeGradientVector();
                            canWork[i, 1] = true;
                        }
                        else
                        {
                            while (canWork[i, 1] != true)
                            {
                                if (canWork[i, 1] == true)
                                {
                                    if (currentThread == 1)
                                        //Console.WriteLine("Current thread: " + currentThread + " Iteration: " + i + " broke through second barrier");
                                    break;
                                }
                            }
                        }
                        grads = gradients.getAllGrads();

                        if (currentThread == 0)
                        {
                            from = 4;
                        }
                        for (int z = from; z < to; z++)
                        {
                            nodesCopy.changeNode(z, nodesCopy.getNode(z)[0] - (step * grads[count]), nodesCopy.getNode(z)[1] - (step * grads[count + 1]));
                            count = count + 2;
                            
                        }
                        nodesChanged[i, currentThread] = true;

                        while (true)
                        {
                            //Console.WriteLine("Current thread: " + currentThread + " Iteration: " + i + " Nodes changed: " + nodesChanged[i, currentThread]);
                            bool breaker = true;
                            for (int z = 0; z < threadCount; z++)
                            {
                                breaker = breaker && nodesChanged[i, z];
                            }
                            if (breaker == true)
                            {
                                break;
                            }
                        }

                        if (currentThread == 0)
                        {
                            double fValue1 = CostFunction(nodesCopy.getAllNodes());

                            if (fValue1 < fValue)
                            {
                                nodes.setAllNodes(nodesCopy.getAllNodes());
                                result = fValue1;
                            }
                            else
                            {
                                step = step / 2;
                            }
                            gradients.Clear();
                            canWork[i, 2] = true;
                        }
                        else
                        {
                            while (canWork[i, 2] != true)
                            {
                                if (canWork[i, 2] == true)
                                {
                                    if(currentThread == 1)
                                        //Console.WriteLine("Current thread: " + currentThread + " Iteration: " + i + " broke through third barrier");
                                    break;
                                }
                            }
                        }
                    }
                });
                threads[j].Name = j.ToString();
            }

            for (int j = 0; j < threads.Length; j++)
            {
                threads[j].Start();
            }

            for (int j = 0; j < threads.Length; j++)
            {
                threads[j].Join();
            }
        }

        static double Distance(double x1, double x2, double y1, double y2)
        {
            return Math.Sqrt(Math.Pow(x2 - x1, 2) + Math.Pow(y2 - y1, 2));
        }

        static double AvarageDistance(double[,] nodes)
        {
            int dCount = 0;
            double sum = 0;
            for (int i = 0; i < nodes.GetLength(0); i++)
            {
                for (int j = i + 1; j < nodes.GetLength(0); j++)
                {
                    sum += Distance(nodes[i, 0], nodes[j, 0], nodes[i, 1], nodes[j, 1]);
                    dCount++;
                }
            }

            return sum / dCount;
        }

        static double NodesCost(double[,] nodes)
        {
            double sum = 0;
            for (int i = 0; i < nodes.GetLength(0); i++)
            {
                sum += nodes[i, 0] * Math.Pow(Math.E, -((Math.Pow(nodes[i, 0], 2) + Math.Pow(nodes[i, 1], 2)) / 10)) + 1.5;
            }
            return sum;
        }

        static double NodeCost(double x, double y)
        {
            return x * Math.Pow(Math.E, -((Math.Pow(x, 2) + Math.Pow(y, 2)) / 10)) + 1.5;
        }
        static double[,] NodesCopy(double[,] nodes)
        {
            double[,] nodesCopy = new double[nodes.GetLength(0), nodes.GetLength(1)];

            for (int i = 0; i < nodes.GetLength(0); i++)
            {
                for (int j = 0; j < nodes.GetLength(1); j++)
                {
                    nodesCopy[i, j] = nodes[i, j];
                }
            }

            return nodesCopy;
        }

        static double CostFunction(double[,] nodes)
        {
            double nodeCost = NodesCost(nodes);
            double avgD = AvarageDistance(nodes);
            return nodeCost + avgD;
        }

        static double NodeGradient(int x, int y, double[,] nodes, double h)
        {
            double[,] nodesCopy = NodesCopy(nodes);

            nodesCopy[x, y] += h;
            return (CostFunction(nodesCopy) - CostFunction(nodes)) / h;
        }

        static double[] Gradient(double[,] nodes, int from, int to)
        {
            List<double> grads = new List<double>();
            for (int i = from; i < to; i++)
            {
                for (int j = 0; j < nodes.GetLength(1); j++)
                {
                    grads.Add(NodeGradient(i, j, nodes, 1e-5));
                }
            }
            return grads.ToArray();
        }

        static double VectorLength(double[] vector)
        {
            double sum = 0;
            foreach (double item in vector)
            {
                sum += Math.Pow(item, 2);
            }
            return Math.Sqrt(sum);
        }
    }
}
