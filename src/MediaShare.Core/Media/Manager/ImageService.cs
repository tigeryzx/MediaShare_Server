using Abp.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.PixelFormats;
using MediaShare.Media.Pic;

namespace MediaShare.Media.Manager
{
    public class ImageService : IImageService
    {
        private readonly IRepository<Image> _imageRepo;
        private readonly ISettingService _settingService;
        private readonly IRepository<Picture> _pictureRepo;

        public ImageService(
            IRepository<Image> imageRepo,
            ISettingService settingService,
            IRepository<Picture> pictureRepo
        )
        {
            this._imageRepo = imageRepo;
            this._settingService = settingService;
            this._pictureRepo = pictureRepo;
        }

        public byte[] GetPicByte(int picId,int? maxWidth)
        {
            var picInfo = this._pictureRepo.Get(picId);
            if (picInfo != null)
                return this.GetImageByte(picInfo.RealPath, maxWidth);
            return null;
        }

        public byte[] GetVideoImageByte(int imageId, int? maxWidth)
        {
            var imageInfo = this._imageRepo.Get(imageId);
            if (imageInfo != null)
            {
                var imageRootPath = this._settingService.GetSettingValue(SettingKey.VideoImageDir);
                var imageFullPath = imageRootPath + imageInfo.Path.Replace("/", "\\");
                return this.GetImageByte(imageFullPath, maxWidth);
            }
            return null;
        }

        /// <summary>
        /// 获取图片二进制数据
        /// </summary>
        /// <param name="path">物理路径</param>
        /// <param name="maxWidth">最大宽度</param>
        /// <returns></returns>
        protected byte[] GetImageByte(string path, int? maxWidth)
        {
            if (System.IO.File.Exists(path))
            {
                var imageFullPath = path.Replace("/", "\\");
                var imageExt = Path.GetExtension(imageFullPath);

                // 原图输出
                if (!maxWidth.HasValue)
                    return System.IO.File.ReadAllBytes(imageFullPath);
                else
                {

                    // 处理缩略图
                    using (SixLabors.ImageSharp.Image<Rgba32> srcImg = SixLabors.ImageSharp.Image.Load(imageFullPath))
                    {
                        int resetWidth = maxWidth.Value;
                        if (resetWidth > srcImg.Width)
                            resetWidth = srcImg.Width;

                        // 缩细倍数
                        var imgP = (Convert.ToDouble(resetWidth) / Convert.ToDouble(srcImg.Width));
                        var resetHeight = Convert.ToInt32(Convert.ToDouble(srcImg.Height) * imgP);

                        srcImg.Mutate(x => x.Resize(resetWidth, resetHeight));

                        using (System.IO.MemoryStream ms = new MemoryStream())
                        {
                            srcImg.Save(ms, SixLabors.ImageSharp.ImageFormats.Jpeg);
                            byte[] imageByte = ms.ToArray();
                            return imageByte;
                        }

                    }
                }

            }
            return null;
        }
    }
}
