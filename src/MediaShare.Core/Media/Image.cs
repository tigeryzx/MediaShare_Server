using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MediaShare.Media
{
    public class Image :Entity
    {
        public virtual Video Video { get; set; }

        public string Path { get; set; }

        public bool IsStoryCascade { get; set; }

        public int OrderCode { get; set; }

        public int Width { get; set; }

        public int Height { get; set; }

        public bool IsCover { get; set; }
    }
}
