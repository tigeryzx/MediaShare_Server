using Abp.Application.Services;
using System;
using System.Collections.Generic;
using System.Text;
using Abp.Application.Services.Dto;
using MediaShare.Media.Dto;

namespace MediaShare.Media
{
    public interface IVideoAppService : IApplicationService
    {
        PagedResultDto<VideoDto> GetAll(VideoPageRequestDto input);

        void Delete(EntityDto input);

        VideoDto GetLuckVideo(LuckVideoRequestDto input);

        VideoDto SetVideoCover(VideoCoverSettingDto input);

        int AddVideoViewRecord(VideoViewRequestDto input);
    }
}
