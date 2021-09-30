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
        List<Timer> Timerlist = new List<Timer>();

        public Form1()
        {
            InitializeComponent();
            Initialize();
        }

        double scanZingsnis = 0.5;
        double scanDabartinisZingsnis;
        double scanBuvesZingsnis;
        
        
        
        Dictionary<double, double> saknuIntervalai = new Dictionary<double, double>();

        #region F(x)

        double x1, x2; // izoliacijos intervalo pradžia ir galas

        Series Fx, X1X2; // naudojama atvaizduoti f-jai, šaknų rėžiams ir vidiniams taškams


        /// <summary>
        /// Sprendžiama lygtis F(x) = 0
        /// </summary>
        /// <param name="x">funkcijos argumentas</param>
        /// <returns></returns>
        private double F(double x)
        {
            return (double)(-0.79 * Math.Pow(x, 4) + 6.17 * Math.Pow(x, 3) - 16.66 * Math.Pow(x, 2) + 17.91 * x - 6.19);
        }

        #region saknu intervalu radimas

        int iterations;

        /// <summary>
        /// F(x) saknu intervalu radimas 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {
            iterations = 0;
            button2.Enabled = true;
            button5.Enabled = true;
            button7.Enabled = true;
            ClearForm(); // išvalomi programos duomenys
            PreparareForm(0, 4, -8, 2);
            x1 = 0; // izoliacijos intervalo pradžia
            x2 = 23.67088608; // izoliacijos intervalo galas

            scanZingsnis = 0.5;
            scanBuvesZingsnis = x1;
            scanDabartinisZingsnis = scanBuvesZingsnis + scanZingsnis;
            saknuIntervalai.Clear();

            // Nubraižoma f-ja, kuriai ieskome saknies
            Fx = chart1.Series.Add("F(x)");
            Fx.ChartType = SeriesChartType.Line;
            double x = 0;
            for (int i = 0; i < 10000; i++)
            {
                Fx.Points.AddXY(x, F(x)); x = x + 0.01;
            }
            Fx.BorderWidth = 3;

            X1X2 = chart1.Series.Add("X1X2");
            X1X2.MarkerStyle = MarkerStyle.Circle;
            X1X2.MarkerSize = 8;
            X1X2.ChartType = SeriesChartType.Point;
            X1X2.ChartType = SeriesChartType.Line;

            timer2.Enabled = true;
            timer2.Interval = 50; // timer2 intervalas milisekundemis
            timer2.Start();
        }

        /// <summary>
        /// F(x) saknu intervalu radimo timeris
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer2_Tick(object sender, EventArgs e)
        {
            X1X2.Points.Clear();

            X1X2.Points.AddXY(scanBuvesZingsnis, 0);
            X1X2.Points.AddXY(scanDabartinisZingsnis, 0);

            iterations++;
            richTextBox1.AppendText(String.Format("{4}) x1 = {0,0:0.000000}   x2 = {1,0:0.000000}   f(x1) = {2,10:0.0000000}   f(x2) = {3,10:0.0000000}\n",
                    scanBuvesZingsnis, scanDabartinisZingsnis, F(scanBuvesZingsnis), F(scanDabartinisZingsnis), iterations));

            if (Math.Sign(F(scanBuvesZingsnis)) != Math.Sign(F(scanDabartinisZingsnis)))
            {
                saknuIntervalai.Add(scanBuvesZingsnis, scanDabartinisZingsnis);
            }



            if (scanDabartinisZingsnis >= x2)
            {
                richTextBox1.AppendText("Rasti izoliacijos intervalai:\n");
                foreach (KeyValuePair<double, double> entry in saknuIntervalai)
                {
                    richTextBox1.AppendText(String.Format(" x1 = {0,6}   x2 = {1,6}\n", entry.Key, entry.Value));
                }
                richTextBox1.AppendText(String.Format("Skenavimo intervalas x1 = {0,6}   x2 = {1,6}\n", x1, x2));
                richTextBox1.AppendText(String.Format("Skenavimo zingsnis: {0}\n", scanZingsnis));
                richTextBox1.AppendText("\n\n");
                timer2.Stop();
            }

            scanBuvesZingsnis += scanZingsnis;
            scanDabartinisZingsnis += scanZingsnis;
        }

        #endregion

        #region tikslinimas skenavimo metodu

        int b2Index;

        /// <summary>
        /// Tikslinimas skenavimo metodu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button2_Click(object sender, EventArgs e)
        {
            iterations = 0;
            scanZingsnis = 0.01;
            scanBuvesZingsnis = saknuIntervalai.ElementAt(b2Index).Key;
            scanDabartinisZingsnis = scanBuvesZingsnis + scanZingsnis;
            b2Index++;
            timer1.Enabled = true;
            timer1.Interval = 50;
            timer1.Start();
        }

        /// <summary>
        /// Tikslinimo skenavimo metodu timeris
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Timer1_Tick(object sender, EventArgs e)
        {
            X1X2.Points.Clear();

            X1X2.Points.AddXY(scanBuvesZingsnis, 0);
            X1X2.Points.AddXY(scanDabartinisZingsnis, 0);

            iterations++;
            richTextBox1.AppendText(String.Format("{4}) x1 = {0,5:0.0000000}   x2 = {1,5:0.0000000}   f(x1) = {2,10:0.0000000}   f(x2) = {3,10:0.0000000}\n",
                    scanBuvesZingsnis, scanDabartinisZingsnis, F(scanBuvesZingsnis), F(scanDabartinisZingsnis), iterations));

            if (Math.Sign(F(scanBuvesZingsnis)) != Math.Sign(F(scanDabartinisZingsnis)))
            {
                if(Math.Abs(Math.Abs(scanDabartinisZingsnis) - Math.Abs(scanBuvesZingsnis)) > 0.000001)
                {
                    Console.WriteLine("Buvo {0}", scanZingsnis);
                    scanZingsnis = scanZingsnis / 10;
                    scanDabartinisZingsnis = scanBuvesZingsnis + scanZingsnis;
                    Timer1_Tick(null, null);
                }
                else
                {
                    richTextBox1.AppendText("\n");
                    richTextBox1.AppendText(String.Format("Patikslintas saknies intervalas: x1 = {0,5:0.0000000}   x2 = {1,5:0.0000000}\n",
                        scanBuvesZingsnis, scanDabartinisZingsnis));
                    if (b2Index == saknuIntervalai.Count)
                    {
                        button2.Enabled = false;
                        richTextBox1.AppendText(String.Format("Visi intervalai patikslinti\n"));
                        richTextBox1.AppendText(String.Format("Skenavimo intervalai:\n"));
                        foreach (KeyValuePair<double, double> entry in saknuIntervalai)
                        {
                            richTextBox1.AppendText(String.Format("{0} {1}\n", entry.Key, entry.Value));
                        }
                    }
                    richTextBox1.AppendText("\n\n");
                    timer1.Stop();
                }
                
            }

            scanBuvesZingsnis += scanZingsnis;
            scanDabartinisZingsnis += scanZingsnis;
        }

        #endregion

        #region tikslinimas kirstiniu metodu

        double kx1;
        double kx2;
        double kx3;
        List<double> xai;
        int b5Index;
        int t3Index;

        /// <summary>
        /// Tikslinimas kirstiniu metodu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button5_Click(object sender, EventArgs e)
        {
            iterations = 0;
            x1 = saknuIntervalai.ElementAt(b5Index).Key;
            x2 = saknuIntervalai.ElementAt(b5Index).Value;
            b5Index++;

            t3Index = 0;
            xai = new List<double>();
            xai.Add(x1);
            xai.Add(x2);
            
            timer3.Enabled = true;
            timer3.Interval = 250; // timer2 intervalas milisekundemis
            timer3.Start();

            richTextBox1.AppendText("\n\n");
        }

        /// <summary>
        /// Tikslinimo kirstiniu metodu timeris
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Timer3_Tick(object sender, EventArgs e)
        {
            X1X2.Points.Clear();

            kx1 = xai.ElementAt(t3Index);
            kx2 = xai.ElementAt(t3Index + 1);
            double fx1 = F(kx1);
            double fx2 = F(kx2);
            kx3 = kx2 - ((F(kx2) * (kx2 - kx1)) / (F(kx2) - F(kx1)));
            iterations++;
            richTextBox1.AppendText(String.Format("{6}) x{0} = {1,0:0.0000000} x{2} = {3,0:0.0000000} F(x{0}) = {4,0:0.0000000} F(x{2}) = {5,0:0.0000000}\n", t3Index + 1, kx1, t3Index + 2, kx2, fx1, fx2, iterations));
            richTextBox1.AppendText(String.Format("{3}) x{0} = {1,0:0.0000000} F(x{0}) = {2,0:0.0000000}\n", t3Index + 3, kx3, F(kx3), iterations));
            xai.Add(kx3);

            X1X2.Points.AddXY(kx1, F(kx1));
            X1X2.Points.AddXY(kx2, F(kx2));
            t3Index++;
            if (Math.Abs(F(kx3)) < 0.000001 || Math.Abs(F(kx3) - F(kx2)) < 0.000001)
            {
                richTextBox1.AppendText(String.Format("Patikslinta reiksme: x3 = {0,000000} f(x3) = {1,000000}\n", kx3, F(kx3)));
                if (b5Index == saknuIntervalai.Count)
                {
                    button5.Enabled = false;
                    richTextBox1.AppendText(String.Format("Visi intervalai patikslinti\n"));
                    richTextBox1.AppendText(String.Format("tikslinti intervalai:\n"));
                    foreach (KeyValuePair<double, double> entry in saknuIntervalai)
                    {
                        richTextBox1.AppendText(String.Format("{0} {1}\n", entry.Key, entry.Value));
                    }
                }
                
                richTextBox1.AppendText("\n\n");
                timer3.Stop();
            }
        }

        #endregion

        #region tikslinimas stygy metodu

        int b7index;

        /// <summary>
        /// Tikslinimas stygu metodu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button7_Click(object sender, EventArgs e)
        {
            iterations = 0;
            x1 = saknuIntervalai.ElementAt(b7index).Key;
            x2 = saknuIntervalai.ElementAt(b7index).Value;
            b7index++;
            timer5.Enabled = true;
            timer5.Interval = 500; // timer2 intervalas milisekundemis
            timer5.Start();
        }

        /// <summary>
        /// Tikslinimo stygu metodu timeris
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Timer5_Tick(object sender, EventArgs e)
        {
            X1X2.Points.Clear();

            double fx = double.MaxValue;
            double c = 0;

            iterations++;
            c = x1 - (F(x1) / (F(x2) - F(x1))) * (x2 - x1);
            if(Math.Sign(F(x1)) != Math.Sign(F(c)))
            {
                x2 = c;
                richTextBox1.AppendText(String.Format("{4}) x1 = {0,0:0.0000000} c = {1,0:0.0000000} F(x1) = {2,0:0.0000000} F(c) = {3,0:0.0000000}\n", x1, c, F(x1), F(c), iterations));
                X1X2.Points.AddXY(x1, F(x1));
                X1X2.Points.AddXY(x2, F(x2));
            }
            else if(Math.Sign(F(x2)) != Math.Sign(F(c)))
            {
                x1 = c;
                richTextBox1.AppendText(String.Format("{4}) c = {0,0:0.0000000} x2 = {1,0:0.0000000} F(c) = {2,0:0.0000000} F(x2) = {3,0:0.0000000}\n", c, x2, F(c), F(x2), iterations));
                X1X2.Points.AddXY(x1, F(x1));
                X1X2.Points.AddXY(x2, F(x2));
            }
            fx = F(c);

            

            if (Math.Abs(fx) < 0.000001)
            {
                richTextBox1.AppendText(String.Format("Patikslinta reiksme: c = {0,000000} f(c) = {1,000000}\n", c, fx));
                richTextBox1.AppendText("\n\n");
                if (b7index == saknuIntervalai.Count)
                {
                    button7.Enabled = false;
                    richTextBox1.AppendText(String.Format("Visi intervalai patikslinti\n"));
                    richTextBox1.AppendText(String.Format("tikslinti intervalai:\n"));
                    foreach (KeyValuePair<double, double> entry in saknuIntervalai)
                    {
                        richTextBox1.AppendText(String.Format("{0} {1}\n", entry.Key, entry.Value));
                    }
                }
                timer5.Stop();
            }
        }

        #endregion

        #endregion

        #region G(x)

        private double G(double x)
        {
            return (double)(2 * x * Math.Cos(x) - Math.Pow(x / 2 + 0.5, 3));
        }

        #region saknu intervalu radimas

        private void Button6_Click(object sender, EventArgs e)
        {
            iterations = 0;
            button8.Enabled = true;
            button9.Enabled = true;
            button10.Enabled = true;
            ClearForm(); // išvalomi programos duomenys
            PreparareForm(-10, 10, -10, 10);
            x1 = -10; // izoliacijos intervalo pradžia
            x2 = 10; // izoliacijos intervalo galas

            scanZingsnis = 0.5;
            scanBuvesZingsnis = x1;
            scanDabartinisZingsnis = scanBuvesZingsnis + scanZingsnis;
            saknuIntervalai.Clear();

            Fx = chart1.Series.Add("G(x)");
            Fx.ChartType = SeriesChartType.Line;
            double x = x1;

            for (int i = 0; i < 10000; i++)
            {
                Fx.Points.AddXY(x, G(x)); x = x + 0.01;//(2 * Math.PI) /50;
            }

            Fx.BorderWidth = 3;
            X1X2 = chart1.Series.Add("X1X2");
            X1X2.MarkerStyle = MarkerStyle.Circle;
            X1X2.MarkerSize = 8;
            X1X2.ChartType = SeriesChartType.Point;
            X1X2.ChartType = SeriesChartType.Line;

            timer4.Enabled = true;
            timer4.Interval = 50; // timer2 intervalas milisekundemis
            timer4.Start();
        }

        private void Timer4_Tick(object sender, EventArgs e)
        {
            X1X2.Points.Clear();

            X1X2.Points.AddXY(scanBuvesZingsnis, 0);
            X1X2.Points.AddXY(scanDabartinisZingsnis, 0);

            iterations++;
            richTextBox1.AppendText(String.Format("{4}) x1 = {0,0:0.000000}   x2 = {1,0:0.000000}   G(x1) = {2,0:0.000000}   G(x2) = {3,0:0.000000}\n",
                    scanBuvesZingsnis, scanDabartinisZingsnis, G(scanBuvesZingsnis), G(scanDabartinisZingsnis), iterations));

            if (Math.Sign(G(scanBuvesZingsnis)) != Math.Sign(G(scanDabartinisZingsnis)))
            {
                saknuIntervalai.Add(scanBuvesZingsnis, scanDabartinisZingsnis);
            }

            if (scanDabartinisZingsnis >= x2)
            {
                richTextBox1.AppendText("Rasti izoliacijos intervalai:\n");
                foreach (KeyValuePair<double, double> entry in saknuIntervalai)
                {
                    richTextBox1.AppendText(String.Format(" x1 = {0,6}   x2 = {1,6}\n", entry.Key, entry.Value));
                }
                richTextBox1.AppendText(String.Format("Skenavimo intervalas x1 = {0,6}   x2 = {1,6}\n", x1, x2));
                richTextBox1.AppendText(String.Format("Skenavimo zingsnis: {0}\n", scanZingsnis));
                richTextBox1.AppendText("\n\n");
                timer4.Stop();
            }

            scanBuvesZingsnis += scanZingsnis;
            scanDabartinisZingsnis += scanZingsnis;
        }

        #endregion

        #region tikslinimas skenavimo metodu

        int b8Index;

        private void Button8_Click(object sender, EventArgs e)
        {
            iterations = 0;
            scanZingsnis = 0.01;
            scanBuvesZingsnis = saknuIntervalai.ElementAt(b8Index).Key;
            scanDabartinisZingsnis = scanBuvesZingsnis + scanZingsnis;
            b8Index++;
            timer6.Enabled = true;
            timer6.Interval = 100; // timer2 intervalas milisekundemis
            timer6.Start();
        }

        private void Timer6_Tick(object sender, EventArgs e)
        {
            X1X2.Points.Clear();

            X1X2.Points.AddXY(scanBuvesZingsnis, 0);
            X1X2.Points.AddXY(scanDabartinisZingsnis, 0);

            iterations++;
            richTextBox1.AppendText(String.Format("{4}) x1 = {0,5:0.0000000}   x2 = {1,5:0.0000000}   G(x1) = {2,10:0.0000000}   G(x2) = {3,10:0.0000000}\n",
                    scanBuvesZingsnis, scanDabartinisZingsnis, G(scanBuvesZingsnis), G(scanDabartinisZingsnis), iterations));

            if (Math.Sign(G(scanBuvesZingsnis)) != Math.Sign(G(scanDabartinisZingsnis)))
            {
                if (Math.Abs(Math.Abs(scanDabartinisZingsnis) - Math.Abs(scanBuvesZingsnis)) > 0.00001)
                {
                    Console.WriteLine("Buvo {0}", scanZingsnis);
                    scanZingsnis = scanZingsnis / 10;
                    scanDabartinisZingsnis = scanBuvesZingsnis + scanZingsnis;
                    Timer6_Tick(null, null);
                }
                else
                {
                    richTextBox1.AppendText("\n");
                    richTextBox1.AppendText(String.Format("Patikslintas saknies intervalas: x1 = {0,5:0.0000000}   x2 = {1,5:0.0000000}\n",
                        scanBuvesZingsnis, scanDabartinisZingsnis));
                    if (b8Index == saknuIntervalai.Count)
                    {
                        button8.Enabled = false;
                        richTextBox1.AppendText(String.Format("Visi intervalai patikslinti\n"));
                        richTextBox1.AppendText(String.Format("Skenavimo intervalai:\n"));
                        foreach (KeyValuePair<double, double> entry in saknuIntervalai)
                        {
                            richTextBox1.AppendText(String.Format("{0} {1}\n", entry.Key, entry.Value));
                        }
                    }
                    richTextBox1.AppendText("\n\n");
                    timer6.Stop();
                } 
            }

            scanBuvesZingsnis += scanZingsnis;
            scanDabartinisZingsnis += scanZingsnis;
        }

        #endregion

        #region tikslinimas kirstiniu metodu

        int b9Index;

        private void Button9_Click(object sender, EventArgs e)
        {
            iterations = 0;
            x1 = saknuIntervalai.ElementAt(b9Index).Key;
            x2 = saknuIntervalai.ElementAt(b9Index).Value;
            b9Index++;

            t3Index = 0;
            xai = new List<double>();
            xai.Add(x1);
            xai.Add(x2);

            timer7.Enabled = true;
            timer7.Interval = 250;
            timer7.Start();

            richTextBox1.AppendText("\n\n");
        }

        private void Timer7_Tick(object sender, EventArgs e)
        {
            X1X2.Points.Clear();

            kx1 = xai.ElementAt(t3Index);
            kx2 = xai.ElementAt(t3Index + 1);
            double fx1 = G(kx1);
            double fx2 = G(kx2);

            iterations++;

            kx3 = kx2 - ((G(kx2) * (kx2 - kx1)) / (G(kx2) - G(kx1)));
            richTextBox1.AppendText(String.Format("{6}) x{0} = {1,0:0.0000000} x{2} = {3,0:0.0000000} G(x{0}) = {4,0:0.0000000} G(x{2}) = {5,0:0.0000000}\n", t3Index + 1, kx1, t3Index + 2, kx2, fx1, fx2, iterations));
            richTextBox1.AppendText(String.Format("{3}) x{0} = {1,0:0.0000000} G(x{0}) = {2,0:0.0000000}\n", t3Index + 3, kx3, G(kx3), iterations));
            xai.Add(kx3);

            X1X2.Points.AddXY(kx1, G(kx1));
            X1X2.Points.AddXY(kx2, G(kx2));
            t3Index++;
            if (Math.Abs(G(kx3)) < 0.000001 || Math.Abs(G(kx3) - G(kx2)) < 0.000001)
            {
                richTextBox1.AppendText(String.Format("Patikslinta reiksme: x3 = {0,000000} f(x3) = {1,000000}\n", kx3, F(kx3)));
                if (b9Index == saknuIntervalai.Count)
                {
                    button9.Enabled = false;
                    richTextBox1.AppendText(String.Format("Visi intervalai patikslinti\n"));
                    richTextBox1.AppendText(String.Format("tikslinti intervalai:\n"));
                    foreach (KeyValuePair<double, double> entry in saknuIntervalai)
                    {
                        richTextBox1.AppendText(String.Format("{0} {1}\n", entry.Key, entry.Value));
                    }
                }
                richTextBox1.AppendText("\n\n");
                timer7.Stop();
            }
        }

        #endregion

        #region tikslinimas stygy metodu

        int b10index;

        private void Button10_Click(object sender, EventArgs e)
        {
            iterations = 0;
            x1 = saknuIntervalai.ElementAt(b10index).Key;
            x2 = saknuIntervalai.ElementAt(b10index).Value;
            b10index++;
            timer8.Enabled = true;
            timer8.Interval = 500; // timer2 intervalas milisekundemis
            timer8.Start();
        }

        private void Timer8_Tick(object sender, EventArgs e)
        {
            X1X2.Points.Clear();

            double fx = double.MaxValue;
            double c = 0;

            iterations++;

            c = x1 - (G(x1) / (G(x2) - G(x1))) * (x2 - x1);
            if (Math.Sign(G(x1)) != Math.Sign(G(c)))
            {
                x2 = c;
                richTextBox1.AppendText(String.Format("{4}) x1 = {0,0:0.0000000} c = {1,0:0.0000000} G(x1) = {2,0:0.0000000} G(c) = {3,0:0.0000000}\n", x1, c, G(x1), G(c), iterations));
                X1X2.Points.AddXY(x1, G(x1));
                X1X2.Points.AddXY(x2, G(x2));
            }
            else if (Math.Sign(G(x2)) != Math.Sign(G(c)))
            {
                x1 = c;
                richTextBox1.AppendText(String.Format("{4}) c = {0,0:0.0000000} x2 = {1,0:0.0000000} G(c) = {2,0:0.0000000} G(x2) = {3,0:0.0000000}\n", c, x2, G(c), G(x2), iterations));
                X1X2.Points.AddXY(x1, G(x1));
                X1X2.Points.AddXY(x2, G(x2));
            }
            fx = G(c);



            if (Math.Abs(fx) < 0.000001)
            {
                richTextBox1.AppendText(String.Format("Patikslinta reiksme: c = {0,000000} f(c) = {1,000000}\n", c, fx));
                richTextBox1.AppendText("\n\n");
                if (b10index == saknuIntervalai.Count)
                {
                    button10.Enabled = false;
                    richTextBox1.AppendText(String.Format("Visi intervalai patikslinti\n"));
                    richTextBox1.AppendText(String.Format("tikslinti intervalai:\n"));
                    foreach (KeyValuePair<double, double> entry in saknuIntervalai)
                    {
                        richTextBox1.AppendText(String.Format("{0} {1}\n", entry.Key, entry.Value));
                    }
                }
                timer8.Stop();
            }
        }

        #endregion

        #endregion

        #region V(t)

        private double V(double m)
        {
            double smt1 = Math.Pow(Math.E, -1 * 0.05 * 3 / m);
            return (double)(-49 + 100 * smt1 + m * 9.8 / 0.05 * (smt1 - 1));
        }

        #region saknu intervalu radimas

        private void Button11_Click_1(object sender, EventArgs e)
        {
            iterations = 0;
            button12.Enabled = true;
            ClearForm(); // išvalomi programos duomenys
            PreparareForm(-50, 50, -30, 70);
            x1 = -50; // izoliacijos intervalo pradžia
            x2 = 50; // izoliacijos intervalo galas

            scanZingsnis = 1;
            scanBuvesZingsnis = x1;
            scanDabartinisZingsnis = scanBuvesZingsnis + scanZingsnis;
            saknuIntervalai.Clear();


            // Nubraižoma f-ja, kuriai ieskome saknies
            Fx = chart1.Series.Add("V(m)");
            Fx.ChartType = SeriesChartType.Line;
            double m = x1;
            for (int i = 0; i < 10000; i++)
            {
                Console.WriteLine("m = {0} V(m) = {1}", m, V(m));
                if (m != 0)
                {
                    Fx.Points.AddXY(m, V(m));
                }
                m = m + 0.01;
            }
            Fx.BorderWidth = 3;

            X1X2 = chart1.Series.Add("X1X2");
            X1X2.MarkerStyle = MarkerStyle.Circle;
            X1X2.MarkerSize = 8;
            X1X2.ChartType = SeriesChartType.Point;
            X1X2.ChartType = SeriesChartType.Line;

            timer9.Enabled = true;
            timer9.Interval = 1;
            timer9.Start();
        }

        private void Timer9_Tick(object sender, EventArgs e)
        {
            X1X2.Points.Clear();

            X1X2.Points.AddXY(scanBuvesZingsnis, 0);
            X1X2.Points.AddXY(scanDabartinisZingsnis, 0);

            iterations++;

            richTextBox1.AppendText(String.Format("{4}) m1 = {0,0:0.000000}   m2 = {1,0:0.000000}   V(m1) = {2,10:0.0000000}   V(m2) = {3,10:0.0000000}\n",
                    scanBuvesZingsnis, scanDabartinisZingsnis, V(scanBuvesZingsnis), V(scanDabartinisZingsnis), iterations));

            if (Math.Sign(V(scanBuvesZingsnis)) != Math.Sign(V(scanDabartinisZingsnis)))
            {
                saknuIntervalai.Add(scanBuvesZingsnis, scanDabartinisZingsnis);
            }

            if (scanDabartinisZingsnis >= x2)
            {
                richTextBox1.AppendText("Rasti izoliacijos intervalai:\n");
                foreach (KeyValuePair<double, double> entry in saknuIntervalai)
                {
                    richTextBox1.AppendText(String.Format(" m1 = {0,6}   m2 = {1,6}\n", entry.Key, entry.Value));
                }
                richTextBox1.AppendText("\n\n");
                timer9.Stop();
            }

            scanBuvesZingsnis += scanZingsnis;
            scanDabartinisZingsnis += scanZingsnis;
        }

        int b12Index;
        private void Button12_Click(object sender, EventArgs e)
        {
            iterations = 0;
            x1 = saknuIntervalai.ElementAt(b12Index).Key;
            x2 = saknuIntervalai.ElementAt(b12Index).Value;
            b12Index++;

            t3Index = 0;
            xai = new List<double>();
            xai.Add(x1);
            xai.Add(x2);

            timer10.Enabled = true;
            timer10.Interval = 250; // timer2 intervalas milisekundemis
            timer10.Start();

            richTextBox1.AppendText("\n\n");
        }

        private void Timer10_Tick(object sender, EventArgs e)
        {
            X1X2.Points.Clear();

            kx1 = xai.ElementAt(t3Index);
            kx2 = xai.ElementAt(t3Index + 1);
            double fx1 = V(kx1);
            double fx2 = V(kx2);
            kx3 = kx2 - ((V(kx2) * (kx2 - kx1)) / (V(kx2) - V(kx1)));

            iterations++;

            richTextBox1.AppendText(String.Format("{6}) m{0} = {1,0:0.0000000} m{2} = {3,0:0.0000000} V(m{0}) = {4,0:0.0000000} V(m{2}) = {5,0:0.0000000}\n", t3Index + 1, kx1, t3Index + 2, kx2, fx1, fx2, iterations));
            richTextBox1.AppendText(String.Format("{3}) m{0} = {1,0:0.0000000} V(m{0}) = {2,0:0.0000000}\n", t3Index + 3, kx3, V(kx3), iterations));
            xai.Add(kx3);

            X1X2.Points.AddXY(kx1, V(kx1));
            X1X2.Points.AddXY(kx2, V(kx2));
            t3Index++;
            if (Math.Abs(V(kx3)) < 0.000001 || Math.Abs(V(kx3) - V(kx2)) < 0.000001)
            {
                if (b12Index == saknuIntervalai.Count)
                {
                    button12.Enabled = false;
                    richTextBox1.AppendText(String.Format("Visi intervalai patikslinti"));
                }
                richTextBox1.AppendText("\n\n");
                timer10.Stop();
            }
        }

        #endregion

        #endregion

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
