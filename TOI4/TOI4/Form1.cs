using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Limilabs.Mail;
using Limilabs.Client.IMAP;

namespace TOI4
{
    public partial class Form1 : Form
    {
        List<long> spamMessagesList;
        List<long> inputMassageList;
        int numberOfProcess = 1;

        List<string> finalWordsList = new List<string>();
        List<int> counts = new List<int>();

        List<string> spam = new List<string>();
        List<int> spamcounts = new List<int>();
        List<double> persent = new List<double>();
        List<double> probability = new List<double>();

        public Form1()
        {
            InitializeComponent();
        }

        void ConnectToEmail()
        {
            using (Imap ourImap = new Imap())
            {
                TextBoxOfMessages.Text += "       Соединение установлено" + "\r\n";              
                ourImap.Connect("imap.***.ru");
                ourImap.Login("YourMail", "MailPassword");
                ourImap.Select("Спам");

                spamMessagesList = ourImap.Search(Flag.All);
                TextBoxOfMessages.Text += "Писем в папке \"Спам\" : " + spamMessagesList.Count + "\r\n";

                foreach (long id in spamMessagesList)
                {
                    IMail email = new MailBuilder().CreateFromEml(ourImap.GetMessageByUID(id));
                    CleanFromSymbols(email.Text); // убрал email.Subject т.к. кривые символы            
                }

                ourImap.SelectInbox();
                inputMassageList = ourImap.Search(Flag.Flagged);
                TextBoxOfMessages.Text += "Отмечено писем : " + inputMassageList.Count + "\r\n";

                ourImap.Close();
            }
        }

        private void Connect_Click(object sender, EventArgs e)
        {
            ConnectToEmail();
            CountingProcess();
        }

        private void ButtonCheck_Click(object sender, EventArgs e)
        {
            Compare();
        }

        private void Compare()
        {
            if (numberOfProcess <= inputMassageList.Count)
            {
                finalWordsList.Clear();
                listBox2.Items.Clear();
                probability.Clear();

                using (Imap ourImap2 = new Imap())
                {
                    ourImap2.Connect("imap.***.ru");
                    ourImap2.Login("YourMail", "MailPassword");
                    ourImap2.SelectInbox();

                    inputMassageList = ourImap2.Search(Flag.Flagged);
                    long id = inputMassageList[numberOfProcess - 1];
                    IMail email = new MailBuilder().CreateFromEml(ourImap2.GetMessageByUID(id));
                    TextBoxOfMessages.Text += "Письмо с датой : " + email.Date + "\r\n";
                    CleanFromSymbols(email.Text);                              

                    for (int i = 0; i < spam.Count; i++)
                        for (int j = 0; j < finalWordsList.Count; j++)
                            if (LevenshteinDistance(spam[i], finalWordsList[j]) < 4)
                            {
                                probability.Add(persent[i]);
                            }
                }
                numberOfProcess++;
                BayesMethod();
            }
            else
            {
                TextBoxOfMessages.Text += "Список пуст" + "\r\n";
                TextBoxOfMessages.Text += "       Соединение отключено" + "\r\n";
            }
        }

        private void CountingProcess()
        {
            for (int i = 0; i < finalWordsList.Count; i++)
            {
                counts.Add(0);
            }
            DeleteSameWords();
            int count = 0;
            for (int i = 0; i < finalWordsList.Count; i++)
            {
                count = 0;
                for (int j = 0; j < finalWordsList.Count; j++)
                    if (LevenshteinDistance(finalWordsList[i], finalWordsList[j]) < 4)
                    {
                        counts[i] = count;
                        count++;
                    }
            }
            for (int i = 0; i < counts.Count; i++)
                if (counts[i] > 0)
                    listBox1.Items.Add(finalWordsList[i] + " " + counts[i]);
            int max = 0;
            for (int i = 0; i < counts.Count; i++)
                if (counts[i] > max)
                {
                    max = counts[i];
                }
            max++;
            listBox2.Items.Clear();
            while (spam.Count < 50)
            {
                max--;
                for (int i = 0; i < finalWordsList.Count; i++)
                    if ((counts[i] == max) && (spam.Count < 50))
                    {
                        spam.Add(finalWordsList[i]);
                        spamcounts.Add(counts[i]);
                    }
            }

            for (int i = 0; i < spam.Count; i++)
                listBox2.Items.Add(spam[i] + " " + spamcounts[i]);

            CountProcents();
        }

        private void CleanFromSymbols(string inputString)
        {
            char[] separate_symbols = { ' ', ',', '.', ')', '(', ':', ';', '"', '!', '?', '/', '\r', '\n' };
            List<string> listOfWords = inputString.Split(separate_symbols).ToList<string>();

            foreach (string s in listOfWords)
            {
                if (s.Length > 3)
                {
                    listBox2.Items.Add(s);
                    finalWordsList.Add(s);
                }
            }
        }

        private void DeleteSameWords()
        {
            for (int i = 0; i < finalWordsList.Count; i++)
            {
                for (int j = 0; j < finalWordsList.Count; j++)
                    if ((finalWordsList[i] == finalWordsList[j]) && (i != j))
                    {
                        finalWordsList.RemoveAt(j);
                        counts.RemoveAt(j);
                    }
            }
        }

        private void CountProcents()
        {
            int count = 0;
            for (int i = 0; i < spam.Count; i++)
            {
                count += spamcounts[i];
            }
            for (int i = 0; i < spam.Count; i++)
            {
                persent.Add((double)spamcounts[i] * 100 / (double)count);
            }
        }

        private void BayesMethod()
        {
            double p = 0;
            double up = 1, down1 = 1, down2 = 1;
            for (int a = 0; a < probability.Count; a++)
            {
                if (probability.Count > 1)
                {
                    up *= probability[a];
                    down1 *= probability[a];
                    down2 *= (1 - probability[a]);
                    p = p + Math.Abs(up / (down1 - down2));

                }
                else p = probability[0];
            }

            double show = Math.Round(p, 2);
            TextBoxOfMessages.Text += "Вероятность Байеса : " + show.ToString() + "\r\n";
            if (show > 70)
                TextBoxOfMessages.Text += "- - - - - - Письмо спам ! - - - - - - \r\n";
            else
                TextBoxOfMessages.Text += "- - - - - Письмо не спам - - - - - \r\n";

        }

        private static int LevenshteinDistance(string string1, string string2)
        {
            if (string1 == null) throw new ArgumentNullException("string1");
            if (string2 == null) throw new ArgumentNullException("string2");
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
    }
}
