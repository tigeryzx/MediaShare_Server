using System;
using System.Collections.Generic;
using System.Text;

namespace MediaShare.Media.Manager
{
    public class SettingKey
    {
        public string KeyName { get; private set; }

        private SettingKey(string keyName)
        {
            this.KeyName = keyName;
        }

        public static readonly SettingKey FtpServer = new SettingKey("FtpServer");

        public static readonly SettingKey ImageDir = new SettingKey("ImageDir");
    }
}
