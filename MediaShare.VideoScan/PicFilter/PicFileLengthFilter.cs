using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediaShare.VideoScan.Model;

namespace MediaShare.VideoScan.PicFilter
{
    public class PicFileLengthFilter : IPicFilter
    {
        int _minFileByteLength;

        public PicFileLengthFilter(int minFileByteLength)
        {
            this._minFileByteLength = minFileByteLength;
        }

        public bool Filter(Picture picture)
        {
            if (picture.Size < this._minFileByteLength)
                return false;
            return true;
        }
    }
}
