using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TOI1
{
    public class File
    {
        public string FileName { get; set; }
        public byte[] Size { get; set; }

        public int OriginalSize { get; set; }
        public int RLESize { get; set; }
        public int LZWSize { get; set; }
        public int JPEGSize { get; set; }

    }
}
