using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Factorization;
using System.IO;

namespace Pvz1
{
    public partial class Form1 : Form
    {
        const int TASKAI = 10;
        List<Timer> Timerlist = new List<Timer>();

        public Form1()
        {
            InitializeComponent();
            Initialize();
        }

        // ---------------------------------------------- PUSIAUKIRTOS METODAS ----------------------------------------------

        float x1, x2, xtemp; // izoliacijos intervalo pradžia ir galas, vidurio taškas
        int N = 1000; // maksimalus iteracijų skaičius
        int iii; // iteracijos numeris

        Series Fx, X1X2, XMid, F1XY; // naudojama atvaizduoti f-jai, šaknų rėžiams ir vidiniams taškams

        #region uzduotis 1 a

        /// <summary>
        /// Sprendžiama lygtis F(x) = 0
        /// </summary>
        /// <param name="x">funkcijos argumentas</param>
        /// <returns></returns>
        private double F(double x)
        {
            return (Math.Log(Math.E, x) / (Math.Sin(2 * x) + 1.5)) - x / 7;
        }

        static public int order = 8;
        public const int datapoints = 100;
        static double[] y_k = new double[order];    
        static double[] x_k = new double[order];    
        static double[] b = new double[order];
        static double[] yp = new double[datapoints];
        static double[] xp = new double[datapoints];
        static int lowerBound = 2;
        static int upperBound = 10;
        private void button3_Click(object sender, EventArgs e)
        {
            double[] yArr = new double[TASKAI];
            double[] xArr = new double[TASKAI];
            ClearForm(); // išvalomi programos duomenys
            PreparareForm(2, 10, -5, 5);
            Fx = chart1.Series.Add("F(x)");
            Fx.ChartType = SeriesChartType.Line;
            for (double i = 2; i < 10;)
            {
                Fx.Points.AddXY(i, F(i));
                i = i + 0.1;
            }

            for (int i = 0; i < 8; i++)
            {
                x_k[i] = i + 2;
            }
            for (int i = 0; i < 8; i++)
            {
                y_k[i] = F(x_k[i]);
            }
            for (int i = 0; i < 8; i++)
            {
                richTextBox1.AppendText(String.Format("x = {0} y = {1}\n", x_k[i], y_k[i]));
            }
            CalcElements(y_k, order, 1);

            richTextBox1.AppendText("XP's: ");
            for (int i = 0; i < datapoints; i++)
            {
                xp[i] = (double)lowerBound + (double)i * (((double)upperBound - (double)lowerBound) / (double)datapoints);
                yp[i] = Interpolate(xp[i], order);
                //richTextBox1.AppendText(String.Format("x = {0}\n", xp[i]));
            }


            double x = 2;
            double step = 1;
            Fx.BorderWidth = 3;

            X1X2 = chart1.Series.Add("Taskai\n");
            X1X2.ChartType = SeriesChartType.Point;
            X1X2.Color = Color.Red;

            F1XY = chart1.Series.Add("Interp\n");
            F1XY.ChartType = SeriesChartType.Line;
            F1XY.Color = Color.Green;
            F1XY.BorderWidth = 3;

            richTextBox1.AppendText("Step size:   " + step + "\n");
            for (int i = 0; i <= order; i++)
            {
                yArr[i] = F(x);
                xArr[i] = x;
               // richTextBox1.AppendText("x=   " + x + "  F(x)=" + F(x) + "\n");
                X1X2.Points.AddXY(x, F(x));
                x++;
            }

            for (int i = 0; i < datapoints; i++)
            {
                F1XY.Points.AddXY(xp[i], yp[i]);
                richTextBox1.AppendText("x=   " + xp[i] + "  F(x)=" + yp[i] + "\n");
            }
        }



        static void CalcElements(double[] x, int order, int step)
        {
            double[] xx;
            if (order >= 1)
            {
                xx = new double[order];
                for (int i = 0; i < order - 1; i++)
                {
                    xx[i] = (x[i + 1] - x[i]) / (x_k[i + step] - x_k[i]);
                }
                b[step - 1] = x[0];
                CalcElements(xx, order - 1, step + 1);
            }
        }

        static double Interpolate(double xp, int order)
        {
            double tempYp = 0;
            double yp = 0;
            for (int i = 1; i < order; i++)
            {
                tempYp = b[i];
                for (int k = 0; k < i; k++)
                {
                    tempYp = tempYp * (xp - x_k[k]);
                }
                yp = yp + tempYp;
            }
            return b[0] + yp;
        }

#endregion

        #region uzduotis 1 b

        private void button6_Click(object sender, EventArgs e)
        {
            double[] yArr = new double[TASKAI];
            double[] xArr = new double[TASKAI];
            ClearForm(); // išvalomi programos duomenys
            PreparareForm(2, 10, -5, 5);
            iii = 0; // iteraciju skaičius
            Fx = chart1.Series.Add("F(x)");
            Fx.ChartType = SeriesChartType.Line;
            double x = 2;
            for (double i = 2; i < 10;)
            {
                Fx.Points.AddXY(i, F(i));
                i = i + 0.1;
            }

            
            Fx.BorderWidth = 3;

            x = 2;
            X1X2 = chart1.Series.Add("Taskai\n");
            X1X2.ChartType = SeriesChartType.Point;
            X1X2.Color = Color.Red;

            F1XY = chart1.Series.Add("Interp\n");
            F1XY.ChartType = SeriesChartType.Line;
            F1XY.Color = Color.Green;
            F1XY.BorderWidth = 3;

            for (int i = 1; i < order+1; i++)
            {
                x = convX(i);
                y_k[i-1] = F(x);
                x_k[i-1] = x;
                richTextBox1.AppendText("x=   " + x + "  F(x)=" + F(x) + "\n");
                X1X2.Points.AddXY(x, F(x));
            }

            CalcElements(y_k, order, 1);
            for (int i = 0; i < datapoints; i++)
            {
                xp[i] = (double)x_k[0] + (double)i * (((double)x_k[order-1] - (double)x_k[0]) / (double)datapoints);
                richTextBox1.AppendText(String.Format("x = {0}\n", xp[i]));
                yp[i] = Interpolate(xp[i], order);
            }

            for (int i = 0; i < datapoints; i++)
            {
                F1XY.Points.AddXY(xp[i], yp[i]);
                //richTextBox1.AppendText("x=   " + xp[i] + "  F(x)=" + yp[i] + "\n");
            }
        }

        private double convX(int i)
        {
            double a = 2;
            double b = 10;

            double Order = order;
            //richTextBox1.AppendText("order=   " + Order + "\n");
            double mainCalc = Math.Cos((2 * i - 1) * Math.PI / (2 * Order));
            //richTextBox1.AppendText("mainCalc=   " + mainCalc + "\n");

            return 0.5*(a+b) + 0.5*(b-a) * mainCalc;
        }

#endregion

        #region uzduotis 2 a

        private void temp_daugi_Click(object sender, EventArgs e)
        {
            double[] yArr = { -1.2304, -0.5933, 3.27007, 6.3813, 10.9601, 14.7767, 16.7494, 18.6058, 13.7815, 5.93818, 4.33412, 1.82333 };
            double[] xArr = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 };
            order = 12;
            x_k = new double[order];
            y_k = new double[order];
            b = new double[order];

            ClearForm(); // išvalomi programos duomenys
            PreparareForm(0, 13, -5, 20);
            iii = 0; // iteraciju skaičius
            F1XY = chart1.Series.Add("Duota\n");
            F1XY.ChartType = SeriesChartType.Point;
            F1XY.Color = Color.Red;
            F1XY.BorderWidth = 6;

            Fx = chart1.Series.Add("Temp");
            Fx.ChartType = SeriesChartType.Line;
            Fx.BorderWidth = 3;

            for (int i = 0; i < 12; i++)
            {
                x_k[i] = xArr[i];
                y_k[i] = yArr[i];
                F1XY.Points.AddXY(xArr[i], yArr[i]);
            }
            CalcElements(y_k, order, 1);
            for (int i = 0; i < datapoints; i++)
            {
                xp[i] = (double)1 + (double)i * (((double)13 - (double)1) / (double)datapoints);
                yp[i] = Interpolate(xp[i], order);
                //richTextBox1.AppendText(String.Format("x = {0}\n", xp[i]));
            }

            for (int i = 0; i < datapoints; i++)
            {
                Fx.Points.AddXY(xp[i], yp[i]);
                richTextBox1.AppendText("x=   " + xp[i] + "  F(x)=" + yp[i] + "\n");
            }
        }

        #endregion

        #region uzduotis 2 b

        private void spline(object sender, EventArgs e)
        {
            double[] yArr = { -1.2304, -0.5933, 3.27007, 6.3813, 10.9601, 14.7767, 16.7494, 18.6058, 13.7815, 5.93818, 4.33412, 1.82333 };
            double[] xArr = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 };

            ClearForm();
            PreparareForm(0, 13, -5, 20);
            F1XY = chart1.Series.Add("Duota\n");
            F1XY.ChartType = SeriesChartType.Point;
            F1XY.Color = Color.Red;
            F1XY.BorderWidth = 6;

            Fx = chart1.Series.Add("Spline");
            Fx.ChartType = SeriesChartType.Line;
            Fx.BorderWidth = 3;

            for (int i = 0; i < yArr.Length; i++)
            {
                F1XY.Points.AddXY(xArr[i], yArr[i]);
            }

            double[] DY = Akima(xArr, yArr);

            for (int iii = 0; iii < xArr.Length - 1; iii++)
            {
                int nnn = 100;
                double[] xxx = new double[nnn + 1];;
                for (int j = 0; j < xxx.Length; j++)
                {
                    xxx[j] = (xArr[iii] + (xArr[iii + 1] - xArr[iii]) / 100 * j);
                }

                double[] f = new double[xxx.Length];
                for (int i = 0; i < xxx.Length; i++)
                {
                    f[i] = 0;
                }
                Vector<double> fff = Vector<double>.Build.DenseOfArray(f);

                for (int j = 0; j < 2; j++)
                {
                    double[] U, V;
                    double[] subXArr = { xArr[iii], xArr[iii + 1] };
                    Hermite(out U, out V, subXArr, j, xxx);
                    Vector<double> UV = Vector<double>.Build.DenseOfArray(U);
                    Vector<double> VV = Vector<double>.Build.DenseOfArray(V);
                    if(j == 0)
                    {
                        fff = fff + UV.Multiply(yArr[iii]) + VV.Multiply(DY[iii]);
                    }
                    else if(j == 1)
                    {
                        fff = fff + UV.Multiply(yArr[iii + 1]) + VV.Multiply(DY[iii + 1]);
                    }
                }

                for(int i = 0; i <= nnn; i++)
                {
                    Fx.Points.AddXY(xxx[i], fff.ToArray()[i]);
                }
            }
        }

        void Hermite(out double[] U, out double[] V, double[] xArr, int j, double[] x)
        {
            double[] L = Lagrange(xArr, j, x);
            double DL = D_Lagrange(xArr, j, xArr[j]);
            Vector<double> xxx = Vector<double>.Build.DenseOfArray(x);
            Vector<double> LV = Vector<double>.Build.DenseOfArray(L);
            double[] u = new double[x.Length];
            for (int i = 0; i < x.Length; i++)
            {
                u[i] = 1;
            }
            Vector<double> UV = Vector<double>.Build.DenseOfArray(u);
            U = ((UV - xxx.Subtract(xArr[j]).Multiply(2 * DL)).PointwiseMultiply(LV.PointwisePower(2))).ToArray();
            V = xxx.Subtract(xArr[j]).PointwiseMultiply(LV.PointwisePower(2)).ToArray();
        }

        double[] Lagrange(double[] xArr, int j, double[] x)
        {
            double[] l = new double[x.Length];
            for (int i = 0; i < x.Length; i++)
            {
                l[i] = 1;
            }
            
            Vector<double> L = Vector<double>.Build.DenseOfArray(l);
            //richTextBox1.AppendText("L Length: " + L.Count + "\n");
            Vector<double> xxx = Vector<double>.Build.DenseOfArray(x);
            //richTextBox1.AppendText("xxx Length: " + xxx.Count + "\n");
            for (int k = 0; k < xArr.Length; k++)
            {
                if(k != j)
                {
                    L = L.PointwiseMultiply(xxx.Subtract(xArr[k])).Divide(xArr[j] - xArr[k]);
                }
            }
            return L.ToArray();
        }

        double D_Lagrange(double[] xArr, int j, double x)
        {
            double DL = 0;
            for (int i = 0; i < xArr.Length; i++)
            {
                if (i == j) continue;

                double Lds = 1;

                for (int k = 0; k < xArr.Length; k++)
                {
                    if(k != j && k != i)
                    {
                        Lds = Lds * (x - xArr[k]);
                    }
                }
                DL = DL + Lds;
            }

            double Ldv = 1;

            for (int k = 0; k < xArr.Length; k++)
            {
                if(k != j)
                {
                    Ldv = Ldv * (xArr[j] - xArr[k]);
                }
            }
            DL = DL / Ldv;
            return DL;
        }

        double[] Akima(double[] xArr, double[] yArr)
        {
            double[] DY = new double[xArr.Length];
            for (int i = 0; i < xArr.Length; i++)
            {
                if(i == 0)
                {
                    double xim1 = xArr[0];
                    double xi = xArr[1];
                    double xip1 = xArr[2];
                    double yim1 = yArr[0];
                    double yi = yArr[1];
                    double yip1 = yArr[2];
                    DY[i] = supportAkima(xim1, xi, xim1, xip1, yi, yim1, yip1);
                }
                else if (i == xArr.Length - 1)
                {
                    double xim1 = xArr[xArr.Length - 1 - 2];
                    double xi = xArr[xArr.Length - 1 - 1];
                    double xip1 = xArr[xArr.Length - 1];
                    double yim1 = yArr[yArr.Length - 1 - 2];
                    double yi = yArr[yArr.Length - 1 - 1];
                    double yip1 = yArr[yArr.Length - 1];
                    DY[xArr.Length - 1] = supportAkima(xip1, xi, xim1, xip1, yi, yim1, yip1);
                }
                else
                {
                    double xim1 = xArr[i - 1];
                    double xi = xArr[i];
                    double xip1 = xArr[i + 1];
                    double yim1 = yArr[i - 1];
                    double yi = yArr[i];
                    double yip1 = yArr[i + 1];
                    DY[i] = supportAkima(xi, xi, xim1, xip1, yi, yim1, yip1);
                }
            }
            return DY;
        }

        double supportAkima(double x, double xi, double xim1, double xip1, double yi, double yim1, double yip1)
        {
            return (2 * x - xi - xip1) / ((xim1 - xi) * (xim1 - xip1)) * yim1 + (2 * x - xim1 - xip1) / ((xi - xim1) * (xi - xip1)) * yi + (2 * x - xim1 - xi) / ((xip1 - xim1) * (xip1 - xi)) * yip1;
        }
        #endregion

        #region 3 uzduotis

        /// <summary>
        /// 10
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button5_Click_1(object sender, EventArgs e)
        {
            string xtxt = File.ReadAllText(@"..\..\..\X.txt");
            string ytxt = File.ReadAllText(@"..\..\..\Y.txt");
            double[] xArr = Array.ConvertAll(xtxt.Split(','), Double.Parse);
            double[] yArr = Array.ConvertAll(ytxt.Split(','), Double.Parse);

            ClearForm();
            PreparareForm((float)20.9, (float)28.3, (float)55.6, (float)58.1);
            chart1.ChartAreas[0].AxisX.LabelStyle.Format = "#.##";
            chart1.ChartAreas[0].AxisY.LabelStyle.Format = "#.##";
            F1XY = chart1.Series.Add("Duota\n");
            F1XY.ChartType = SeriesChartType.Point;
            F1XY.Color = Color.Red;
            F1XY.BorderWidth = 6;

            Fx = chart1.Series.Add("Spline");
            Fx.ChartType = SeriesChartType.Line;
            Fx.BorderWidth = 3;

            richTextBox1.AppendText("Minimum X: " + xArr.Min() + "\n");
            richTextBox1.AppendText("Maximum X: " + xArr.Max() + "\n");
            richTextBox1.AppendText("Minimum Y: " + yArr.Min() + "\n");
            richTextBox1.AppendText("Maximum Y: " + yArr.Max() + "\n");
            richTextBox1.AppendText("x.Length = " + xArr.Length + " y.Length = " + yArr.Length + "\n");
            for (int i = 0; i < xArr.Length; i++)
            {
                richTextBox1.AppendText("x = " + xArr[i] + " y = " + yArr[i] + "\n");
            }

            double[] tempX = new double[10];
            double[] tempY = new double[10];
            int index = 0;
            for (int i = 0; i < xArr.Length; i += 54)
            {
                if(index > 9)
                {
                    break;
                }
                tempX[index] = xArr[i];
                tempY[index] = yArr[i];
                index++;
            }
            xArr = tempX;
            yArr = tempY;

            for (int i = 0; i < yArr.Length; i++)
            {
                F1XY.Points.AddXY(xArr[i], yArr[i]);
            }

            double[] DY = Akima(xArr, yArr);

            for (int iii = 0; iii < xArr.Length - 1; iii++)
            {
                int nnn = 100;
                double[] xxx = new double[nnn + 1]; ;
                for (int j = 0; j < xxx.Length; j++)
                {
                    xxx[j] = (xArr[iii] + (xArr[iii + 1] - xArr[iii]) / 100 * j);
                }

                double[] f = new double[xxx.Length];
                for (int i = 0; i < xxx.Length; i++)
                {
                    f[i] = 0;
                }
                Vector<double> fff = Vector<double>.Build.DenseOfArray(f);

                for (int j = 0; j < 2; j++)
                {
                    double[] U, V;
                    double[] subXArr = { xArr[iii], xArr[iii + 1] };
                    Hermite(out U, out V, subXArr, j, xxx);
                    Vector<double> UV = Vector<double>.Build.DenseOfArray(U);
                    Vector<double> VV = Vector<double>.Build.DenseOfArray(V);
                    if (j == 0)
                    {
                        fff = fff + UV.Multiply(yArr[iii]) + VV.Multiply(DY[iii]);
                    }
                    else if (j == 1)
                    {
                        fff = fff + UV.Multiply(yArr[iii + 1]) + VV.Multiply(DY[iii + 1]);
                    }
                }

                for (int i = 0; i <= nnn; i++)
                {
                    if (xxx[i] == null || fff[i] == null || double.IsInfinity(fff[i]) || double.IsNaN(fff[i]))
                    {
                        continue;
                    }
                    //richTextBox1.AppendText("x = " + xxx[i] + " y = " + fff[i] + "\n");
                    Fx.Points.AddXY(xxx[i], fff.ToArray()[i]);
                }
            }
        }
        /// <summary>
        /// 20
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button7_Click(object sender, EventArgs e)
        {
            string xtxt = File.ReadAllText(@"..\..\..\X.txt");
            string ytxt = File.ReadAllText(@"..\..\..\Y.txt");
            double[] xArr = Array.ConvertAll(xtxt.Split(','), Double.Parse);
            double[] yArr = Array.ConvertAll(ytxt.Split(','), Double.Parse);

            ClearForm();
            PreparareForm((float)20.9, (float)28.3, (float)55.6, (float)58.1);
            chart1.ChartAreas[0].AxisX.LabelStyle.Format = "#.##";
            chart1.ChartAreas[0].AxisY.LabelStyle.Format = "#.##";
            F1XY = chart1.Series.Add("Duota\n");
            F1XY.ChartType = SeriesChartType.Point;
            F1XY.Color = Color.Red;
            F1XY.BorderWidth = 6;

            Fx = chart1.Series.Add("Spline");
            Fx.ChartType = SeriesChartType.Line;
            Fx.BorderWidth = 3;

            richTextBox1.AppendText("Minimum X: " + xArr.Min() + "\n");
            richTextBox1.AppendText("Maximum X: " + xArr.Max() + "\n");
            richTextBox1.AppendText("Minimum Y: " + yArr.Min() + "\n");
            richTextBox1.AppendText("Maximum Y: " + yArr.Max() + "\n");
            richTextBox1.AppendText("x.Length = " + xArr.Length + " y.Length = " + yArr.Length + "\n");
            for (int i = 0; i < xArr.Length; i++)
            {
                richTextBox1.AppendText("x = " + xArr[i] + " y = " + yArr[i] + "\n");
            }

            double[] tempX = new double[20];
            double[] tempY = new double[20];
            int index = 0;
            for (int i = 0; i < xArr.Length; i += 27)
            {
                if (index > 19)
                {
                    break;
                }
                tempX[index] = xArr[i];
                tempY[index] = yArr[i];
                index++;
            }
            xArr = tempX;
            yArr = tempY;

            for (int i = 0; i < yArr.Length; i++)
            {
                F1XY.Points.AddXY(xArr[i], yArr[i]);
            }

            double[] DY = Akima(xArr, yArr);

            for (int iii = 0; iii < xArr.Length - 1; iii++)
            {
                int nnn = 100;
                double[] xxx = new double[nnn + 1]; ;
                for (int j = 0; j < xxx.Length; j++)
                {
                    xxx[j] = (xArr[iii] + (xArr[iii + 1] - xArr[iii]) / 100 * j);
                }

                double[] f = new double[xxx.Length];
                for (int i = 0; i < xxx.Length; i++)
                {
                    f[i] = 0;
                }
                Vector<double> fff = Vector<double>.Build.DenseOfArray(f);

                for (int j = 0; j < 2; j++)
                {
                    double[] U, V;
                    double[] subXArr = { xArr[iii], xArr[iii + 1] };
                    Hermite(out U, out V, subXArr, j, xxx);
                    Vector<double> UV = Vector<double>.Build.DenseOfArray(U);
                    Vector<double> VV = Vector<double>.Build.DenseOfArray(V);
                    if (j == 0)
                    {
                        fff = fff + UV.Multiply(yArr[iii]) + VV.Multiply(DY[iii]);
                    }
                    else if (j == 1)
                    {
                        fff = fff + UV.Multiply(yArr[iii + 1]) + VV.Multiply(DY[iii + 1]);
                    }
                }

                for (int i = 0; i <= nnn; i++)
                {
                    if (xxx[i] == null || fff[i] == null || double.IsInfinity(fff[i]) || double.IsNaN(fff[i]))
                    {
                        continue;
                    }
                    //richTextBox1.AppendText("x = " + xxx[i] + " y = " + fff[i] + "\n");
                    Fx.Points.AddXY(xxx[i], fff.ToArray()[i]);
                }
            }
        }
        /// <summary>
        /// 50
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button8_Click(object sender, EventArgs e)
        {
            string xtxt = File.ReadAllText(@"..\..\..\X.txt");
            string ytxt = File.ReadAllText(@"..\..\..\Y.txt");
            double[] xArr = Array.ConvertAll(xtxt.Split(','), Double.Parse);
            double[] yArr = Array.ConvertAll(ytxt.Split(','), Double.Parse);

            ClearForm();
            PreparareForm((float)20.9, (float)28.3, (float)55.6, (float)58.1);
            chart1.ChartAreas[0].AxisX.LabelStyle.Format = "#.##";
            chart1.ChartAreas[0].AxisY.LabelStyle.Format = "#.##";
            F1XY = chart1.Series.Add("Duota\n");
            F1XY.ChartType = SeriesChartType.Point;
            F1XY.Color = Color.Red;
            F1XY.BorderWidth = 6;

            Fx = chart1.Series.Add("Spline");
            Fx.ChartType = SeriesChartType.Line;
            Fx.BorderWidth = 3;

            richTextBox1.AppendText("Minimum X: " + xArr.Min() + "\n");
            richTextBox1.AppendText("Maximum X: " + xArr.Max() + "\n");
            richTextBox1.AppendText("Minimum Y: " + yArr.Min() + "\n");
            richTextBox1.AppendText("Maximum Y: " + yArr.Max() + "\n");
            richTextBox1.AppendText("x.Length = " + xArr.Length + " y.Length = " + yArr.Length + "\n");
            for (int i = 0; i < xArr.Length; i++)
            {
                richTextBox1.AppendText("x = " + xArr[i] + " y = " + yArr[i] + "\n");
            }

            double[] tempX = new double[50];
            double[] tempY = new double[50];
            int index = 0;
            for (int i = 0; i < xArr.Length; i += 10)
            {
                if (index > 49)
                {
                    break;
                }
                tempX[index] = xArr[i];
                tempY[index] = yArr[i];
                index++;
            }
            xArr = tempX;
            yArr = tempY;

            for (int i = 0; i < yArr.Length; i++)
            {
                F1XY.Points.AddXY(xArr[i], yArr[i]);
            }

            double[] DY = Akima(xArr, yArr);

            for (int iii = 0; iii < xArr.Length - 1; iii++)
            {
                int nnn = 100;
                double[] xxx = new double[nnn + 1]; ;
                for (int j = 0; j < xxx.Length; j++)
                {
                    xxx[j] = (xArr[iii] + (xArr[iii + 1] - xArr[iii]) / 100 * j);
                }

                double[] f = new double[xxx.Length];
                for (int i = 0; i < xxx.Length; i++)
                {
                    f[i] = 0;
                }
                Vector<double> fff = Vector<double>.Build.DenseOfArray(f);

                for (int j = 0; j < 2; j++)
                {
                    double[] U, V;
                    double[] subXArr = { xArr[iii], xArr[iii + 1] };
                    Hermite(out U, out V, subXArr, j, xxx);
                    Vector<double> UV = Vector<double>.Build.DenseOfArray(U);
                    Vector<double> VV = Vector<double>.Build.DenseOfArray(V);
                    if (j == 0)
                    {
                        fff = fff + UV.Multiply(yArr[iii]) + VV.Multiply(DY[iii]);
                    }
                    else if (j == 1)
                    {
                        fff = fff + UV.Multiply(yArr[iii + 1]) + VV.Multiply(DY[iii + 1]);
                    }
                }

                for (int i = 0; i <= nnn; i++)
                {
                    if (xxx[i] == null || fff[i] == null || double.IsInfinity(fff[i]) || double.IsNaN(fff[i]))
                    {
                        continue;
                    }
                    //richTextBox1.AppendText("x = " + xxx[i] + " y = " + fff[i] + "\n");
                    Fx.Points.AddXY(xxx[i], fff.ToArray()[i]);
                }
            }
        }
        /// <summary>
        /// 100
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button9_Click(object sender, EventArgs e)
        {
            string xtxt = File.ReadAllText(@"..\..\..\X.txt");
            string ytxt = File.ReadAllText(@"..\..\..\Y.txt");
            double[] xArr = Array.ConvertAll(xtxt.Split(','), Double.Parse);
            double[] yArr = Array.ConvertAll(ytxt.Split(','), Double.Parse);

            ClearForm();
            PreparareForm((float)20.9, (float)28.3, (float)55.6, (float)58.1);
            chart1.ChartAreas[0].AxisX.LabelStyle.Format = "#.##";
            chart1.ChartAreas[0].AxisY.LabelStyle.Format = "#.##";
            F1XY = chart1.Series.Add("Duota\n");
            F1XY.ChartType = SeriesChartType.Point;
            F1XY.Color = Color.Red;
            F1XY.BorderWidth = 6;

            Fx = chart1.Series.Add("Spline");
            Fx.ChartType = SeriesChartType.Line;
            Fx.BorderWidth = 3;

            richTextBox1.AppendText("Minimum X: " + xArr.Min() + "\n");
            richTextBox1.AppendText("Maximum X: " + xArr.Max() + "\n");
            richTextBox1.AppendText("Minimum Y: " + yArr.Min() + "\n");
            richTextBox1.AppendText("Maximum Y: " + yArr.Max() + "\n");
            richTextBox1.AppendText("x.Length = " + xArr.Length + " y.Length = " + yArr.Length + "\n");
            for (int i = 0; i < xArr.Length; i++)
            {
                richTextBox1.AppendText("x = " + xArr[i] + " y = " + yArr[i] + "\n");
            }

            double[] tempX = new double[100];
            double[] tempY = new double[100];
            int index = 0;
            for (int i = 0; i < xArr.Length; i += 5)
            {
                if (index > 99)
                {
                    break;
                }
                tempX[index] = xArr[i];
                tempY[index] = yArr[i];
                index++;
            }
            xArr = tempX;
            yArr = tempY;

            for (int i = 0; i < yArr.Length; i++)
            {
                F1XY.Points.AddXY(xArr[i], yArr[i]);
            }

            double[] DY = Akima(xArr, yArr);

            for (int iii = 0; iii < xArr.Length - 1; iii++)
            {
                int nnn = 100;
                double[] xxx = new double[nnn + 1]; ;
                for (int j = 0; j < xxx.Length; j++)
                {
                    xxx[j] = (xArr[iii] + (xArr[iii + 1] - xArr[iii]) / 100 * j);
                }

                double[] f = new double[xxx.Length];
                for (int i = 0; i < xxx.Length; i++)
                {
                    f[i] = 0;
                }
                Vector<double> fff = Vector<double>.Build.DenseOfArray(f);

                for (int j = 0; j < 2; j++)
                {
                    double[] U, V;
                    double[] subXArr = { xArr[iii], xArr[iii + 1] };
                    Hermite(out U, out V, subXArr, j, xxx);
                    Vector<double> UV = Vector<double>.Build.DenseOfArray(U);
                    Vector<double> VV = Vector<double>.Build.DenseOfArray(V);
                    if (j == 0)
                    {
                        fff = fff + UV.Multiply(yArr[iii]) + VV.Multiply(DY[iii]);
                    }
                    else if (j == 1)
                    {
                        fff = fff + UV.Multiply(yArr[iii + 1]) + VV.Multiply(DY[iii + 1]);
                    }
                }

                for (int i = 0; i <= nnn; i++)
                {
                    if (xxx[i] == null || fff[i] == null || double.IsInfinity(fff[i]) || double.IsNaN(fff[i]))
                    {
                        continue;
                    }
                    //richTextBox1.AppendText("x = " + xxx[i] + " y = " + fff[i] + "\n");
                    Fx.Points.AddXY(xxx[i], fff.ToArray()[i]);
                }
            }
        }

        private void Button10_Click(object sender, EventArgs e)
        {
            string xtxt = File.ReadAllText(@"..\..\..\X.txt");
            string ytxt = File.ReadAllText(@"..\..\..\Y.txt");
            double[] xArr = Array.ConvertAll(xtxt.Split(','), Double.Parse);
            double[] yArr = Array.ConvertAll(ytxt.Split(','), Double.Parse);

            ClearForm();
            PreparareForm((float)20.9, (float)28.3, (float)55.6, (float)58.1);
            chart1.ChartAreas[0].AxisX.LabelStyle.Format = "#.##";
            chart1.ChartAreas[0].AxisY.LabelStyle.Format = "#.##";
            F1XY = chart1.Series.Add("Duota\n");
            F1XY.ChartType = SeriesChartType.Point;
            F1XY.Color = Color.Red;
            F1XY.BorderWidth = 6;

            Fx = chart1.Series.Add("Spline");
            Fx.ChartType = SeriesChartType.Line;
            Fx.BorderWidth = 3;

            richTextBox1.AppendText("Minimum X: " + xArr.Min() + "\n");
            richTextBox1.AppendText("Maximum X: " + xArr.Max() + "\n");
            richTextBox1.AppendText("Minimum Y: " + yArr.Min() + "\n");
            richTextBox1.AppendText("Maximum Y: " + yArr.Max() + "\n");
            richTextBox1.AppendText("x.Length = " + xArr.Length + " y.Length = " + yArr.Length + "\n");
            for (int i = 0; i < xArr.Length; i++)
            {
                richTextBox1.AppendText("x = " + xArr[i] + " y = " + yArr[i] + "\n");
            }

            for (int i = 0; i < yArr.Length; i++)
            {
                F1XY.Points.AddXY(xArr[i], yArr[i]);
            }

            double[] DY = Akima(xArr, yArr);

            for (int iii = 0; iii < xArr.Length - 1; iii++)
            {
                int nnn = 100;
                double[] xxx = new double[nnn + 1]; ;
                for (int j = 0; j < xxx.Length; j++)
                {
                    xxx[j] = (xArr[iii] + (xArr[iii + 1] - xArr[iii]) / 100 * j);
                }

                double[] f = new double[xxx.Length];
                for (int i = 0; i < xxx.Length; i++)
                {
                    f[i] = 0;
                }
                Vector<double> fff = Vector<double>.Build.DenseOfArray(f);

                for (int j = 0; j < 2; j++)
                {
                    double[] U, V;
                    double[] subXArr = { xArr[iii], xArr[iii + 1] };
                    Hermite(out U, out V, subXArr, j, xxx);
                    Vector<double> UV = Vector<double>.Build.DenseOfArray(U);
                    Vector<double> VV = Vector<double>.Build.DenseOfArray(V);
                    if (j == 0)
                    {
                        fff = fff + UV.Multiply(yArr[iii]) + VV.Multiply(DY[iii]);
                    }
                    else if (j == 1)
                    {
                        fff = fff + UV.Multiply(yArr[iii + 1]) + VV.Multiply(DY[iii + 1]);
                    }
                }

                for (int i = 0; i <= nnn; i++)
                {
                    if (xxx[i] == null || fff[i] == null || double.IsInfinity(fff[i]) || double.IsNaN(fff[i]))
                    {
                        continue;
                    }
                    //richTextBox1.AppendText("x = " + xxx[i] + " y = " + fff[i] + "\n");
                    Fx.Points.AddXY(xxx[i], fff.ToArray()[i]);
                }
            }
        }

        #endregion

        /// <summary>
        /// timer2 iteracijoje atliekami veiksmai
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer2_Tick(object sender, EventArgs e)
        {
            xtemp = (x1 + x2) / 2; // apskaiciuojamas vidurinis taskas


            if (Math.Abs(F(xtemp)) > 1e-6 & iii <= N)
            // tikrinama salyga, ar funkcijos absoliuti reiksme daugiau uz nustatyta (norima) 
            // tiksluma ir nevirsytas maksimalus iteraciju skaicius
            {
                X1X2.Points.Clear();
                XMid.Points.Clear();

                X1X2.Points.AddXY(x1, 0);
                X1X2.Points.AddXY(x2, 0);
                XMid.Points.AddXY(xtemp, 0);

                richTextBox1.AppendText(String.Format(" {0,6:d}   {1,12:f7}  {2,12:f7} {3,12:f7} {4,12:f7} {5,12:f7} {6,12:f7}\n",
                    iii, xtemp, F(xtemp), x1, x2, F(x1), F(x2)));
                if (Math.Sign((double)F(x1)) != Math.Sign((double)F(xtemp)))
                {
                    x2 = xtemp;
                }
                else
                {
                    x1 = xtemp;
                }
                iii = iii + 1;

            }
            else
            // skaiciavimai stabdomi
            {
                richTextBox1.AppendText("Skaičiavimai baigti");
                timer2.Stop();
            }
        }


        // ---------------------------------------------- PARAMETRINĖS FUNKCIJOS ----------------------------------------------

        List<PointF> data = new List<PointF>(); 
        Series S1;

        /// <summary>
        /// Parametrinis interpoliavimas
        /// </summary>
        private void button5_Click(object sender, EventArgs e)
        {
            ClearForm(); // išvalomi programos duomenys
            PreparareForm(-10, 10, -10, 10);
            data.Clear();
            // apskaičiuojamos funkcijos reikšmės
            for (int i = 0; i < 400; i++)
            {
                float x = i / 50f * (float)(Math.Sin(2*i / 10f));
                float y = i / 50f * (float)(Math.Sin(i / 10f));
                data.Add(new PointF(x, y));
            }
            S1 = chart1.Series.Add("S1");
            S1.BorderWidth = 9;
            S1.ChartType = SeriesChartType.Line;
          
            timer3.Enabled = true;
            timer3.Interval = 15;
            timer3.Start();
        }

        private void timer3_Tick(object sender, EventArgs e)
        {
            Series S1 = chart1.Series[0];
            int pointsSoFar = S1.Points.Count;
            if (pointsSoFar < data.Count)
            {
                S1.Points.AddXY(data[pointsSoFar].X, data[pointsSoFar].Y);
            }
            else
            {
                timer1.Stop();
            }
        }

        // ---------------------------------------------- TIESINĖ ALGEBRA ----------------------------------------------

        /// <summary>
        /// Tiesine algebra (naudojama MathNet)
        /// </summary>
        private void button2_Click(object sender, EventArgs e)
        {
            ClearForm();

            double[,] x = { { 1, 2, 3 }, { 3, 4, 5 }, { 6, 5, 8 } };
            // iš masyvo sugeneruoja matricą, is matricos išskiria eilutę - suformuoja vektorių
            Matrix<double> m = Matrix<double>.Build.DenseOfArray(x);
            Vector<double> v = m.Row(1);
            richTextBox1.AppendText("\nMatrica m:\n");
            richTextBox1.AppendText(m.ToString());

            richTextBox1.AppendText("\nVektorius v:\n");
            richTextBox1.AppendText(v.ToString());

            richTextBox1.AppendText("\ntranspose(m):\n");
            richTextBox1.AppendText(m.Transpose().ToString());

            Matrix<double> vm = v.ToRowMatrix();
            richTextBox1.AppendText("\nvm = v' - toRowMatrix()\n");
            richTextBox1.AppendText(vm.ToString());

            Vector<double> v1 = m * v;
            richTextBox1.AppendText("\nv1 = m * v\n");
            richTextBox1.AppendText(v1.ToString());
            richTextBox1.AppendText("\nmin(v1)\n");
            richTextBox1.AppendText(v1.Min().ToString());

            Matrix<double> m1 = m.Inverse();
            richTextBox1.AppendText("\ninverse(m)\n");
            richTextBox1.AppendText(m1.ToString());

            richTextBox1.AppendText("\ndet(m)\n");
            richTextBox1.AppendText(m.Determinant().ToString());

            // you must add reference to assembly system.Numerics
            Evd<double> eigenv = m.Evd();
            richTextBox1.AppendText("\neigenvalues(m)\n");
            richTextBox1.AppendText(eigenv.EigenValues.ToString());
            
            LU<double> LUanswer = m.LU();
            richTextBox1.AppendText("\nMatricos M LU skaida\n");
            richTextBox1.AppendText("\nMatrica L:\n");
            richTextBox1.AppendText(LUanswer.L.ToString());
            richTextBox1.AppendText("\nMatrica U:\n");
            richTextBox1.AppendText(LUanswer.U.ToString());
            
            QR<double> QRanswer = m.QR();
            richTextBox1.AppendText("\nMatricos M QR skaida\n");
            richTextBox1.AppendText("\nMatrica Q:\n");
            richTextBox1.AppendText(QRanswer.Q.ToString());
            richTextBox1.AppendText("\nMatrica R:\n");
            richTextBox1.AppendText(QRanswer.R.ToString());

            Vector<double> v3 = m.Solve(v);
            richTextBox1.AppendText("\nm*v3 = v sprendziama QR metodu\n");
            richTextBox1.AppendText(v3.ToString());
            richTextBox1.AppendText("Patikrinimas\n");
            richTextBox1.AppendText((m * v3 - v).ToString());
            
        }
        

        // ---------------------------------------------- KITI METODAI ----------------------------------------------

        /// <summary>
        /// Uždaroma programa
        /// </summary>
        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }
        
        /// <summary>
        /// Išvalomas grafikas ir consolė
        /// </summary>
        private void button4_Click(object sender, EventArgs e)
        {
            ClearForm();
        }
        

        public void ClearForm()
        {
            richTextBox1.Clear(); // isvalomas richTextBox1
            // sustabdomi timeriai jei tokiu yra
            foreach (var timer in Timerlist)
            {
                timer.Stop();
            }

            // isvalomos visos nubreztos kreives
            chart1.Series.Clear();
        }
    }
}
