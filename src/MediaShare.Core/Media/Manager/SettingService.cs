using Abp.Domain.Repositories;
using Abp.Domain.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace MediaShare.Media.Manager
{
    public class SettingService : DomainService, ISettingService
    {
        private readonly IRepository<Setting> _settingRepo;

        public SettingService(IRepository<Setting> settingRepo)
        {
            this._settingRepo = settingRepo;
        }

        public string GetSettingValue(SettingKey key)
        {
            return this._settingRepo.Single(x => x.SettingName == key.KeyName).SettingValue;
        }
    }
}
