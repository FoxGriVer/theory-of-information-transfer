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

namespace TOI2._1
{
    public partial class Form1 : Form
    {
        OpenFileDialog dialogWindow;        
        List<string> words;
        List<string> dictionary;
        List<int> counts;
        string text;

        public Form1()
        {
            InitializeComponent();
            words = new List<string>();
            dictionary = new List<string>();
            counts = new List<int>();
            dialogWindow = new OpenFileDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            text = "";
            words.Clear();
            counts.Clear();
            textBox1.Text = "";
            dialogWindow.Filter = "Text files (*.txt)|*.txt|All files(*.*)|*.*";
            if (dialogWindow.ShowDialog() == DialogResult.OK)
            {
                Encoding enc = Encoding.GetEncoding(1251);
                string path = dialogWindow.FileName;
                string temp;
                StreamReader sr = new StreamReader(path, enc);
                temp = sr.ReadLine();
                text = temp;
                while (temp != null)
                {
                    temp = sr.ReadLine();
                    text += temp;
                }
                textBox1.Text = text;

                string[] word = text.Split(' ');

                foreach (string s in word)
                {
                    if (s.Length > 3)
                    {
                        string s1 = s;
                        s1 = RemoveSimbols(s1);//слова без пунктуации
                        words.Add(s1);
                    }
                }

                sr = new StreamReader(@"C:\Users\Pavel\Documents\Visual Studio 2015\Projects\TOI2.1\dictionary.txt", enc);
                string line = "";
                while (!sr.EndOfStream)
                {
                    line = sr.ReadLine();
                    dictionary.Add(line);
                }
                sr.Close();
                for (int i = 0; i < dictionary.Count; i++)
                    counts.Add(0);

                int count = 0;
                for (int i = 0; i < dictionary.Count; i++)
                {
                    count = 0;
                    for (int j = 0; j < words.Count; j++)
                        if (LevenshteinDistance(dictionary[i], words[j]) < 2)
                        {
                            counts[i] = count;
                            count++;
                        }
                }
                for (int i = 0; i < counts.Count; i++)
                    if (counts[i] > 0)
                        listBox1.Items.Add(dictionary[i] + " " + counts[i]);
            }
        }

        private static int LevenshteinDistance(string string1, string string2)
        {
            //if (string1 == null) throw new ArgumentNullException("string1");
            //if (string2 == null) throw new ArgumentNullException("string2");
            int diff;
            int[,] m = new int[string1.Length + 1, string2.Length + 1];

            for (int i = 0; i <= string1.Length; i++) { m[i, 0] = i; }
            for (int j = 0; j <= string2.Length; j++) { m[0, j] = j; }

            for (int i = 1; i <= string1.Length; i++)
            {
                for (int j = 1; j <= string2.Length; j++)
                {
                    diff = (string1[i - 1] == string2[j - 1]) ? 0 : 1;

                    m[i, j] = Math.Min(Math.Min(m[i - 1, j] + 1,
                                             m[i, j - 1] + 1),
                                             m[i - 1, j - 1] + diff);
                }
            }
            return m[string1.Length, string2.Length];
        }

        private static string RemoveSimbols(string words)
        {
            string word = words;
            while (word.Substring(word.Length - 1, 1) == "," || word.Substring(word.Length - 1, 1) == "(" 
                || word.Substring(word.Length - 1, 1) == ")" || word.Substring(word.Length - 1, 1) == "-" 
                || word.Substring(word.Length - 1, 1) == "!" || word.Substring(word.Length - 1, 1) == "»" 
                || word.Substring(word.Length - 1, 1) == "?" || word.Substring(word.Length - 1, 1) == "." || word.Substring(0, 1) == "«")
            {
                if (word.Substring(0, 1) == "«")
                    word = word.Substring(1, word.Length - 1);
                else word = word.Substring(0, word.Length - 1);
            }
            return word;
        }
    }
}
