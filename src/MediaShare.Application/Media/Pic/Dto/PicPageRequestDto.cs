using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace MediaShare.Media.Pic.Dto
{
    public class PicPageRequestDto : PagedResultRequestDto
    {
        public string Key { get; set; }

        public string TagName { get; set; }

        public int? FavId { get; set; }

        public string ViewType { get; set; }
    }
}
