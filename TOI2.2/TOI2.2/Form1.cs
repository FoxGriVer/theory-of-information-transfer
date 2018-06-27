using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace TOI2._2
{
    public partial class Form1 : Form
    {
        public static DataFiles ourData;

        public Form1()
        {
            InitializeComponent();
            ourData = new DataFiles();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog open_dialog = new OpenFileDialog();
            open_dialog.Filter = "Text files (*.txt)|*.txt|All files(*.*)|*.*";
            if (open_dialog.ShowDialog() == DialogResult.OK)
            {
                Encoding enc = Encoding.GetEncoding(1251);
                string path = open_dialog.FileName;
                string temp;
                StreamReader sr = new StreamReader(path, enc);
                temp = sr.ReadLine();
                ourData.AllText = temp;
                while (temp != null)
                {
                    temp = sr.ReadLine();
                    ourData.AllText += temp;
                }
            }
            textBox1.Text = ourData.AllText;
        }

        private int CompareText(string text1, string text2)
        {
            Matching(text1, text2, 5);
            double f = ourData.LenghtCountLike;
            Matching(text1, text1, 5);
            double d = ourData.LenghtSubRows;
            int g = Convert.ToInt32(Math.Round(f / d * 100, 2));
            return g;
        }

        public void Matching(string strInputA, string strInputB, int lngLen)
        {
            int PosStrA;
            int PosStrB;
            string strTempA;
            string strTempB;
            ourData.LenghtCountLike = 0;
            ourData.LenghtSubRows = 0;
            for (PosStrA = 0; PosStrA <= strInputA.Length - lngLen; PosStrA++)
            {
                strTempA = strInputA.Substring(PosStrA, lngLen);
                for (PosStrB = 0; PosStrB <= strInputB.Length - lngLen; PosStrB++)
                {
                    strTempB = strInputB.Substring(PosStrB, lngLen);
                    if ((string.Compare(strTempA, strTempB) == 0))
                    {
                        ourData.LenghtCountLike = (ourData.LenghtCountLike + 1);
                        break;
                    }
                }
                ourData.LenghtSubRows = (ourData.LenghtSubRows + 1);
            }
        }


        private static string RemoveSimbols(string words)
        {
            string word = words;
            while (word.Substring(word.Length - 1, 1) == "," || word.Substring(word.Length - 1, 1) == "»"
                || word.Substring(word.Length - 1, 1) == "!" || word.Substring(word.Length - 1, 1) == "?"
                || word.Substring(word.Length - 1, 1) == "-" || word.Substring(word.Length - 1, 1) == "." || word.Substring(0, 1) == "«")
            {
                if (word.Substring(0, 1) == "«")
                    word = word.Substring(1, word.Length - 1);
                else word = word.Substring(0, word.Length - 1);
            }
            return word;
        }
      
        private string DownloadText(string path)
        {
            Encoding enc = Encoding.GetEncoding(1251);
            StreamReader sr = new StreamReader(@path, enc);
            string temp;
            string text;
            temp = sr.ReadLine();

            text = temp;
            while (temp != null)
            {
                temp = sr.ReadLine();
                text += temp;
            }
            string shiltext = "";
            string[] word = text.Split(' ');

            foreach (string s in word)
            {
                if (s.Length > 3)
                {
                    string s1 = s;
                    s1 = RemoveSimbols(s1);
                    shiltext += s1;
                }
            }
            return shiltext;
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            ourData.Text1 = DownloadText(@"C:\Users\Pavel\Documents\Visual Studio 2015\Projects\TOI2.2\text1.txt");
            ourData.Text2 = DownloadText(@"C:\Users\Pavel\Documents\Visual Studio 2015\Projects\TOI2.2\text2.txt");
            ourData.Text3 = DownloadText(@"C:\Users\Pavel\Documents\Visual Studio 2015\Projects\TOI2.2\text3.txt");
            ourData.Text4 = DownloadText(@"C:\Users\Pavel\Documents\Visual Studio 2015\Projects\TOI2.2\text4.txt");
            ourData.Text5 = DownloadText(@"C:\Users\Pavel\Documents\Visual Studio 2015\Projects\TOI2.2\text5.txt");
            ourData.Text6 = DownloadText(@"C:\Users\Pavel\Documents\Visual Studio 2015\Projects\TOI2.2\text6.txt");
            ourData.Text7 = DownloadText(@"C:\Users\Pavel\Documents\Visual Studio 2015\Projects\TOI2.2\text7.txt");
            ourData.Text8 = DownloadText(@"C:\Users\Pavel\Documents\Visual Studio 2015\Projects\TOI2.2\text8.txt");
            ourData.Text9 = DownloadText(@"C:\Users\Pavel\Documents\Visual Studio 2015\Projects\TOI2.2\text9.txt");

            string[] word1 = ourData.AllText.Split(' ');

            foreach (string s in word1)
            {
                if (s.Length > 3)
                {
                    string s1 = s;
                    s1 = RemoveSimbols(s1);
                    ourData.Text10 += s1;
                }
            }

            ourData.TextResult1 = CompareText(ourData.Text10, ourData.Text1);
            ourData.TextResult2 = CompareText(ourData.Text10, ourData.Text2);
            ourData.TextResult3 = CompareText(ourData.Text10, ourData.Text3);
            ourData.TextResult4 = CompareText(ourData.Text10, ourData.Text4);
            ourData.TextResult5 = CompareText(ourData.Text10, ourData.Text5);
            ourData.TextResult6 = CompareText(ourData.Text10, ourData.Text6);
            ourData.TextResult7 = CompareText(ourData.Text10, ourData.Text7);
            ourData.TextResult8 = CompareText(ourData.Text10, ourData.Text8);
            ourData.TextResult9 = CompareText(ourData.Text10, ourData.Text9);

            Form2 Graphic = new Form2();
            Graphic.Show();
        }
    }
}
