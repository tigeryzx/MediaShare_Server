using Abp.Application.Services;
using Abp.Application.Services.Dto;
using MediaShare.Media.Pic.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace MediaShare.Media.Pic
{
    public interface IPicTagAppService : IApplicationService
    {
        PicTagDto Create(string tagName);

        void Delete(int id);

        PicTagDto Update(PicTagDto input);

        List<PicTagDto> GetAll();

        List<PicTagDto> GetPicTags(int picId);

        void AddPicToTag(int id, int picId);

        void RemovePicFromTag(int id, int picId);
    }
}
