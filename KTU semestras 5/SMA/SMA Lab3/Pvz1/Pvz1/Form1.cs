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
        double a4 = 0, a3 = 0, a2 = 0, a1 = 0, a0 = 0;

        Series Fx, X1X2, XMid, F1XY; // naudojama atvaizduoti f-jai, šaknų rėžiams ir vidiniams taškams


        /// <summary>
        /// Sprendžiama lygtis F(x) = 0
        /// </summary>
        /// <param name="x">funkcijos argumentas</param>
        /// <returns></returns>
        private double F(double x)
        {
            return (Math.Exp(-Math.Pow(x,2)) * Math.Sin(Math.Pow(x,2)) * (x - 3));
        }

        private double F1(double x, double[] A)
        {

            double ans = 0;

            for (int i = 0; i < TASKAI; i++)
            {
                ans += A[TASKAI - i -1] * Math.Pow(x, i);
            }
            return ans; 
        }

        private double F2(double x, double[] A)
        {

            double ans = 0;

            for (int i = 0; i < A.Length; i++)
            {
                ans += A[A.Length - i - 1] * Math.Pow(x, i);
            }
            return ans;
        }


        // Mygtukas "Pusiaukirtos metodas" - ieškoma šaknies, ir vizualizuojamas paieškos procesas
        private void button3_Click(object sender, EventArgs e)
        {
            double[] yArr = new double[TASKAI];
            double[] xArr = new double[TASKAI];
            ClearForm(); // išvalomi programos duomenys
            PreparareForm(-3, 2, -5, 5);
            iii = 0; // iteraciju skaičius
            Fx = chart1.Series.Add("F(x)");
            Fx.ChartType = SeriesChartType.Line;
            double x = -3;
            double step = 5.0/TASKAI;
            while(x<3)
            {
                Fx.Points.AddXY(x, F(x));
                x = x + 0.1;
            }
            Fx.BorderWidth = 3;
            
            x = -3;
            X1X2 = chart1.Series.Add("Taskai\n");
            X1X2.ChartType = SeriesChartType.Point;
            X1X2.Color = Color.Red;

            F1XY = chart1.Series.Add("Interp\n");
            F1XY.ChartType = SeriesChartType.Line;
            F1XY.Color = Color.Green;
            F1XY.BorderWidth = 3;

            richTextBox1.AppendText("Step size:   " + step + "\n");
            for (int i = 0; i < TASKAI; i++)
            {
                x = x + step;
                yArr[i] = F(x);
                xArr[i] = x;
               // richTextBox1.AppendText("x=   " + x + "  F(x)=" + F(x) + "\n");
                X1X2.Points.AddXY(x, F(x));
            }

            Matrix<double> coffs = Matrix<double>.Build.DenseOfArray(getCoffArr(xArr));
           // richTextBox1.AppendText("kofai prie a  \n"+coffs.ToMatrixString() + "\n");
            Vector<double> y = CreateVector.DenseOfArray(yArr);
            //richTextBox1.AppendText("y: \n" + y.ToString() + "\n");
            Vector<double> ans = coffs.Solve(y);
            //richTextBox1.AppendText("gauti a: \n" + ans.ToString() + "\n");

            double[] A = ans.ToArray();

            //richTextBox1.AppendText(ans.ToString());

            x = -3;

            while (x < 3)
            {
                F1XY.Points.AddXY(x, F1(x, A));
                x = x + 0.1;
            }
        }

        public double[] getCoffs(double x)
        {
            double[] coffs = new double[TASKAI];

            for (int i = 0; i < TASKAI; i++)
            {
                coffs[i] = Math.Pow(x, TASKAI-1-i);
            }

            return coffs;
        }

        public double[,] getCoffArr(double[] xArr)
        {
            double[,] arr= new double[xArr.Length, xArr.Length];
            double[] xCoff;

            for (int i = 0; i < xArr.Length; i++)
            {
                xCoff = getCoffs(xArr[i]);
                for (int j = 0; j < xCoff.Length; j++)
                {
                    arr[i, j] = xCoff[j];
                }
            }


            return arr;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            double[] yArr = new double[TASKAI];
            double[] xArr = new double[TASKAI];
            ClearForm(); // išvalomi programos duomenys
            PreparareForm(-3, 2, -5, 5);
            iii = 0; // iteraciju skaičius
            Fx = chart1.Series.Add("F(x)");
            Fx.ChartType = SeriesChartType.Line;
            double x = -3;
            while (x < 3)
            {
                Fx.Points.AddXY(x, F(x));
                x = x + 0.1;
            }
            Fx.BorderWidth = 3;

            x = -3;
            X1X2 = chart1.Series.Add("Taskai\n");
            X1X2.ChartType = SeriesChartType.Point;
            X1X2.Color = Color.Red;

            F1XY = chart1.Series.Add("Interp\n");
            F1XY.ChartType = SeriesChartType.Line;
            F1XY.Color = Color.Green;
            F1XY.BorderWidth = 3;

            for (int i = 1; i < TASKAI+1; i++)
            {
                
                x = convX(i);
                yArr[i-1] = F(x);
                xArr[i-1] = x;
                richTextBox1.AppendText("x=   " + x + "  F(x)=" + F(x) + "\n");
                X1X2.Points.AddXY(x, F(x));
            }

            Matrix<double> coffs = Matrix<double>.Build.DenseOfArray(getCoffArr(xArr));
            //richTextBox1.AppendText("kofai prie a  \n"+coffs.ToMatrixString() + "\n");
            Vector<double> y = CreateVector.DenseOfArray(yArr);
            //richTextBox1.AppendText("y: \n" + y.ToString() + "\n");
            Vector<double> ans = coffs.Solve(y);
            //richTextBox1.AppendText("gauti a: \n" + ans.ToString() + "\n");

            double[] A = ans.ToArray();

            //richTextBox1.AppendText(ans.ToString());

            x = -3;

            while (x < 3)
            {
                F1XY.Points.AddXY(x, F1(x, A));
                x = x + 0.1;
            }
        }

        private double convX(int i)
        {
            double a = -3;
            double b = 2;

            return 0.5*(a+b) + 0.5*(b-a)*Math.Cos((2*i -1) * Math.PI / (2*TASKAI));
        }

        private void temp_daugi_Click(object sender, EventArgs e)
        {
            double[] yArr = { 0.63296, 1.2877, 4.43475, 7.49224, 9.22946, 14.4113, 14.6247, 13.1831, 12.4297, 9.71576, /*5.02944,*/ /*0.68251*/ };
            double[] xArr = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, /*11, 12*/ };
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

            Matrix<double> coffs = Matrix<double>.Build.DenseOfArray(getCoffArr(xArr));
             richTextBox1.AppendText("kofai prie a  \n"+coffs.ColumnCount + "\n" + "xArr.Lenght = " + xArr.Length);
            Vector<double> y = CreateVector.DenseOfArray(yArr);
            richTextBox1.AppendText("y: \n" + y.ToString() + "\n");
            Vector<double> ans = coffs.Solve(y);
            richTextBox1.AppendText("gauti a: \n" + ans.ToString() + "\n");

            double[] A = ans.ToArray();

            //richTextBox1.AppendText(ans.ToString());

            for (int i = 1; i < yArr.Length; i++)
            {
                richTextBox1.AppendText(F2(i, A) + "\n");
            }

            double x = 0;

            while (x < 13)
            {
                Fx.Points.AddXY(x, F2(x, A));
                x = x + 0.1;
            }

            for (int i = 0; i < yArr.Length; i++)
            {
                F1XY.Points.AddXY(i + 1, yArr[i]);
            }

        }

        private void spline(object sender, EventArgs e)
        {
            double[] yArr = { 0.63296, 1.2877, 4.43475, 7.49224, 9.22946, 14.4113, 14.6247, 13.1831, 12.4297, 9.71576, 5.02944, 0.68251 };
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
                F1XY.Points.AddXY(i + 1, yArr[i]);
            }

            double[,] splineMat = createSplineMatrix(xArr);

            richTextBox1.AppendText("\n");


            for (int i = 0; i < xArr.Length; i++)
            {
                for (int j = 0; j < xArr.Length; j++)
                {
                    richTextBox1.AppendText(splineMat[i, j] + " ");
                }
                richTextBox1.AppendText("\n");

            }

            richTextBox1.AppendText("\n");

            double[] splineAnsVect = createSplineAnsVector(xArr, yArr);

            foreach(var ans in splineAnsVect)
            {
                richTextBox1.AppendText(ans + "\n");
            }

            richTextBox1.AppendText("\n");


            Matrix<double> m = Matrix<double>.Build.DenseOfArray(splineMat);
            Vector<double> v = Vector<double>.Build.DenseOfArray(splineAnsVect);

            Vector<double> functions = m.Solve(v);

            richTextBox1.AppendText(functions.ToString());
            richTextBox1.AppendText("\n");

            for (int i = 0; i < yArr.Length-1; i++)
            {
                double s = xArr.Length - xArr[i];
                double d = xArr[i + 1] - xArr[i];
                double step = 0.1 ;

                while(step < 1)
                {
                    draw(step, d, functions.ToArray()[i], functions.ToArray()[i + 1], yArr[i], yArr[i + 1], Fx, xArr[i]+step);
                    s -= 0.1;
                    step += 0.1;
                }
            }
        }

        private void draw(double s, double d, double f1, double f2, double y1, double y2, Series Fx, double xi)
        {
            double f = f1 * (Math.Pow(s, 2) / 2.0) - f1 * (Math.Pow(s, 3) / (6 * d)) + f2 * (Math.Pow(s, 3) / (6 * d)) + (((y2 - y1) / d) * s) - f1 * ((d / 3.0) * s) - f2 * ((d / 6.0) * s) + y1;
            Fx.Points.AddXY(xi, f);

            richTextBox1.AppendText("xi: " + xi + "  f: " + f + "\n");
        }

        private double[,] createSplineMatrix(double[] xArr)
        {
            double[,] ans = new double[xArr.Length,xArr.Length];
            int shift = 0;
            for (int i = 0; i < xArr.Length-2; i++)
            {
                    double d1 = xArr[shift + 1] - xArr[shift];
                    double d2 = xArr[shift + 2] - xArr[shift + 1];

                    ans[i, shift] = d1 / 6.0;
                    ans[i, shift + 1] = (d1 + d2)/3.0;
                    ans[i, shift + 2] = d2/6.0;

                shift++;
            }

            ans[xArr.Length - 2, 0] = (xArr[1] - xArr[0]) / 3.0;
            ans[xArr.Length - 2, 1] = (xArr[1] - xArr[0]) / 6.0;

            ans[xArr.Length - 2, xArr.Length-2] = (xArr[xArr.Length-1] - xArr[xArr.Length-2]) / 6.0;
            ans[xArr.Length - 2, xArr.Length-1] = (xArr[xArr.Length - 1] - xArr[xArr.Length - 2]) / 3.0;

            ans[xArr.Length - 1, 0] = 1;
            ans[xArr.Length - 1, xArr.Length-1] = -1;

            return ans;
        }

        private double[] createSplineAnsVector(double[] xArr, double[] yArr)
        {
            double[] ans = new double[yArr.Length];

            for (int i = 0; i < yArr.Length-2; i++)
            {
                double d1 = xArr[i+1] - xArr[i];
                double d2= xArr[i+2] - xArr[i+1];

                double y1 = yArr[i];
                double y2 = yArr[i + 1];
                double y3 = yArr[i + 2];

                ans[i] = (y3 - y2) / d2 - (y2 - y1) / d1;
            }

            ans[xArr.Length - 2] = (yArr[1] - yArr[0]) / (xArr[1] - xArr[0]) - (yArr[yArr.Length-1] - yArr[yArr.Length-2]) / (xArr[xArr.Length-1] - xArr[xArr.Length-2]);


            return ans;
        }


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
