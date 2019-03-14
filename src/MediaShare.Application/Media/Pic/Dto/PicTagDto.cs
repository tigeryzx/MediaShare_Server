using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace MediaShare.Media.Pic.Dto
{
    [AutoMapFrom(typeof(PicTag))]
    public class PicTagDto :EntityDto
    {
        public string Name { get; set; }
    }
}
