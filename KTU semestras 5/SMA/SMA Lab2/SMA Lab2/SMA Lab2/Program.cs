using System;
using System.Collections.Generic;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Factorization;

namespace SMA_Lab2
{
    class Program
    {
        static void Main(string[] args)
        {
            //Uzduotis1();
            //Uzduotis2Lygtis1();
            //Uzduotis2Lygtis2();
            //Uzduotis3();
        }

        #region Uzduotis 1

        static void Uzduotis1()
        {
            double[,] StartMatrix =
            {
                { 3, 7, 1, 3, 40 },
                { 1, -6, 6, 8, 19 },
                { 4, 4, -7, 1, 36 },
                { 4, 16, 2, 0, 48 }
            };
            double[,] matrix =
            {
                { 3, 7, 1, 3, 40 },
                { 1, -6, 6, 8, 19 },
                { 4, 4, -7, 1, 36 },
                { 4, 16, 2, 0, 48 }
            };

            Console.WriteLine("Pradine matrica: ");
            PrintMatrix(matrix);
            Console.WriteLine();

            int flag = 0;

            Console.WriteLine("Pertvarkomos matricos zingsniai: ");
            flag = GausasZordanas(matrix);

            if (flag == 1)
                flag = CheckConsistency(matrix, flag);

            Console.WriteLine("Pilnai Pertvarkyta matrica: ");
            PrintMatrix(matrix);
            Console.WriteLine("");

            PrintResult(matrix, flag);
            Uzduotis1Funkcija(StartMatrix, matrix);
        }

        static int GausasZordanas(double[,] matrix)
        {
            int c, flag = 0;

            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                if (matrix[i, i] == 0)
                {
                    c = 1;
                    while (matrix[i + c, i] == 0 && (i + c) < matrix.GetLength(0))
                    {
                        c++;
                    }
                    if ((i + c) == matrix.GetLength(0))
                    {
                        flag = 1;
                        break;
                    }
                    for (int j = i, k = 0; k <= matrix.GetLength(0); k++)
                    {
                        double temp = matrix[j, k];
                        matrix[j, k] = matrix[j + c, k];
                        matrix[j + c, k] = temp;
                    }
                }

                for (int j = 0; j < matrix.GetLength(0); j++)
                {
                    if (i != j)
                    {
                        double p = matrix[j, i] / matrix[i, i];

                        for (int k = 0; k <= matrix.GetLength(0); k++)
                            matrix[j, k] = matrix[j, k] - (matrix[i, k]) * p;
                    }
                }
                PrintMatrix(matrix);
                Console.WriteLine();
            }
            return flag;
        }

        static int CheckConsistency(double[,] matrix, int flag)
        {
            int i, j;
            double sum;

            flag = 3;
            for (i = 0; i < matrix.GetLength(0); i++)
            {
                sum = 0;
                for (j = 0; j < matrix.GetLength(0); j++)
                {
                    sum = sum + matrix[i, j];
                } 
                if (sum == matrix[i, j])
                {
                    flag = 2;
                } 
            }
            return flag;
        }

        static void PrintMatrix(double[,] matrix)
        {
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j <= matrix.GetLength(0); j++)
                    Console.Write("{0, 9:0.######} ", matrix[i, j]);
                Console.WriteLine();
            }
        }

        static void PrintResult(double[,] matrix, int flag)
        {
            Console.Write("Rezultatas: ");

            if (flag == 2)
                Console.WriteLine("Begalybe galimu atsakymu");
            else if (flag == 3)
                Console.WriteLine("Nera atsakymo");

            else
            {
                for (int i = 0; i < matrix.GetLength(0); i++)
                    Console.Write(matrix[i, matrix.GetLength(0)] / matrix[i, i] + " ");
            }
            Console.WriteLine();
        }

        static void Uzduotis1Funkcija(double[,] matrix, double[,] resultMatrix)
        {
            double[] coeficients = new double[4];
            for (int i = 0; i < resultMatrix.GetLength(0); i++)
                coeficients[i] = resultMatrix[i, resultMatrix.GetLength(0)] / resultMatrix[i, i];
            double[] results = new double[4];
            for (int i = 0; i < results.Length; i++)
            {
                results[i] = 0;
            }
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(0); j++)
                {
                    results[i] += matrix[i, j] * coeficients[j];
                }
            }
            Console.WriteLine("Isistacius i sistema gautus x, gaunami tokie y: ");
            foreach (double item in results)
            {
                Console.Write(item + " ");
            }
            Console.WriteLine();
        }

        #endregion

        #region Uzduotis 2

        static double[,] J2x2(double x1, double x2)
        {
            double[,] jMatrica = new double[2, 2];

            double x11 = Math.Pow(x1, 3) / 64 - x1 / 2;

            double x12 = Math.Pow(x2, 3) / 64 - x2 / 2;

            double x21 = 2 * x1 + x2 - 8;

            double x22 = 2 * x2 + x1 - 8;

            jMatrica[0, 0] = x11;
            jMatrica[0, 1] = x12;
            jMatrica[1, 0] = x21;
            jMatrica[1, 1] = x22;

            return jMatrica;
        }

        static double[,] TransposeMatrix(double[,] m)
        {
            int index = 0;
            double[] members = new double[m.GetLength(0) * m.GetLength(1)];
            double[,] transposed = new double[m.GetLength(0), m.GetLength(1)];

            for (int j = 0; j < m.GetLength(1); j++)
            {
                for (int i = 0; i < m.GetLength(0); i++)
                {
                    members[index] = m[i, j];
                    index++;
                }
            }

            index = 0;
            for (int i = 0; i < transposed.GetLength(0); i++)
            {
                for (int j = 0; j < transposed.GetLength(1); j++)
                {
                    transposed[i, j] = members[index];
                    index++;
                }
            }

            return transposed;
        }

        static double[,] F1(double x1, double x2)
        {
            double[,] matrica = new double[2, 1];

            double x11 = Math.Pow(x1 / 4, 4) + Math.Pow(x2 / 4, 4) - (Math.Pow(x1 / 2, 2) + Math.Pow(x2 / 2, 2)) + 5;

            double x21 = Math.Pow(x1, 2) + Math.Pow(x2, 2) + x1 * x2 - 8 * (x1 + x2) - 4;

            matrica[0, 0] = x11;
            matrica[1, 0] = x21;

            return matrica;
        }

        static double[,] MatrixMultiplication(double[,] m1, double[,] m2)
        {
            double[,] result = new double[m1.GetLength(0), m2.GetLength(1)];
            for (int i = 0; i < result.GetLength(0); i++)
            {
                for (int j = 0; j < result.GetLength(1); j++)
                {
                    result[i, j] = 0;
                    for (int k = 0; k < m1.GetLength(1); k++) // OR k<b.GetLength(0)
                        result[i, j] = result[i, j] + m1[i, k] * m2[k, j];
                }
            }
            return result;
        }

        static double[,] Grad1(double x1, double x2)
        {
            double[,] jT = TransposeMatrix(J2x2(x1, x2));
            double[,] f = F1(x1, x2);

            double[,] grad = MatrixMultiplication(jT, f);

            return grad;
        }

        static double ObjectiveFunc(double x1, double x2)
        {
            double x11 = Math.Pow(x1 / 4, 4) + Math.Pow(x2 / 4, 4) - (Math.Pow(x1 / 2, 2) + Math.Pow(x2 / 2, 2)) + 5;

            double x21 = Math.Pow(x1, 2) + Math.Pow(x2, 2) + x1 * x2 - 8 * (x1 + x2) - 4;

            x11 = Math.Pow(x11, 2);
            x21 = Math.Pow(x21, 2);
            return (x11 + x21) / 2;
        }

        static void Uzduotis2Lygtis1()
        {
            double[,] xx = { { -3 }, { 8 } };
            Matrix<double> x = Matrix<double>.Build.DenseOfArray(xx);
            double step0 = 0.5;
            double step = step0;
            for(int i = 0; i < 20; i++)
            {
                double[,] grad = Grad1(x.At(0, 0), x.At(1, 0));
                double target = ObjectiveFunc(x.At(0, 0), x.At(1, 0));
                for(int j = 0; j < 30; j++)
                {
                    Matrix<double> g = Matrix<double>.Build.DenseOfArray(grad);
                    Matrix<double> deltax = g.Divide(g.L2Norm()).Multiply(step);

                    x = x - deltax;
                    
                    double target1 = ObjectiveFunc(x.At(0, 0), x.At(1, 0));
                    if(target1 > target)
                    {
                        x = x + deltax;
                        step = step / 10;
                    }
                    else
                    {
                        target = target1;
                    }
                }
                step = step0;
                if(target < 1e-5)
                {
                    Console.WriteLine("Sprendinys {0} {1}", x.At(0, 0), x.At(1, 0));
                    Console.WriteLine("Funkcijos reiksme: {0}", F1(x.At(0, 0), x.At(1, 0)));
                    break;
                }
                else if (i == 9)
                {
                    Console.WriteLine("Tikslumas nepasiektas");
                    Console.WriteLine("Sprendinys {0} {1}", x.At(0, 0), x.At(1, 0));
                    Console.WriteLine("Funkcijos reiksme: {0} {1}", F1(x.At(0, 0), x.At(1, 0))[0, 0], F1(x.At(0, 0), x.At(1, 0))[1, 0]);
                    break;
                }
            }

        }

        static double[,] J4x4(double x1, double x2, double x3, double x4)
        {
            double x11 = 0;
            double x12 = 2;
            double x13 = 0;
            double x14 = 4;

            double x21 = x2;
            double x22 = x1;
            double x23 = 0;
            double x24 = -1;

            double x31 = -6 * x1 - x2;
            double x32 = -1 * x1;
            double x33 = 0;
            double x34 = 9 * Math.Pow(x4, 2);

            double x41 = 0;
            double x42 = -6;
            double x43 = 3;
            double x44 = 2;

            double[,] result = { { x11, x12, x13, x14 }, { x21, x22, x23, x24 }, { x31, x32, x33, x34 }, { x41, x42, x43, x44} };
            return result;
        }

        static double[,] F2(double x1, double x2, double x3, double x4)
        {
            double x11 = 2 * x2 + 4 * x4 + 20;
            double x21 = x1 * x2 - x4 - 14;
            double x31 = -3 * Math.Pow(x1, 2) - x2 * x1 + 3 * Math.Pow(x4, 3) + 277;
            double x41 = 3 * x3 - 6 * x2 + 2 * x4 - 7;

            double[,] result = { { x11 }, { x21 }, { x31 }, { x41 } };
            return result;
        }

        static double[,] Grad2(double x1, double x2, double x3, double x4)
        {
            double[,] jT = TransposeMatrix(J4x4(x1, x2, x3, x4));
            double[,] f = F2(x1, x2, x3, x4);

            double[,] result = MatrixMultiplication(jT, f);

            return result;
        }

        static double ObjectiveFunc2(double x1, double x2, double x3, double x4)
        {
            double[,] f = F2(x1, x2, x3, x4);

            f[0, 0] = Math.Pow(f[0, 0], 2);
            f[1, 0] = Math.Pow(f[1, 0], 2);
            f[2, 0] = Math.Pow(f[2, 0], 2);
            f[3, 0] = Math.Pow(f[3, 0], 2);

            return (f[0, 0] + f[1, 0] + f[2, 0] + f[3, 0]) / 2;
        }

        static void Uzduotis2Lygtis2()
        {
            double[,] xx = { { -5.5 }, { -2.5 }, { 1.5}, { -4.5} };
            Matrix<double> x = Matrix<double>.Build.DenseOfArray(xx);
            double step0 = 0.5;
            double step = step0;
            for (int i = 0; i < 300; i++)
            {
                double[,] grad = Grad2(x.At(0, 0), x.At(1, 0), x.At(2, 0), x.At(3, 0));
                double target = ObjectiveFunc2(x.At(0, 0), x.At(1, 0), x.At(2, 0), x.At(3, 0));
                Console.WriteLine("Iteracija: {0} Tikslumas: {1}", i, target);
                for (int j = 0; j < 100; j++)
                {
                    Matrix<double> g = Matrix<double>.Build.DenseOfArray(grad);
                    Matrix<double> deltax = g.Divide(g.L2Norm()).Multiply(step);

                    x = x - deltax;

                    double target1 = ObjectiveFunc2(x.At(0, 0), x.At(1, 0), x.At(2, 0), x.At(3, 0));
                    if (target1 > target)
                    {
                        x = x + deltax;
                        step = step / 1.31;
                        break;
                    }
                    else
                    {
                        target = target1;
                    }
                    
                    if(target < 1e-5)
                    {
                        Console.WriteLine("Sprendiniai: {0} {1} {2} {3}", x.At(0, 0), x.At(1, 0), x.At(2, 0), x.At(3, 0));
                        double[,] F = F2(x.At(0, 0), x.At(1, 0), x.At(2, 0), x.At(3, 0));
                        Console.WriteLine("Funkcijos reiksmes: {0} {1} {2} {3}", F[0,0], F[1, 0], F[2, 0], F[3, 0]);
                        break;
                    }
                    else if (i == 299)
                    {
                        Console.WriteLine("Tikslumas nepasiektas");
                        Console.WriteLine("Sprendiniai: {0} {1} {2} {3}", x.At(0, 0), x.At(1, 0), x.At(2, 0), x.At(3, 0));
                        double[,] F = F2(x.At(0, 0), x.At(1, 0), x.At(2, 0), x.At(3, 0));
                        Console.WriteLine("Funkcijos reiksmes: {0} {1} {2} {3}", F[0, 0], F[1, 0], F[2, 0], F[3, 0]);
                        break;
                    }
                }
                //step = step0;
            }
        }

#endregion

        #region Uzduotis 3

        static double Distance(double x1, double x2, double y1, double y2)
        {
            return Math.Sqrt(Math.Pow(x2 - x1, 2) + Math.Pow(y2 - y1, 2));
        }

        static double AvarageDistance(double[,] nodes)
        {
            int dCount = 0;
            double sum = 0;
            for(int i = 0; i < nodes.GetLength(0); i++)
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

        static double VectorLength(double[] vector)
        {
            double sum = 0;
            foreach (double item in vector)
            {
                sum += Math.Pow(item, 2);
            }
            return Math.Sqrt(sum);
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

        static double[] Gradient(double[,] nodes)
        {
            List<double> grads = new List<double>();
            for (int i = 0; i < nodes.GetLength(0); i++)
            {
                for (int j = 0; j < nodes.GetLength(1); j++)
                {
                    grads.Add(NodeGradient(i, j, nodes, 1e-5));
                }
            }
            return grads.ToArray();
        }

        static double[] NormalizeGradientVector(double[] vector)
        {
            double vectorLenght = VectorLength(vector);
            double[] newVector = new double[vector.Length];
            for (int i = 0; i < vector.Length; i++)
            {
                newVector[i] = vector[i] / vectorLenght;
            }
            return newVector;
        }

        static void Uzduotis3()
        {
            double result = 0;
            double step = 0.01;
            double[,] nodes = new double[1000, 2];
            
            nodes[0, 0] = 1;
            nodes[0, 1] = 2;
            nodes[1, 0] = 3;
            nodes[1, 1] = 2;
            nodes[2, 0] = 1;
            nodes[2, 1] = -2;
            nodes[3, 0] = 3;
            nodes[3, 1] = -2;
            for (int i = 4; i < 1000; i++)
            {
                nodes[i, 0] = 0;
                nodes[i, 1] = 0;
            }
            
            int iterations = 0;
            int maxIterations = 100;
            string[] values = new string[100];
            for (int i = 0; i < maxIterations; i++)
            {
                double fValue = CostFunction(nodes);
                int count = 0;
                double[] grads = Gradient(nodes);
                grads = NormalizeGradientVector(grads);
                double[,] nodesCopy = NodesCopy(nodes);
                for (int j = 4; j < nodesCopy.GetLength(0); j++)
                {
                    for (int k = 0; k < nodesCopy.GetLength(1); k++)
                    {
                        nodesCopy[j, k] = nodesCopy[j, k] - (step * grads[count]);
                        count++;
                    }
                }
                double fValue1 = CostFunction(nodesCopy);
                
                if(fValue1 < fValue)
                {
                    nodes = nodesCopy;
                    result = fValue1;
                }
                else
                {
                    step = step / 2;
                }
                values[i] = fValue.ToString();
            }

            Console.WriteLine("Taskai: ");
            for (int i = 0; i < nodes.GetLength(0); i++)
            {
                for (int j = 0; j < nodes.GetLength(1); j++)
                {
                    Console.Write(nodes[i, j] + " ");
                }
                Console.WriteLine();
            }

            System.IO.File.WriteAllLines(@"C:\Users\vyten\Desktop\SMA Lab2\values.txt", values);
        }

        #endregion
    }
}
