using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace MediaShare.Media
{
    public class SysAppService : ISysAppService
    {
        public static DateTime? ShutdownDate;

        public void CancelShutdown()
        {
            var psi = new ProcessStartInfo("shutdown", "-a") { RedirectStandardOutput = true };
            var proc = Process.Start(psi);
            ShutdownDate = null;
        }

        public DateTime? GetShutdownDate()
        {
            return ShutdownDate;
        }

        public void ShutdownServer(int second)
        {
            var psi = new ProcessStartInfo("shutdown", "-s -t " + second) { RedirectStandardOutput = true };
            var proc = Process.Start(psi);
            ShutdownDate = DateTime.Now.AddSeconds(second);
        }
    }
}
