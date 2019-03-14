using System;
using System.Collections.Generic;
using System.Text;

namespace MediaShare.Media.Dto
{
    public class VideoViewRequestDto
    {
        public int VideoId { get; set; }

        public bool IsPlay { get; set; }
    }
}
