using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace MediaShare.Media.Dto
{
    [AutoMapFrom(typeof(Image))]
    public class ImageDto :EntityDto
    {
        public bool IsStoryCascade { get; set; }

        public int OrderCode { get; set; }

        public int Width { get; set; }

        public int Height { get; set; }

        public bool IsCover { get; set; }
    }
}
