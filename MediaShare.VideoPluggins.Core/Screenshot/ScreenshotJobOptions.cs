using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaShare.VideoPluggins.Core.Screenshot
{
    /// <summary>
    /// 截图参数
    /// </summary>
    public class ScreenshotJobOptions
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
        /// 输出文件目录
        /// </summary>
        public string OutputDir { get; set; }

        /// <summary>
        /// 截图宽
        /// </summary>
        public int? Width { get; set; }

        /// <summary>
        /// 截图高
        /// </summary>
        public int? Height { get; set; }

        private double _StartScreenSecond = 10;

        /// <summary>
        /// 开始截图秒数（默认10秒片开始）
        /// </summary>
        public double StartScreenSecond 
        {
            get
            {
                return this._StartScreenSecond;
            }
            set
            {
                this._StartScreenSecond = value;
            }
        }
    }
}
