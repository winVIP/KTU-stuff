﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
namespace Pvz1
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea3 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend3 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.timer4 = new System.Windows.Forms.Timer(this.components);
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.timer3 = new System.Windows.Forms.Timer(this.components);
            this.button2 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.button8 = new System.Windows.Forms.Button();
            this.button9 = new System.Windows.Forms.Button();
            this.button10 = new System.Windows.Forms.Button();
            this.timer5 = new System.Windows.Forms.Timer(this.components);
            this.timer6 = new System.Windows.Forms.Timer(this.components);
            this.timer7 = new System.Windows.Forms.Timer(this.components);
            this.timer8 = new System.Windows.Forms.Timer(this.components);
            this.button11 = new System.Windows.Forms.Button();
            this.timer9 = new System.Windows.Forms.Timer(this.components);
            this.button12 = new System.Windows.Forms.Button();
            this.timer10 = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            this.SuspendLayout();
            // 
            // chart1
            // 
            chartArea3.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea3);
            legend3.Name = "Legend1";
            this.chart1.Legends.Add(legend3);
            this.chart1.Location = new System.Drawing.Point(12, 12);
            this.chart1.Name = "chart1";
            this.chart1.Size = new System.Drawing.Size(669, 394);
            this.chart1.TabIndex = 0;
            this.chart1.Text = "chart1";
            // 
            // richTextBox1
            // 
            this.richTextBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.richTextBox1.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(186)));
            this.richTextBox1.Location = new System.Drawing.Point(12, 412);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(669, 251);
            this.richTextBox1.TabIndex = 1;
            this.richTextBox1.Text = "";
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(813, 643);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "Baigti";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // timer2
            // 
            this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(692, 12);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(194, 25);
            this.button3.TabIndex = 4;
            this.button3.Text = "F(x)";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button4.Location = new System.Drawing.Point(732, 643);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(75, 23);
            this.button4.TabIndex = 6;
            this.button4.Text = "Valyti";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(692, 133);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(194, 23);
            this.button6.TabIndex = 8;
            this.button6.Text = "G(x)";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.Button6_Click);
            // 
            // timer4
            // 
            this.timer4.Tick += new System.EventHandler(this.Timer4_Tick);
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.Timer1_Tick);
            // 
            // timer3
            // 
            this.timer3.Tick += new System.EventHandler(this.Timer3_Tick);
            // 
            // button2
            // 
            this.button2.Enabled = false;
            this.button2.Location = new System.Drawing.Point(692, 44);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(194, 23);
            this.button2.TabIndex = 9;
            this.button2.Text = "Tikslinimas skenavimo metodu";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.Button2_Click);
            // 
            // button5
            // 
            this.button5.Enabled = false;
            this.button5.Location = new System.Drawing.Point(692, 74);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(194, 23);
            this.button5.TabIndex = 10;
            this.button5.Text = "Tikslinimas kirstiniu metodu";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.Button5_Click);
            // 
            // button7
            // 
            this.button7.Enabled = false;
            this.button7.Location = new System.Drawing.Point(694, 104);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(192, 23);
            this.button7.TabIndex = 11;
            this.button7.Text = "Tikslinimas stygu metodu";
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.Button7_Click);
            // 
            // button8
            // 
            this.button8.Enabled = false;
            this.button8.Location = new System.Drawing.Point(694, 163);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(192, 23);
            this.button8.TabIndex = 12;
            this.button8.Text = "Tikslinimas skenavimo metodu";
            this.button8.UseVisualStyleBackColor = true;
            this.button8.Click += new System.EventHandler(this.Button8_Click);
            // 
            // button9
            // 
            this.button9.Enabled = false;
            this.button9.Location = new System.Drawing.Point(694, 193);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(192, 23);
            this.button9.TabIndex = 13;
            this.button9.Text = "Tikslinimas kirstiniu metodu";
            this.button9.UseVisualStyleBackColor = true;
            this.button9.Click += new System.EventHandler(this.Button9_Click);
            // 
            // button10
            // 
            this.button10.Enabled = false;
            this.button10.Location = new System.Drawing.Point(694, 222);
            this.button10.Name = "button10";
            this.button10.Size = new System.Drawing.Size(192, 23);
            this.button10.TabIndex = 14;
            this.button10.Text = "Tikslinimas stygu metodu";
            this.button10.UseVisualStyleBackColor = true;
            this.button10.Click += new System.EventHandler(this.Button10_Click);
            // 
            // timer5
            // 
            this.timer5.Tick += new System.EventHandler(this.Timer5_Tick);
            // 
            // timer6
            // 
            this.timer6.Tick += new System.EventHandler(this.Timer6_Tick);
            // 
            // timer7
            // 
            this.timer7.Tick += new System.EventHandler(this.Timer7_Tick);
            // 
            // timer8
            // 
            this.timer8.Tick += new System.EventHandler(this.Timer8_Tick);
            // 
            // button11
            // 
            this.button11.Location = new System.Drawing.Point(694, 252);
            this.button11.Name = "button11";
            this.button11.Size = new System.Drawing.Size(192, 23);
            this.button11.TabIndex = 15;
            this.button11.Text = "V(x)";
            this.button11.UseVisualStyleBackColor = true;
            this.button11.Click += new System.EventHandler(this.Button11_Click_1);
            // 
            // timer9
            // 
            this.timer9.Tick += new System.EventHandler(this.Timer9_Tick);
            // 
            // button12
            // 
            this.button12.Enabled = false;
            this.button12.Location = new System.Drawing.Point(694, 282);
            this.button12.Name = "button12";
            this.button12.Size = new System.Drawing.Size(192, 23);
            this.button12.TabIndex = 16;
            this.button12.Text = "Tikslinimas kirstiniu metodu";
            this.button12.UseVisualStyleBackColor = true;
            this.button12.Click += new System.EventHandler(this.Button12_Click);
            // 
            // timer10
            // 
            this.timer10.Tick += new System.EventHandler(this.Timer10_Tick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(898, 678);
            this.Controls.Add(this.button12);
            this.Controls.Add(this.button11);
            this.Controls.Add(this.button10);
            this.Controls.Add(this.button9);
            this.Controls.Add(this.button8);
            this.Controls.Add(this.button7);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.chart1);
            this.Name = "Form1";
            this.Text = "Skaitiniai metodai ir Algoritmai";
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            this.ResumeLayout(false);

        }


        /// <summary>
        /// Inicializacijos veiksmai
        /// </summary>
        private void Initialize()
        {
            // pridedam timerius
            Timerlist.Clear();
            Timerlist.Add(timer2);
            Timerlist.Add(timer4);
        }

        /// <summary>
        /// Paruošiamas langas vaizdavimui
        /// </summary>
        public void PreparareForm(float xmin, float xmax, float ymin, float ymax)
        {

            float x_grids = 10;
            //double xmin = 0; double xmax = 2 * Math.PI;
            chart1.ChartAreas[0].AxisX.MajorGrid.Interval = (xmax - xmin) / x_grids;
            chart1.ChartAreas[0].AxisX.LabelStyle.Interval = (xmax - xmin) / x_grids;
            chart1.ChartAreas[0].AxisX.MajorTickMark.Interval = (xmax - xmin) / x_grids;
            chart1.ChartAreas[0].AxisX.LabelStyle.Font = new Font("Courier New", 8, FontStyle.Bold);

            float y_grids = 10;
            //double ymin = -1; double ymax = 1;
            chart1.ChartAreas[0].AxisY.MajorGrid.Interval = (ymax - ymin) / y_grids;
            chart1.ChartAreas[0].AxisY.LabelStyle.Interval = (ymax - ymin) / y_grids;
            chart1.ChartAreas[0].AxisY.MajorTickMark.Interval = (ymax - ymin) / y_grids;
            chart1.ChartAreas[0].AxisY.LabelStyle.Font = new Font("Courier New", 8, FontStyle.Bold);

            chart1.ChartAreas[0].AxisX.Minimum = xmin;
            chart1.ChartAreas[0].AxisX.Maximum = xmax;
            chart1.ChartAreas[0].AxisY.Minimum = ymin;
            chart1.ChartAreas[0].AxisY.Maximum = ymax;

            chart1.Legends[0].Font = new Font("Times New Roman", 12, FontStyle.Bold);
            chart1.ChartAreas[0].CursorX.IsUserSelectionEnabled = true;
            chart1.ChartAreas[0].CursorX.Interval = 0.01;
            chart1.ChartAreas[0].CursorY.IsUserSelectionEnabled = true;
            chart1.ChartAreas[0].CursorY.Interval = 0.01;
            
        }
        #endregion
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Timer timer2;
        private System.Windows.Forms.Button button3;
        private Button button4;
        private Button button6;
        private Timer timer4;
        private Timer timer1;
        private Timer timer3;
        private Button button2;
        private Button button5;
        private Button button7;
        private Button button8;
        private Button button9;
        private Button button10;
        private Timer timer5;
        private Timer timer6;
        private Timer timer7;
        private Timer timer8;
        private Button button11;
        private Timer timer9;
        private Button button12;
        private Timer timer10;
    }



}

