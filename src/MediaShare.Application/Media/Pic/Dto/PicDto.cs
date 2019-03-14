using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace MediaShare.Media.Pic.Dto
{
    [AutoMapFrom(typeof(Picture))]
    public class PicDto : EntityDto
    {
        public string FileName { get; set; }

        public string RealPath { get; set; }

        public int Width { get; set; }

        public int Height { get; set; }

        public int Size { get; set; }

        public string Title { get; set; }

        public DateTime CreateDate { get; set; }

        public bool IsHidden { get; set; }

        public bool HasFav { get; set; }

        public int ViewCount { get; set; }

        public List<PicTagDto> Tags { get; set; }
    }
}
