using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaShare.VideoScan
{
    public static class ScanHelper
    {
        public static List<string> ScanFilePath(string[] resDir, string[] scanExt)
        {
            List<string> allVideoPath = new List<string>();

            foreach (var p in resDir)
            {
                if (!Directory.Exists(p))
                    continue;

                foreach (var ext in scanExt)
                    allVideoPath.AddRange(Directory.GetFiles(p, ext, SearchOption.AllDirectories));
            }

            return allVideoPath;
        }
    }
}
