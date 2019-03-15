using MediaShare.VideoScan.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaShare.VideoScan.PicFilter
{
    public interface IPicFilter
    {
        bool Filter(Picture picture);
    }
}
