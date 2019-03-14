using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace MediaShare.Media.Dto
{
    [AutoMapFrom(typeof(Video))]
    public class VideoDto : EntityDto
    {
        /// <summary>
        /// 截图信息
        /// </summary>
        public virtual List<ImageDto> Images { get; set; }

        /// <summary>
        /// 收藏信息
        /// </summary>
        public virtual List<FavoriteDto> Favorite { get; set; }

        /// <summary>
        /// 文件名称
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// Ftp路径
        /// </summary>
        public string FtpPath { get; set; }

        /// <summary>
        /// 添加日期
        /// </summary>
        public DateTime AppendDate { get; set; }

        /// <summary>
        /// 查看次数
        /// </summary>
        public int ViewCount { get; set; }

        /// <summary>
        /// 播放次数
        /// </summary>
        public int PlayCount { get; set; }
    }
}
