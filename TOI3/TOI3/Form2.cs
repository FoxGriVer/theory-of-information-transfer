using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TOI3
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int result;
            if (Int32.TryParse(textBox1.Text, out result))
            {
                if (result < 10)
                {
                    Form1.dataAboutImage.numberOfCells = result;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Укажите число меньше 10");
                }
            }
            else
            {
                MessageBox.Show("Введите число");
            }
        }
    }
}
