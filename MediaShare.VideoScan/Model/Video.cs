using System;
using System.Collections.Generic;
using System.Text;
using static System.Net.Mime.MediaTypeNames;
using System.Linq;

namespace MediaShare.VideoScan.Model
{
    public class Video
    {
        public int Id { get; set; }

        /// <summary>
        /// 文件名称
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// 物理路径
        /// </summary>
        public string PhysicalPath { get; set; }

        /// <summary>
        /// 添加日期
        /// </summary>
        public DateTime AppendDate { get; set; }

        /// <summary>
        /// 忽略的视频
        /// </summary>
        public bool IsSkip { get; set; }

        /// <summary>
        /// 截图信息
        /// </summary>
        public virtual ICollection<Image> Images { get; set; }

    }
}
