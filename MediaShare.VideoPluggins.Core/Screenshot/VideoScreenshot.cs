using MediaShare.VideoPluggins.Core.Common.Loger;
using NReco.VideoConverter;
using NReco.VideoInfo;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaShare.VideoPluggins.Core.Screenshot
{
    /// <summary>
    /// 视频截图器
    /// </summary>
    public class VideoScreenshot
    {
        protected VideoScreenshotConfig _config;

        protected FFProbe _ffprobe;

        protected FFMpegConverter _ffMpeg;

        protected ILoger _loger;

        public VideoScreenshot(VideoScreenshotConfig options)
        {
            this._config = options;
            
            //分析器设置
            this._ffprobe = new FFProbe();
            _ffprobe.ToolPath = this._config.FFProbeToolPath;

            //视频转换器初始化
            this._ffMpeg = new FFMpegConverter();

            //日志对象
            this._loger = LogerFactory.Instance.GetLoger();
        }

        /// <summary>
        /// 进行截图
        /// </summary>
        /// <param name="opt">参数集</param>
        /// <returns>输出文件路径集合</returns>
        public List<string> Screenshot(ScreenshotJobOptions opt)
        {
            var mediaFile = opt.InputFilePath;
            var picCount = opt.PicCount;
            var cStartPosition = opt.StartScreenSecond;
            var outputDir = opt.OutputDir;
            

            var mediaInfo = this._ffprobe.GetMediaInfo(mediaFile);
            var totalTime = mediaInfo.Duration;

            var width = mediaInfo.Streams[0].Width;
            var height = mediaInfo.Streams[0].Height;

            if (width == -1)
            {
                width = mediaInfo.Streams[1].Width;
                height = mediaInfo.Streams[1].Height;
            }

            int picWidth = width;
            int picHeight = height;

            //只有指定的分辨率小于原图才进行缩放
            if ((opt.Width.HasValue && opt.Width < width) || (opt.Height.HasValue && opt.Height < height))
            {
                if (opt.Width.HasValue && opt.Height.HasValue)
                {
                    picWidth = opt.Width.Value;
                    picHeight = opt.Height.Value;
                }
                else if (opt.Width.HasValue)
                {
                    picWidth = opt.Width.Value;
                    picHeight = (height / (width / picWidth)) - 20;
                }
                else if (opt.Height.HasValue)
                {
                    picHeight = opt.Height.Value;
                    picWidth = (width / (height / picHeight)) - 50;
                }
            }

            var videoFrameSize = string.Format("{0}x{1}", picWidth, picHeight);

            var totalSeconds = totalTime.TotalSeconds;
            if (cStartPosition > totalSeconds)
                cStartPosition = 0;

            var cSpan = Math.Round(((totalTime.TotalSeconds - cStartPosition) / picCount), 0);
            var hostFileName = Guid.NewGuid().ToString().Replace("-", "_");

            if (!Directory.Exists(outputDir))
                Directory.CreateDirectory(outputDir);

            this._loger.Info("=====================================");
            this._loger.Info(string.Format("开始处理文件{0}", opt.InputFilePath));
            this._loger.Info(string.Format("准备向目录中输出文件{0}", outputDir));

            List<string> OutPutFileList = new List<string>();

            for (var i = 0; i < picCount; i++)
            {
                if (cStartPosition > totalSeconds)
                    break;

                var filename = string.Format("{0}_{1}.jpg", hostFileName, cStartPosition);
                var outputFileName = Path.Combine(outputDir, filename);
                Screenshot(opt.InputFilePath, outputFileName, cStartPosition, videoFrameSize);
                cStartPosition += cSpan;
                OutPutFileList.Add(outputFileName);

                this._loger.Info(string.Format("{0}/{1}:输出 {2}", i + 1, picCount, filename));
            }

            return OutPutFileList;
        }

        /// <summary>
        /// 截图指定秒数的一张图指定高宽的图片
        /// </summary>
        /// <param name="inputFilePath">文件路径</param>
        /// <param name="outputFilePath">图片保存路径</param>
        /// <param name="startScreenSecond">截图秒数</param>
        /// <param name="videoFrameSize">截图宽高 格式:200x300</param>
        public void Screenshot(string inputFilePath, string outputFilePath, double startScreenSecond)
        {
            Screenshot(inputFilePath, outputFilePath, startScreenSecond, null);
        }

        /// <summary>
        /// 截图指定秒数的一张图指定高宽的图片
        /// </summary>
        /// <param name="inputFilePath">文件路径</param>
        /// <param name="outputFilePath">图片保存路径</param>
        /// <param name="startScreenSecond">截图秒数</param>
        /// <param name="videoFrameSize">截图宽高 格式:200x300</param>
        public void Screenshot(string inputFilePath, string outputFilePath, double startScreenSecond, string videoFrameSize)
        {
            var setting = new ConvertSettings();

            setting.VideoFrameCount = new int?(1);
            setting.MaxDuration = new float?(1f);
            setting.Seek = (float)startScreenSecond;

            if (!string.IsNullOrEmpty(videoFrameSize))
                setting.VideoFrameSize = videoFrameSize;
            

            this._ffMpeg.ConvertMedia(inputFilePath, null, outputFilePath, Format.mjpeg, setting);
        }
    }
}
