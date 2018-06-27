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

namespace TOI1
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
            if (Form1.OurFile.OriginalSize != 0)
            {
                chart1.Series[i].Points.Add(Form1.OurFile.OriginalSize);
                chart1.Series[i].Name = "Original";
            }
            if (Form1.OurFile.RLESize != 0)
            {
                i++;
                chart1.Series.Add(new Series("RLE"));
                chart1.Series[i].Points.Add(Form1.OurFile.RLESize);                
            }
            if (Form1.OurFile.LZWSize != 0)
            {
                i++;
                chart1.Series.Add(new Series("LZW"));
                chart1.Series[i].Points.Add(Form1.OurFile.LZWSize);
            }
            if (Form1.OurFile.JPEGSize != 0)
            {
                i++;
                chart1.Series.Add(new Series("JPEG"));
                chart1.Series[i].Points.Add(Form1.OurFile.JPEGSize);
            }
        }
    }
}
