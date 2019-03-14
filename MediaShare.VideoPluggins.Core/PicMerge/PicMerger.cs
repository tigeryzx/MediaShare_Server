using MediaShare.VideoPluggins.Core.Common.Loger;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaShare.VideoPluggins.Core.PicMerge
{
    /// <summary>
    /// 图片合并器
    /// </summary>
    public class PicMerger
    {
        protected ILoger _loger;

        public PicMerger()
        {
            //日志对象
            this._loger = LogerFactory.Instance.GetLoger();
        }

        /// <summary>
        /// 合并图片
        /// </summary>
        /// <param name="opt"></param>
        public void Merge(PicMergeJobOptions opt)
        {
            List<string> FilePaths = opt.SourcePicPath;

            PixelFormat? SourceFormat = null;

            List<Bitmap> NewPicList = new List<Bitmap>();

            foreach (var p in FilePaths)
            {
                Bitmap oldImage = (Bitmap)Bitmap.FromFile(p);

                if (SourceFormat == null)
                    SourceFormat = oldImage.PixelFormat;

                //如果没设置高宽取第一张的
                if (opt.Width == -1 || opt.Height == -1)
                {
                    opt.Width = oldImage.Width;
                    opt.Height = oldImage.Height;
                }

                if (oldImage.Width > opt.Width || oldImage.Height > opt.Height)
                    continue;

                Bitmap newImage = null;
                if (oldImage.Width < opt.Width || oldImage.Height < opt.Height || opt.PicMargin > 0)
                {
                    newImage = new Bitmap(opt.Width + opt.PicMargin * 2, opt.Height + opt.PicMargin * 2, oldImage.PixelFormat);
                    //从指定的System.Drawing.Image创建新的System.Drawing.Graphics        
                    Graphics g = Graphics.FromImage(newImage);

                    var newXpoint = opt.Width / 2 - oldImage.Width / 2 + opt.PicMargin;
                    var newYpoint = opt.Height / 2 - oldImage.Height / 2 + opt.PicMargin;

                    g.DrawImage(oldImage, newXpoint, newYpoint, oldImage.Width, oldImage.Height);      // g.DrawImage(imgBack, 0, 0, 相框宽, 相框高); 
                }
                else
                    newImage = oldImage;

                NewPicList.Add(newImage);

                //释放原文件资源
                oldImage.Dispose();
            }

            if (NewPicList.Count <= 0)
                return;

            int maxIcon = NewPicList.Count;
            int picWidth = (opt.Width + opt.PicMargin * 2) * opt.RowCount;
            int picHeight = Convert.ToInt32(Math.Ceiling(Convert.ToDecimal(maxIcon) / Convert.ToDecimal(opt.RowCount))) * (opt.Height + opt.PicMargin * 2);
            Bitmap bigPic = new Bitmap(picWidth, picHeight, SourceFormat.Value);
            
            Graphics spicGraphics = Graphics.FromImage(bigPic);

            int currentLine = 0;
            int currentColumn = 0;
            for (int i = 0; i < NewPicList.Count(); i++)
            {
                var spic = NewPicList[i];

                var xpoint = currentColumn * spic.Width;
                var ypoint = currentLine * spic.Height;

                spicGraphics.DrawImage(spic, xpoint, ypoint, spic.Width, spic.Height);

                if (currentColumn == opt.RowCount - 1)
                    currentColumn = 0;
                else
                    currentColumn++;

                //每够一行数据加一个单位高度
                if ((i + 1) % opt.RowCount == 0)
                    currentLine++;
            }

            bigPic.Save(opt.OutPutPicPath,ImageFormat.Jpeg);
            this._loger.Info(string.Format("图片合并完成!保存于{0}!", opt.OutPutPicPath));

            foreach (var item in NewPicList)
                item.Dispose();
            spicGraphics.Dispose();

            //清理生成源
            if(opt.DeleteSourcePic)
            {
                foreach(var item in opt.SourcePicPath)
                {
                    if (!File.Exists(item))
                        continue;

                    File.Delete(item);
                }

                this._loger.Info("清理合并源成功!");
            }
        }
    }
}
