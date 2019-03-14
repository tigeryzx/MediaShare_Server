using MediaShare.VideoPluggins.Core.Common.Loger;
using MediaShare.VideoPluggins.Core.Screenshot;
using MediaShare.VideoPluggins.Core.StoryCascade;
using MediaShare.VideoScan.DB;
using MediaShare.VideoScan.Model;
using MySql.Data.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MediaShare.VideoScan
{
    public partial class Form1 : Form
    {
        ILoger _loger;
        VScanDB _DB;

        public Form1()
        {
            InitializeComponent();

            LogerFactory.Instance.SetLoger(new WinLoger(this.txtLog));
            _loger = LogerFactory.Instance.GetLoger();

            this._DB = new VScanDB();

            var dirList = this._DB.ResDirs.ToList();
            foreach (var item in dirList)
            {
                this.txtResDirs.AppendText(string.Format("{0};\r\n", item.DirName));
            }

            this.txtOutputDir.Text = GetSetting("ImageDir");

            this.txtScanType.Text = GetSetting("ScanType");
        }

        public string GetSetting(string settingName)
        {
            return this._DB.MSettings.Single(x => x.SettingName == settingName).SettingValue;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            HanderVideo();
        }

        protected void HanderVideo()
        {
            string outputDir = this.txtOutputDir.Text;
            var fileList = (List<string>)this.lbResult.DataSource;

            if (fileList == null || fileList.Count <= 0)
                return;

            StoryCascadePicBuilder builder = new StoryCascadePicBuilder(new VideoScreenshotConfig()
            {
                FFProbeToolPath = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, @"ffmpeg-20170330-ad7aff0-win64-static\bin\")
            });

            for (var i = 0; i < fileList.Count; i++)
            {
                var item = fileList[i];

                //选中项
                this.lbResult.SelectedItem = item;

                var fileName = Path.GetFileNameWithoutExtension(item);
                var subPutDir = Path.Combine(outputDir, fileName);

                if (!Directory.Exists(subPutDir))
                    Directory.CreateDirectory(subPutDir);

                //已经在库中的则不再处理
                if (this._DB.Videos.Count(x => x.PhysicalPath == item) > 0)
                {
                    this._loger.Info(string.Format("已处理,跳过{0}。", item));
                    continue;
                }

                try
                {
                    //截取普通图
                    var normalPicFilePaths = builder.GetVideoScreenshot()
                        .Screenshot(new ScreenshotJobOptions()
                        {
                            StartScreenSecond = 120,
                            InputFilePath = item,
                            OutputDir = subPutDir,
                            PicCount = 10
                        });

                    //截取故事连图
                    var picFilePath = Path.Combine(subPutDir, string.Format("sclist_{0}.jpg", DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss")));

                    builder.Build(new StoryCascadeJobOptions
                    {
                        InputFilePath = item,
                        OutPutPicPath = picFilePath,
                        PicCount = 40
                    });

                    //保存信息
                    Video videoInfo = new Video();
                    videoInfo.FileName = fileName;
                    videoInfo.PhysicalPath = item;
                    videoInfo.Images = new List<MediaShare.VideoScan.Model.Image>();
                    videoInfo.AppendDate = DateTime.Now;

                    var isCover = true;
                    var picOrderCode = 1;

                    foreach (var imagePath in normalPicFilePaths)
                    {
                        var image = new MediaShare.VideoScan.Model.Image()
                        {
                            Path = imagePath.Replace(outputDir, string.Empty).Replace("\\", "/")
                        };

                        System.Drawing.Image rimage = System.Drawing.Image.FromFile(imagePath);
                        image.Width = rimage.Width;
                        image.Height = rimage.Height;
                        rimage.Dispose();

                        //第一个图片设置为封面
                        if (isCover)
                        {
                            image.IsCover = true;
                            isCover = false;
                        }
                        image.OrderCode = picOrderCode;
                        picOrderCode++;
                        videoInfo.Images.Add(image);
                    }

                    var bigImage = new MediaShare.VideoScan.Model.Image()
                    {
                        Path = picFilePath.Replace(outputDir, string.Empty).Replace("\\", "/"),
                        IsStoryCascade = true,
                        OrderCode = picOrderCode
                    };

                    System.Drawing.Image bimage = System.Drawing.Image.FromFile(picFilePath);
                    bigImage.Width = bimage.Width;
                    bigImage.Height = bimage.Height;
                    bimage.Dispose();

                    videoInfo.Images.Add(bigImage);

                    this._DB.Videos.Add(videoInfo);
                }
                catch (Exception ex)
                {
                    Video videoInfo = new Video();
                    videoInfo.FileName = fileName;
                    videoInfo.PhysicalPath = item;
                    videoInfo.AppendDate = DateTime.Now;
                    videoInfo.IsSkip = true;
                    this._DB.Videos.Add(videoInfo);

                    this._loger.Error(ex.Message);
                    continue;
                }
                finally
                {
                    this._DB.SaveChanges();
                    _loger.Info(string.Format("【总进度：{0}/{1}】", i + 1, fileList.Count));
                }

            }
        }

        private void btnScan_Click(object sender, EventArgs e)
        {
            string[] resDir = this.txtResDirs.Text.Replace("\r\n",string.Empty).Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
            string[] scanExt = this.txtScanType.Text.Split(new char[]{ '|'},StringSplitOptions.RemoveEmptyEntries);

            var fileList = ScanHelper.ScanFilePath(resDir, scanExt);
            var newFileList = new List<string>();

            //过滤已处理的
            foreach (var item in fileList)
            {
                //已经在库中的则不再处理
                if (this._DB.Videos.Count(x => x.PhysicalPath == item) > 0)
                    continue;
                newFileList.Add(item);
            }

            this.lbResult.DataSource = newFileList;
        }

        private void btnCover_Click(object sender, EventArgs e)
        {
            CoverManager coverManager = new CoverManager();
            coverManager.ShowDialog();
        }
    }
}
