using System;
using System.Collections.Generic;
using System.Text;

namespace MediaShare.VideoScan.Model
{
    public class Image
    {
        public int Id { get; set; }

        public virtual Video Video { get; set; }

        public int VideoId { get; set; }

        public string Path { get; set; }

        public bool IsStoryCascade { get; set; }

        public int OrderCode { get; set; }

        public int Width { get; set; }

        public int Height { get; set; }

        public bool IsCover { get; set; }
    }
}
