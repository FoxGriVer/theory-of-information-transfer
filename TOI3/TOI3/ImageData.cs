using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace TOI3
{
    public class ImageData
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public int numberOfCells { get; set; }

        public Bitmap Picture { get; set; }
    }
}
