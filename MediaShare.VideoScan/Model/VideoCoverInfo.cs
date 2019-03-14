using MediaShare.VideoScan.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaShare.VideoScan.Model
{
    public class VideoCoverInfo
    {
        /// <summary>
        /// 视频原始路径
        /// </summary>
        public string SrcVideoPath { get; set; }

        /// <summary>
        /// 对应的视频信息
        /// </summary>
        public Video VideoInfo { get; set; }
    }
}
