using Abp.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.PixelFormats;

namespace MediaShare.Media.Manager
{
    public class ImageService : IImageService
    {
        private readonly IRepository<Image> _imageRepo;
        private readonly ISettingService _settingService;

        public ImageService(
            IRepository<Image> imageRepo,
            ISettingService settingService
        )
        {
            this._imageRepo = imageRepo;
            this._settingService = settingService;
        }


        public byte[] GetVideoImageByte(int imageId, int? maxWidth)
        {
            var imageInfo = this._imageRepo.Get(imageId);

            

            if (imageInfo != null)
            {
                var imageRootPath = this._settingService.GetSettingValue(SettingKey.ImageDir);
                var imageFullPath = imageRootPath + imageInfo.Path.Replace("/", "\\");
                var imageExt = Path.GetExtension(imageInfo.Path);

                if (System.IO.File.Exists(imageFullPath))
                {
                    // 原图输出
                    if(!maxWidth.HasValue)
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
                    
            }
            return null;
        }
    }
}
