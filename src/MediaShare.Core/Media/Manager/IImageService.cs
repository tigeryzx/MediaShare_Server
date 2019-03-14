using Abp.Domain.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace MediaShare.Media.Manager
{
    public interface IImageService :IDomainService
    {
        byte[] GetVideoImageByte(int imageId, int? maxWidth);
    }
}
