using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaShare.VideoScan.Model
{
    public class Picture
    {
        public int Id { get; set; }

        public string FileName { get; set; }

        public string RealPath { get; set; }

        public int Width { get; set; }

        public int Height { get; set; }

        public long Size { get; set; }

        public string Title { get; set; }

        public DateTime CreateDate { get; set; }

        public bool IsHidden { get; set; }
    }
}
