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


namespace TOI2._2
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            int i = 0;
            if (Form1.ourData.TextResult1 >= 0)
            {
                chart1.Series[i].Points.Add(Form1.ourData.TextResult1);
                chart1.Series[i].Name = "1";
            }
            if (Form1.ourData.TextResult2 >= 0)
            {
                i++;
                chart1.Series.Add(new Series("2"));
                chart1.Series[i].Points.Add(Form1.ourData.TextResult2);
            }
            if (Form1.ourData.TextResult3 >= 0)
            {
                i++;
                chart1.Series.Add(new Series("3"));
                chart1.Series[i].Points.Add(Form1.ourData.TextResult3);
            }
            if (Form1.ourData.TextResult4 >= 0)
            {
                i++;
                chart1.Series.Add(new Series("4"));
                chart1.Series[i].Points.Add(Form1.ourData.TextResult4);
            }
            if (Form1.ourData.TextResult5 >= 0)
            {
                i++;
                chart1.Series.Add(new Series("5"));
                chart1.Series[i].Points.Add(Form1.ourData.TextResult5);
            }
            if (Form1.ourData.TextResult6 >= 0)
            {
                i++;
                chart1.Series.Add(new Series("6"));
                chart1.Series[i].Points.Add(Form1.ourData.TextResult6);
            }            
            if (Form1.ourData.TextResult7 >= 0)
            {
                i++;
                chart1.Series.Add(new Series("7"));
                chart1.Series[i].Points.Add(Form1.ourData.TextResult7);
            }
            if (Form1.ourData.TextResult8 >= 0)
            {
                i++;
                chart1.Series.Add(new Series("8"));
                chart1.Series[i].Points.Add(Form1.ourData.TextResult8);
            }
            if (Form1.ourData.TextResult9 >= 0)
            {
                i++;
                chart1.Series.Add(new Series("9"));
                chart1.Series[i].Points.Add(Form1.ourData.TextResult9);
            }
        }
    }
}
