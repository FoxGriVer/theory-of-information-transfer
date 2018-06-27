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

namespace TOI3
{
    public partial class Form1 : Form
    {
        public static ImageData dataAboutImage;
        PictureBox[] massOfPictureBoxes;

        public Form1()
        {
            InitializeComponent();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openDialog = new OpenFileDialog();

            openDialog.ShowDialog();
            if(openDialog.FileName == "")
            {
                return;
            }
            pictureBox1.ImageLocation = openDialog.FileName;
            dataAboutImage = new ImageData();
            dataAboutImage.Picture = new Bitmap(openDialog.FileName);
            dataAboutImage.Height = pictureBox1.Height;
            dataAboutImage.Width = pictureBox1.Width;
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            Form2 form2Epl = new Form2();
            form2Epl.Show();
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            massOfPictureBoxes = new PictureBox[dataAboutImage.numberOfCells * dataAboutImage.numberOfCells];

            int widthOfEachPictureBox = dataAboutImage.Width / dataAboutImage.numberOfCells;
            int heightOfEachPictureBox = dataAboutImage.Height / dataAboutImage.numberOfCells;

            int countX = 0;
            int countY = 0;
            for(int i = 0; i < massOfPictureBoxes.Length; i++)
            {
                massOfPictureBoxes[i] = new PictureBox();
                massOfPictureBoxes[i].Width = widthOfEachPictureBox;
                massOfPictureBoxes[i].Height = heightOfEachPictureBox;
                massOfPictureBoxes[i].Left = 0 + countX * massOfPictureBoxes[i].Width;
                massOfPictureBoxes[i].Top = 0 + countY * massOfPictureBoxes[i].Height;

                countX++;
                if (countX == dataAboutImage.numberOfCells)
                {
                    countX = 0;
                    countY++;
                }

                massOfPictureBoxes[i].Parent = pictureBox1;
                massOfPictureBoxes[i].BorderStyle = BorderStyle.Fixed3D;
                massOfPictureBoxes[i].SizeMode = PictureBoxSizeMode.StretchImage;
                massOfPictureBoxes[i].Show();

                massOfPictureBoxes[i].Click += new EventHandler(ClickOnOneOfPictures);
            }
            pictureBox1.Image = null;
            DrawPicture();
        }

        void ClickOnOneOfPictures(object sender, EventArgs e)
        {
            PictureBox pb = (PictureBox)sender;
            var IndexX = pb.Location.X / (dataAboutImage.Width / dataAboutImage.numberOfCells);
            var IndexY = pb.Location.Y / (dataAboutImage.Height / dataAboutImage.numberOfCells);

            int resultIndex = ((IndexY + 1) - 1) * dataAboutImage.numberOfCells + (IndexX + 1);

            SaveOneOfPictures(resultIndex - 1);

        }

        void DrawPicture()
        {
            int countX = 0;
            int countY = 0;
            for(int i = 0; i < massOfPictureBoxes.Length; i++)
            {
                int width = dataAboutImage.Picture.Width / dataAboutImage.numberOfCells;
                int height = dataAboutImage.Picture.Height / dataAboutImage.numberOfCells;
                massOfPictureBoxes[i].Image = dataAboutImage.Picture.Clone(new RectangleF(countX * width, countY * height, width, height), dataAboutImage.Picture.PixelFormat);
                countX++;
                if (countX == dataAboutImage.numberOfCells)
                {
                    countX = 0;
                    countY++;
                }
            }
        }

        void SaveOneOfPictures(int index)
        {
            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.Filter = "bitmap image (*.bmp)|*.bmp";
            if (saveDialog.ShowDialog() == DialogResult.OK)
            {
                Bitmap bitmap = new Bitmap(massOfPictureBoxes[index].Image);
                bitmap.Save(saveDialog.FileName);
            }
        }
    }
}
