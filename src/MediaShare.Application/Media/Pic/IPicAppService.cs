using Abp.Application.Services;
using Abp.Application.Services.Dto;
using MediaShare.Media.Pic.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace MediaShare.Media.Pic
{
    public interface IPicAppService : IApplicationService
    {
        PagedResultDto<PicDto> GetAll(PicPageRequestDto input);

        void Delete(EntityDto input);

        void Hidden(int id);

        void AddViewRecord(int id);
    }
}
