using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MediaShare.Media
{
    public class Setting :Entity
    {
        public string SettingName { get; set; }

        public string SettingValue { get; set; }
    }
}
