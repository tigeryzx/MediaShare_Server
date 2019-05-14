using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediaShare.VideoScan.DB;
using MediaShare.VideoScan.Model;

namespace MediaShare.VideoScan.PicFilter
{
    public class PicExistsFilter : IPicFilter
    {
        VScanDB _DB;

        public PicExistsFilter(VScanDB db)
        {
            this._DB = db;
        }

        public bool Filter(Picture picture)
        {
            return this._DB.Picture.Count(x => x.RealPath == picture.RealPath) <= 0;
        }
    }
}
