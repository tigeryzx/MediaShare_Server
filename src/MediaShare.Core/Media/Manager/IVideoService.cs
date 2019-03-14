using Abp.Domain.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace MediaShare.Media.Manager
{
    public interface IVideoService :IDomainService
    {
        Video GetLuckVideo(int favId);

        Video GetLuckVideoInMyFav();

        Video GetLuckVideoInAll();

        void DeleteVideo(int videoId);

        Video SetVideoCover(int videoId, int imageId);

        int AddVideoViewRecord(int videoId, bool isPlay);
    }
}
