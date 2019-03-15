using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediaShare.VideoScan.Model;

namespace MediaShare.VideoScan.PicFilter
{
    public class PicSizeFilter : IPicFilter
    {
        int _MinWidth;
        int _MinHeight;

        public PicSizeFilter(int minWidth,int minHeight)
        {
            this._MinWidth = minWidth;
            this._MinHeight = minHeight;
        }

        public bool Filter(Picture picture)
        {
            if (picture.Width < this._MinWidth)
                return false;

            if (picture.Height < this._MinHeight)
                return false;

            return true;
        }
    }
}
