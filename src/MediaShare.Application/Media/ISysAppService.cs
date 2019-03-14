using Abp.Application.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace MediaShare.Media
{
    public interface ISysAppService : IApplicationService
    {
        void ShutdownServer(int second);

        void CancelShutdown();

        DateTime? GetShutdownDate();
    }
}
