using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace MediaShare.Media.Dto
{
    [AutoMapTo(typeof(Favorite))]
    public class FavoriteDto :EntityDto
    {
        public string CategoryName { get; set; }

        public int VideoCount { get; set; }
    }
}
