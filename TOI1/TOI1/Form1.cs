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
using System.Drawing.Imaging;

namespace TOI1
{
    public partial class Form1 : Form
    {
        public static File OurFile;

        public Form1()
        {
            OurFile = new File();
            InitializeComponent();            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            if(openFileDialog1.FileName == "")
            { return; }
            OurFile.FileName = openFileDialog1.FileName;
            OurFile.Size = System.IO.File.ReadAllBytes(OurFile.FileName);
            OurFile.OriginalSize = OurFile.Size.Length;
            pictureBox1.ImageLocation = OurFile.FileName;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Form2 Graphic = new Form2();
            Graphic.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            List<byte> RLEList = new List<byte>();
            List<byte> TimeList = new List<byte>();
            int Lenght = 0, i = 0, j = 0;            
            for(i = 0; i < OurFile.Size.Length;)
            {
                byte CurrentByte = OurFile.Size[i];
                if (i == OurFile.Size.Length - 1)
                {
                    RLEList.Add(CurrentByte);
                    break;
                }
                if (OurFile.Size[i + 1] == CurrentByte)
                {
                    for (j = i; j < OurFile.Size.Length; j++)
                    {
                        if (OurFile.Size[j] == CurrentByte)
                            Lenght++;
                        else
                            break;
                    }
                    i = j;
                }
                else
                {
                    for (j = i; j < OurFile.Size.Length; j++)
                    {
                        if (j == OurFile.Size.Length - 1)  // last symb.
                        {
                            RLEList.Add(OurFile.Size[j]);
                            break;
                        }
                        if (OurFile.Size[j + 1] != CurrentByte)
                        {
                            TimeList.Add(CurrentByte);
                            CurrentByte = OurFile.Size[j + 1];
                            i++;
                            Lenght++;
                        }
                        else
                            break;
                    }
                    RLEList.Add((byte)(0 + Lenght - 1));
                    RLEList.AddRange(TimeList);
                    Lenght = 0;
                    TimeList.Clear();
                }
                if (Lenght != 0)
                {
                    RLEList.Add((byte)(80 + Lenght - 2));
                    RLEList.Add(CurrentByte);
                    Lenght = 0;
                }
            }

            OurFile.RLESize = RLEList.Count;
            label1.Text = Convert.ToString(RLEList.Count);
            label1.Visible = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string ImageSizeString = "";
            for (int i = 0; i < OurFile.Size.Length; i++)
            {
                ImageSizeString += OurFile.Size[i];
            }

            #region LZW
            Dictionary<string, int> dictionary = new Dictionary<string, int>();
            for (int i = 0; i < 256; i++)
                dictionary.Add(i.ToString(), i);

            string w = string.Empty;
            List<int> compressed = new List<int>();

            foreach (char c in ImageSizeString)
            {
                string wc = w + c;
                if (dictionary.ContainsKey(wc))
                {
                    w = wc;
                }
                else
                {
                    compressed.Add(dictionary[w]);
                    dictionary.Add(wc, dictionary.Count);
                    w = c.ToString();
                }
            }
            if (!string.IsNullOrEmpty(w))
                compressed.Add(dictionary[w]);
            #endregion

            OurFile.LZWSize = compressed.Count;
            label2.Text = Convert.ToString(compressed.Count);
            label2.Visible = true;
        }

        #region JPEG
        private void button4_Click(object sender, EventArgs e)
        {
            string save = "TestPhotoQualityFifty.jpg";
            Bitmap bmp = new Bitmap(OurFile.FileName);
            ImageCodecInfo jpgEncoder = GetEncoder(ImageFormat.Jpeg);
            System.Drawing.Imaging.Encoder myEncoder =
            System.Drawing.Imaging.Encoder.Quality;
            EncoderParameters myEncoderParameters = new EncoderParameters(1);
            EncoderParameter myEncoderParameter = new EncoderParameter(myEncoder, 40L);
            myEncoderParameters.Param[0] = myEncoderParameter;
            bmp.Save(save, jpgEncoder, myEncoderParameters);
            OurFile.Size = System.IO.File.ReadAllBytes(save);
            OurFile.JPEGSize = OurFile.Size.Length;
            label3.Text = Convert.ToString(OurFile.JPEGSize);
            label3.Visible = true;
        }

        private ImageCodecInfo GetEncoder(ImageFormat format)
        {

            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageDecoders();

            foreach (ImageCodecInfo codec in codecs)
            {
                if (codec.FormatID == format.Guid)
                {
                    return codec;
                }
            }
            return null;
        }
        #endregion
    }
}
