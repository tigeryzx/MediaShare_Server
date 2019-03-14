using MediaShare.VideoPluggins.Core.PicMerge;
using MediaShare.VideoPluggins.Core.Screenshot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaShare.VideoPluggins.Core.StoryCascade
{
    public class StoryCascadeJobOptions 
    {
        /// <summary>
        /// 输入文件路径
        /// </summary>
        public string InputFilePath { get; set; }

        /// <summary>
        /// 图片个数
        /// </summary>
        public int PicCount { get; set; }

        /// <summary>
        /// 输出文件
        /// </summary>
        public string OutPutPicPath { get; set; }
    }
}
