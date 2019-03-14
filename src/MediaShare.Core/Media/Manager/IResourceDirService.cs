using Abp.Dependency;
using Abp.Domain.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace MediaShare.Media.Manager
{
    public interface IResourceDirService : IDomainService
    {
        string GetVideoFtpPath(Video video);
    }
}
