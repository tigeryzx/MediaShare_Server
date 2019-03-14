using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaShare.VideoPluggins.Core.PicMerge
{
    /// <summary>
    /// 图片合并参数
    /// </summary>
    public class PicMergeJobOptions
    {
        private int _Width = -1;

        /// <summary>
        /// 宽度
        /// </summary>
        public int Width 
        {
            get
            {
                return this._Width;
            }
            set
            {
                this._Width = value;
            }
        }

        private int _Height = -1;

        /// <summary>
        /// 高度
        /// </summary>
        public int Height 
        {
            get
            {
                return this._Height;
            }
            set
            {
                this._Height = value;
            }
        }

        /// <summary>
        /// 图片边距
        /// </summary>
        public int PicMargin { get; set; }

        /// <summary>
        /// 每个输出个数
        /// </summary>
        public int RowCount { get; set; }

        /// <summary>
        /// 输出文件
        /// </summary>
        public string OutPutPicPath { get; set; }

        /// <summary>
        /// 合并后删除源
        /// </summary>
        public bool DeleteSourcePic { get; set; }

        /// <summary>
        /// 源图片列表
        /// </summary>
        public List<string> SourcePicPath { get; set; }
    }
}
