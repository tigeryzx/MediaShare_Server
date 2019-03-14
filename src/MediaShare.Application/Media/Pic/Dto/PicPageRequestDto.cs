﻿using Abp.Application.Services.Dto;
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

        /// <summary>
        /// 按常看排序
        /// </summary>
        public bool? IsHotPlay { get; set; }

        /// <summary>
        /// 按查看历史排序
        /// </summary>
        public bool? IsHistoryView { get; set; }

        /// <summary>
        /// 是否随机列表
        /// </summary>
        public bool? IsRandomList { get; set; }
    }
}
