using MediaShare.VideoPluggins.Core.PicMerge;
using MediaShare.VideoPluggins.Core.Screenshot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaShare.VideoPluggins.Core.StoryCascade
{
    /// <summary>
    /// 剧情流图片构建
    /// </summary>
    public class StoryCascadePicBuilder
    {
        protected VideoScreenshot _videoScreenshot;

        protected PicMerger _picMerger;

        public StoryCascadePicBuilder(VideoScreenshotConfig videoScreenshotConfig)
        {
            this._videoScreenshot = new VideoScreenshot(videoScreenshotConfig);
            this._picMerger = new PicMerger();
        }

        public VideoScreenshot GetVideoScreenshot()
        {
            return this._videoScreenshot;
        }

        public void Build(StoryCascadeJobOptions opt)
        {
            string windowTempDir = System.Environment.GetEnvironmentVariable("TEMP");

            var picList = _videoScreenshot.Screenshot(new ScreenshotJobOptions()
            {
                InputFilePath = opt.InputFilePath,
                OutputDir = windowTempDir,
                PicCount = opt.PicCount,
                Width = 300
            });

            //合并图片
            PicMerger picMerger = new PicMerger();
            picMerger.Merge(new PicMergeJobOptions()
            {
                OutPutPicPath = opt.OutPutPicPath,
                PicMargin = 5,
                SourcePicPath = picList,
                RowCount = 5,
                DeleteSourcePic = true
            });
        }

    }
}

