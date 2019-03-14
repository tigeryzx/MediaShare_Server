using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace MediaShare.Media.Dto
{
    public class FavoriteVideoDto
    {
        public int[] FavIds { get; set; }

        public int VideoId { get; set; }
    }
}
