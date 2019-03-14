using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace MediaShare.Media.Pic.Dto
{
    [AutoMapFrom(typeof(PicFav))]
    public class PicFavDto :EntityDto
    {
        public string Name { get; set; }

        public int? CoverId { get; set; }
    }
}
