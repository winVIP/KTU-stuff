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

        public Form1()
        {
            InitializeComponent();
            Initialize();
        }

        Series Fx, V, V1; // naudojama atvaizduoti f-jai, šaknų rėžiams ir vidiniams taškams


        /// <summary>
        /// Sprendžiama lygtis F(x) = 0
        /// </summary>
        /// <param name="x">funkcijos argumentas</param>
        /// <returns></returns>
        private double F2O(double v)
        {
            //richTextBox1.AppendText(String.Format("F20 v={0:00.0000000000000000}\n", v));
            double m = 1.2;
            double ks = 0.001;
            double g = 9.8;           

            double tarpinis1 = -(m * g);
            //richTextBox1.AppendText(String.Format(" tarp1={0:00.0000000000000000}\n", tarpinis1));
            double tarpinis2 = ks * Math.Pow(v, 2);
            //richTextBox1.AppendText(String.Format(" ks={0:00.0000000000000000}\n", ks));
            
            //richTextBox1.AppendText(String.Format(" v^2={0:00.0000000000000000}\n", Math.Pow(v, 2)));
            //richTextBox1.AppendText(String.Format(" tarp2={0:00.0000000000000000}\n", tarpinis2));
            double tarpinis3 = tarpinis1 - tarpinis2;
            //richTextBox1.AppendText(String.Format(" tarp3={0:00.0000000000000000}\n", tarpinis3));
            double tarpinis4 = tarpinis3 / m;
            //richTextBox1.AppendText(String.Format("F20 a={0:00.0000000000000000}\n", tarpinis4));

            return tarpinis4;
        }

        private double F01(double v)
        {
            //richTextBox1.AppendText(String.Format("F01 v={0:00.0000000000000000}\n", v));
            double m1 = 0.4;
            double k1 = 0.02;
            double g = 9.8;

            //richTextBox1.AppendText(String.Format("F01 a={0:00.0000000000000000}\n", (m1 * g - k1 * v * v) / m1));
            return (-(m1 * g) - k1 * v * v) / m1;
        }

        private double F02(double v)
        {
            //richTextBox1.AppendText(String.Format("F02 v={0:00.0000000000000000}\n", v));
            double m2 = 0.8;
            double k2 = 0.02;
            double g = 9.8;

            //richTextBox1.AppendText(String.Format("F02 a={0:00.0000000000000000}\n", (m2 * g - k2 * v * v) / m2));
            return (-(m2 * g) - k2 * v * v) / m2;
        }


        // Mygtukas "Pusiaukirtos metodas" - ieškoma šaknies, ir vizualizuojamas paieškos procesas
        private void button3_Click(object sender, EventArgs e)
        {
            ClearForm(); // išvalomi programos duomenys
            PreparareForm(0, 10, 0, 50);
            
            // Nubraižoma f-ja, kuriai ieskome saknies
            Fx = chart1.Series.Add("V(t)");
            Fx.ChartType = SeriesChartType.Line;
            Fx.BorderWidth = 3;
            
            V = chart1.Series.Add("Greitis pirmo");
            V.MarkerStyle = MarkerStyle.Circle;
            V.MarkerSize = 8;
            V.ChartType = SeriesChartType.Point;
            V.ChartType = SeriesChartType.Line;
            V.Color = Color.Blue;

            V1 = chart1.Series.Add("Greitis antro");
            V1.MarkerStyle = MarkerStyle.Circle;
            V1.MarkerSize = 8;
            V1.ChartType = SeriesChartType.Point;
            V1.ChartType = SeriesChartType.Line;
            V1.Color = Color.Red;

            double tmax = 10;
            double ts = 2;
            double v0 = 50;
            double step = 0.1;
            int arraySize = (int)Math.Ceiling(tmax / step) + 1;
            double[,] result = new double[arraySize, 2];
            result[0, 0] = v0;
            result[0, 1] = v0;
            int arrIndex = 1;

            for (double t = 0.1; t < tmax; t += step)
            {
                if(t <= ts)
                {
                    //richTextBox1.AppendText("sitas\n");
                    //richTextBox1.AppendText("ArrIndex: " + arrIndex + "\n");
                    result[arrIndex, 0] = result[arrIndex - 1, 0] + step * F2O(result[arrIndex - 1, 0]);
                    result[arrIndex, 1] = result[arrIndex, 0];
                    arrIndex++;
                }
                else
                {
                    //richTextBox1.AppendText("anas\n");
                    //richTextBox1.AppendText("ArrIndex: " + arrIndex + "\n");
                    result[arrIndex, 0] = result[arrIndex - 1, 0] + step * F01(result[arrIndex - 1, 0]);
                    result[arrIndex, 1] = result[arrIndex - 1, 1] + step * F02(result[arrIndex - 1, 1]);
                    arrIndex++;
                }
            }

            arrIndex = 0;
            richTextBox1.AppendText("Iteracija t            V1(t)            V2(t)            \n");
            for (double t = 0; t < tmax; t += step)
            {
                richTextBox1.AppendText(String.Format("        {0:00.00}       {1:00.000000}        {2:00.000000} \n", t, result[arrIndex, 0], result[arrIndex, 1]));
                if (result[arrIndex, 0] >= 0)
                    V.Points.AddXY(t, result[arrIndex, 0]);
                if (result[arrIndex, 1] >= 0)
                    V1.Points.AddXY(t, result[arrIndex, 1]);
                arrIndex++;
            }

            arrIndex = 0;
            for (double t = 0; t < tmax; t += step)
            {
                if (result[arrIndex, 0] <= 0)
                {
                    richTextBox1.AppendText("Pirmas objektas pasieke auksciausia taska: " + t + " s\n");
                    break;
                }

                arrIndex++;
            }

            arrIndex = 0;
            for (double t = 0; t < tmax; t += step)
            {
                if (result[arrIndex, 1] <= 0)
                {
                    richTextBox1.AppendText("Antras objektas pasieke auksciausia taska: " + t + " s\n");
                    break;
                }

                arrIndex++;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ClearForm(); // išvalomi programos duomenys
            PreparareForm(0, 10, 0, 50);

            // Nubraižoma f-ja, kuriai ieskome saknies
            Fx = chart1.Series.Add("V(t)");
            Fx.ChartType = SeriesChartType.Line;
            Fx.BorderWidth = 3;

            V = chart1.Series.Add("Greitis pirmo");
            V.MarkerStyle = MarkerStyle.Circle;
            V.MarkerSize = 8;
            V.ChartType = SeriesChartType.Point;
            V.ChartType = SeriesChartType.Line;
            V.Color = Color.Blue;

            V1 = chart1.Series.Add("Greitis antro");
            V1.MarkerStyle = MarkerStyle.Circle;
            V1.MarkerSize = 8;
            V1.ChartType = SeriesChartType.Point;
            V1.ChartType = SeriesChartType.Line;
            V1.Color = Color.Red;

            double tmax = 10;
            double ts = 2;
            double v0 = 50;
            double step = 0.05;
            int arraySize = (int)Math.Ceiling(tmax / step) + 1;
            double[,] result = new double[arraySize, 2];
            result[0, 0] = v0;
            result[0, 1] = v0;
            int arrIndex = 1;

            for (double t = 0.1; t < tmax; t += step)
            {
                if (t <= ts)
                {
                    double k1 = step * F2O(result[arrIndex - 1, 0]);
                    double k2 = step * F2O(result[arrIndex - 1, 0] + k1 / 2);
                    double k3 = step * F2O(result[arrIndex - 1, 0] + k2 / 2);
                    double k4 = step * F2O(result[arrIndex - 1, 0] + k3);
                    
                    //richTextBox1.AppendText(String.Format(" t={4:00.0} k1={0:00.000000} k2={1:00.000000} k3={2:00.000000} k4={3:00.000000}\n", k1, k2, k3, k4, t));

                    //richTextBox1.AppendText(String.Format(" t={4:00.0} F1={0:00.000000} F2={1:00.000000} F3={2:00.000000} F4={3:00.000000}\n", 
                    //    F2O(result[arrIndex - 1, 0]), F2O(result[arrIndex - 1, 0] + k1 / 2), F2O(result[arrIndex - 1, 0] + k2 / 2),
                    //    F2O(result[arrIndex - 1, 0] + k3), t));

                    result[arrIndex, 0] = result[arrIndex - 1, 0] + ((k1 + 2 * k2 + 2 * k3 + k4) / 6);
                    result[arrIndex, 1] = result[arrIndex, 0];
                    arrIndex++;
                    //richTextBox1.AppendText(String.Format("------------------------------------------------------------------------------\n"));
                }
                else
                {
                    double k11 = step * F01(result[arrIndex - 1, 0]);
                    double k21 = step * F01(result[arrIndex - 1, 0] + k11 / 2);
                    double k31 = step * F01(result[arrIndex - 1, 0] + k21 / 2);
                    double k41 = step * F01(result[arrIndex - 1, 0] + k31);
                    //richTextBox1.AppendText(String.Format(" t={4:00.0} k11={0:00.000000} k21={1:00.000000} k31={2:00.000000} k41={3:00.000000}\n", k11, k21, k31, k41, t));

                    //richTextBox1.AppendText(String.Format(" t={4:00.0} F11={0:00.000000} F21={1:00.000000} F31={2:00.000000} F41={3:00.000000}\n", F01(result[arrIndex - 1, 0]),
                    //    F01(result[arrIndex - 1, 0] + k11 / 2), F01(result[arrIndex - 1, 0] + k21 / 2), F01(result[arrIndex - 1, 0] + k31), t));

                    double k12 = step * F02(result[arrIndex - 1, 1]);
                    double k22 = step * F02(result[arrIndex - 1, 1] + k12 / 2);
                    double k32 = step * F02(result[arrIndex - 1, 1] + k22 / 2);
                    double k42 = step * F02(result[arrIndex - 1, 1] + k32);
                    //richTextBox1.AppendText(String.Format(" t={4:00.0} k12={0:00.000000} k22={1:00.000000} k32={2:00.000000} k42={3:00.000000}\n", k12, k22, k32, k42, t));

                    //richTextBox1.AppendText(String.Format(" t={4:00.0} F12={0:00.000000} F22={1:00.000000} F32={2:00.000000} F42={3:00.000000}\n", F02(result[arrIndex - 1, 1]),
                    //    F02(result[arrIndex - 1, 1] + k12 / 2), F02(result[arrIndex - 1, 1] + k22 / 2), k42 = step * F02(result[arrIndex - 1, 1] + k32), t));

                    result[arrIndex, 0] = result[arrIndex - 1, 0] + ((k11 + 2 * k21 + 2 * k31 + k41) / 6);
                    result[arrIndex, 1] = result[arrIndex - 1, 1] + ((k12 + 2 * k22 + 2 * k32 + k42) / 6);
                    arrIndex++;
                    //richTextBox1.AppendText(String.Format("------------------------------------------------------------------------------\n"));
                }
            }

            arrIndex = 0;
            richTextBox1.AppendText("Iteracija t            V1(t)            V2(t)            \n");
            for (double t = 0; t < tmax; t += step)
            {
                richTextBox1.AppendText(String.Format("        {0:00.00}       {1:00.000000}        {2:00.000000} \n", t, result[arrIndex, 0], result[arrIndex, 1]));
                if (result[arrIndex, 0] >= 0)
                    V.Points.AddXY(t, result[arrIndex, 0]);
                if (result[arrIndex, 1] >= 0)
                    V1.Points.AddXY(t, result[arrIndex, 1]);
                arrIndex++;
            }

            arrIndex = 0;
            for (double t = 0; t < tmax; t += step)
            {
                if (result[arrIndex, 0] <= 0)
                {
                    richTextBox1.AppendText("Pirmas objektas pasieke auksciausia taska: " + t + " s\n");
                    break;
                }

                arrIndex++;
            }

            arrIndex = 0;
            for (double t = 0; t < tmax; t += step)
            {
                if (result[arrIndex, 1] <= 0)
                {
                    richTextBox1.AppendText("Antras objektas pasieke auksciausia taska: " + t + " s\n");
                    break;
                }

                arrIndex++;
            }
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

            // isvalomos visos nubreztos kreives
            chart1.Series.Clear();
        }
    }
}
