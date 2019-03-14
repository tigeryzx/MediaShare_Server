using Abp.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Abp.Linq;
using Abp.Domain.Services;

namespace MediaShare.Media.Manager
{
    public class ResourceDirService : DomainService, IResourceDirService
    {
        private IRepository<ResDir> _resDirRepo;
        private ISettingService _settingManager;

        public ResourceDirService(
            IRepository<ResDir> resDirRepo,
            ISettingService settingManager
            )
        {
            this._resDirRepo = resDirRepo;
            this._settingManager = settingManager;
        }

        protected List<ResDir> AllResDir { get; set; }

        protected string FtpPath { get; set; }

        public string GetVideoFtpPath(Video video)
        {
            if (this.AllResDir == null)
                this.AllResDir = this._resDirRepo.GetAllList();

            if (string.IsNullOrEmpty(this.FtpPath))
                this.FtpPath = this._settingManager.GetSettingValue(SettingKey.FtpServer);

            var videoPhysicalPath = video.PhysicalPath;

            var resDirInfo = this.AllResDir.Single(x => videoPhysicalPath.StartsWith(x.DirName));
            var rpFtp = resDirInfo.DirName.Substring(0, resDirInfo.DirName.LastIndexOf("\\") + 1);
            return videoPhysicalPath.Replace(rpFtp, this.FtpPath).Replace("\\", "/");
        }
    }
}
