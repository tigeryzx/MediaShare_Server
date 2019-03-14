using MediaShare.VideoPluggins.Core.Common.Loger;
using MediaShare.VideoScan.DB;
using MediaShare.VideoScan.Model;
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
    public partial class CoverManager : Form
    {
        public const string SCAN_SETTING_ALL = "SCAN_SETTING_ALL";
        public const string SCAN_SETTING_PATH = "SCAN_SETTING_PATH";

        public const string CREATE_SETTING_NOIMG = "CREATE_SETTING_NOIMG";
        public const string CREATE_SETTING_OVERRIDE = "CREATE_SETTING_OVERRIDE";


        public string CURRENT_SCAN_SETTING = string.Empty;
        public string CURRENT_CREATE_SETTING = string.Empty;

        ILoger _loger;
        VScanDB _DB;

        public CoverManager()
        {
            InitializeComponent();

            this.rbScanAll.Tag = SCAN_SETTING_ALL;
            this.rbScanPath.Tag = SCAN_SETTING_PATH;

            this.rbCreateNoImg.Tag = CREATE_SETTING_NOIMG;
            this.rbOverride.Tag = CREATE_SETTING_OVERRIDE;

            this.CURRENT_SCAN_SETTING = SCAN_SETTING_ALL;
            this.CURRENT_CREATE_SETTING = CREATE_SETTING_NOIMG;

            this._DB = new VScanDB();

            LogerFactory.Instance.SetLoger(new WinLoger(this.txtLog));
            _loger = LogerFactory.Instance.GetLoger();
        }

        private void rbScan_CheckedChanged(object sender, EventArgs e)
        {
            var setting = ((string)((RadioButton)sender).Tag);
            if (setting == SCAN_SETTING_ALL)
            {
                this.txtScanPath.Enabled = false;
            }
            else if (setting == SCAN_SETTING_PATH)
            {
                this.txtScanPath.Enabled = true;
            }
            CURRENT_SCAN_SETTING = setting;
        }

        public string GetSetting(string settingName)
        {
            return this._DB.MSettings.Single(x => x.SettingName == settingName).SettingValue;
        }

        private void rbCreate_CheckedChanged(object sender, EventArgs e)
        {
            var setting = ((string)((RadioButton)sender).Tag);
            CURRENT_CREATE_SETTING = setting;
        }

        private void btnRun_Click(object sender, EventArgs e)
        {
            List<VideoCoverInfo> videoList = new List<VideoCoverInfo>();

            this.txtLog.Text = string.Empty;

            #region 找出扫描的视频

            if (CURRENT_SCAN_SETTING == SCAN_SETTING_ALL)
            {
                var srcVideoInfo = this._DB.Videos
                    .Where(x => x.IsSkip != true)
                    .ToList();

                foreach (var item in srcVideoInfo)
                {
                    videoList.Add(new VideoCoverInfo 
                    {
                        VideoInfo = item,
                        SrcVideoPath = item.PhysicalPath
                    });
                }
            }
            else if (CURRENT_SCAN_SETTING == SCAN_SETTING_PATH)
            {
                string[] scanPaths = this.txtScanPath.Text
                    .Replace("\r\n", string.Empty)
                    .Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                var scanTypes = GetSetting("ScanType").Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);

                if (scanPaths == null || scanPaths.Length <= 0)
                {
                    MessageBox.Show("未设置扫描目录!");
                    return;
                }

                List<string> videoFilePath = ScanHelper.ScanFilePath(scanPaths, scanTypes);
                foreach (var videoPath in videoFilePath)
                {
                    // 默认以全路径找
                    var videoInfo = this._DB.Videos.SingleOrDefault(x => x.PhysicalPath == videoPath);
                    // 如果全路径匹配不到则以文件名找
                    if (videoInfo == null)
                    {
                        var videoFileName = Path.GetFileName(videoPath);
                        videoInfo = this._DB.Videos.FirstOrDefault(x => x.PhysicalPath.EndsWith(videoFileName));
                    }

                    if (videoInfo == null)
                    {
                        this._loger.Info(string.Format("在数据库中找不到[{0}]的视频信息，跳过处理。", videoPath));
                    }

                    //if (videoInfo.IsSkip == true)
                    //{
                    //    this._loger.Info(string.Format("[{0}]该视频在数据库中被标记为[跳过]", videoPath));
                    //    continue;
                    //}

                    videoList.Add(new VideoCoverInfo()
                    {
                        VideoInfo = videoInfo,
                        SrcVideoPath = videoPath
                    });
                }
            } 
            #endregion

            #region 处理找出来的视频

            var imgDir = GetSetting("ImageDir");
            var isOverride = (CURRENT_CREATE_SETTING == CREATE_SETTING_OVERRIDE);

            if (videoList != null && videoList.Count > 0)
            {
                foreach (var videoinfo in videoList)
                {
                    var coverInfo = videoinfo.VideoInfo.Images.SingleOrDefault(x => x.IsCover == true);

                    // 封面为空则取第一图为封面
                    if (coverInfo == null)
                        coverInfo = videoinfo.VideoInfo.Images.OrderBy(x => x.OrderCode).FirstOrDefault();

                    if (coverInfo == null)
                    {
                        this._loger.Info(string.Format("[{0}]没有设置封面信息或第一图找不到，跳过处理。", videoinfo.SrcVideoPath));
                        continue;
                    }

                    string videoFileName = Path.GetFileNameWithoutExtension(videoinfo.SrcVideoPath);
                    string videoDir = Path.GetDirectoryName(videoinfo.SrcVideoPath);

                    string coverFullPath = Path.Combine(imgDir, coverInfo.Path);
                    string coverTargetFullPath = Path.Combine(videoDir, videoFileName + "-1.jpg");

                    // 如果不是覆盖模式并且已经存在封面则跳过
                    if (!isOverride && File.Exists(coverTargetFullPath))
                    {
                        this._loger.Info(string.Format("[{0}]封面已存在，跳过处理。", videoinfo.VideoInfo.PhysicalPath));
                        continue;
                    }
                        

                    if (!File.Exists(coverFullPath))
                    {
                        this._loger.Info(string.Format("[{0}]没有设置封面信息，跳过处理。", videoinfo.VideoInfo.PhysicalPath));
                        continue;
                    }

                    // 复制并覆盖封面到指定目录下的封面
                    File.Copy(coverFullPath, coverTargetFullPath, isOverride);
                    this._loger.Info(string.Format("[{0}]生成封面成功!", coverTargetFullPath));
                }
            }
            else
            {
                this._loger.Info("未找到任何视频信息。");
                return;
            }

            this._loger.Info("处理完成。");

            #endregion
        }
    }
}
