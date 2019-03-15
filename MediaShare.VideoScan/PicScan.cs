using MediaShare.VideoPluggins.Core.Common.Loger;
using MediaShare.VideoScan.DB;
using MediaShare.VideoScan.PicFilter;
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
    public partial class PicScan : Form
    {
        ILoger _loger;
        VScanDB _DB;

        public PicScan()
        {
            InitializeComponent();

            InitFromData();
        }

        private void InitFromData()
        {
            this._DB = new VScanDB();

            var dirList = new string[] 
            {
                @"D:\Program Files",
                GetSetting("PicScanPath")
            };

            foreach (var path in dirList)
            {
                if (!string.IsNullOrEmpty(path))
                {
                    if (!string.IsNullOrEmpty(this.txtResDirs.Text))
                        this.txtResDirs.AppendText("\r\n");
                    this.txtResDirs.AppendText(string.Format("{0};", path));
                }
            }
        }

        public string GetSetting(string settingName)
        {
            var setting = this._DB.MSettings.SingleOrDefault(x => x.SettingName == settingName);
            if (setting != null)
                return setting.SettingValue;
            else
                return null;
        }

        private async void btnScan_Click(object sender, EventArgs e)
        {
            string[] paths = this.txtResDirs.Text
                .Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var p in paths)
            {
                if (!string.IsNullOrEmpty(p) && !System.IO.Directory.Exists(p))
                {
                    MessageBox.Show(string.Format("路径不存在:{0}",p));
                    return;
                }
            }
            this.btnScan.Enabled = false;
            this.lbProcess.Text = "扫描中，请稍候……";
            var result = await Task.Run(() => ScanPic(paths, new IPicFilter[] {
                // 至少1K
                new PicFileLengthFilter(1000),
                // 至少 w 300 以上，H 300 以上
                new PicSizeFilter(300,300)
            }));

            this.btnScan.Enabled = true;
        }

        protected Task<List<string>> ScanPic(string[] paths, IPicFilter[] filters)
        {
            var result = ScanHelper.ScanFilePath(paths, new string[] { "*.jpg", "*.jpeg", "*.png", "*.bmp", "*.gif" });

            int count_success = 0;
            int count_failed = 0;
            int count_skip = 0;

            var changeProcess = new Action<int>((currentValue) =>
            {
                this.progressBar1.Maximum = result.Count;
                this.progressBar1.Value = currentValue;
                var precent = Math.Round((Convert.ToDouble(currentValue) / Convert.ToDouble(this.progressBar1.Maximum)) * 100);
                this.lbProcess.Text = string.Format("{0}/{1}({2})%", currentValue,
                    this.progressBar1.Maximum, precent);

                this.lbCount.Text = string.Format("成功:{0} 失败:{1} 跳过:{2}", count_success, count_failed, count_skip);
            });

            for (var i = 0; i < result.Count; i++)
            {
                Bitmap bmp = null;
                bool skip = false;
                bool error = false;
                var picPath = result[i];
                var currentIndex = i + 1;
                var pic = new Model.Picture()
                {
                    CreateDate = DateTime.Now,
                    FileName = System.IO.Path.GetFileName(picPath),
                    RealPath = picPath,
                    Title = System.IO.Path.GetFileNameWithoutExtension(picPath)
                };

                try
                {
                    // 收集信息
                    bmp = new Bitmap(picPath);
                    pic.Width = bmp.Width;
                    pic.Height = bmp.Height;
                    bmp.Dispose();

                    FileInfo fileInfo = new FileInfo(picPath);
                    pic.Size = fileInfo.Length;

                    // 过滤
                    if (filters != null && filters.Length > 0)
                    {
                        foreach (var f in filters)
                        {
                            var isAllow = f.Filter(pic);
                            if (!isAllow)
                            {
                                skip = true;
                                break;
                            }
                        }
                    }

                    // 判断已存在
                    skip = this._DB.Picture.Count(x => x.RealPath == pic.RealPath
                    || (
                        x.FileName == pic.FileName
                        && x.Width == pic.Width
                        && x.Height == pic.Height
                        && x.Size == pic.Size)) > 0;
                }
                catch
                {
                    pic.IsHidden = true;
                    error = true;
                }
                finally
                {
                    // 记录失败、成功、跳过的

                    // 写入
                    if (!skip && !error)
                    {
                        this._DB.Picture.Add(pic);
                        count_success++;
                    }
                    else if (error)
                    {
                        count_failed++;
                    }
                    else if (skip)
                    {
                        count_skip++;
                    }

                    if (currentIndex % 1000 == 0)
                        this._DB.SaveChanges();

                    Invoke(changeProcess, currentIndex);

                    if (bmp != null)
                        bmp.Dispose();
                }

            }

            this._DB.SaveChanges();

            return Task.FromResult(result);
        }
    }
}
