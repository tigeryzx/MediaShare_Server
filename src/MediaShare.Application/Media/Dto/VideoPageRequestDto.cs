using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace MediaShare.Media.Dto
{
    public class VideoPageRequestDto : PagedResultRequestDto
    {
        public string VideoName { get; set; }

        public int? FavId { get; set; }

        /// <summary>
        /// 按常看排序
        /// </summary>
        public bool? IsHotPlay { get; set; }

        /// <summary>
        /// 按最近排序
        /// </summary>
        public bool? IsLatePlay { get; set; }

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
