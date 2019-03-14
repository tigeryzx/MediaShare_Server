using Abp.Application.Services;
using Abp.Application.Services.Dto;
using MediaShare.Media.Pic.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace MediaShare.Media.Pic
{
    public interface IPicFavAppService :IApplicationService
    {
        PicFavDto Create(string favName);

        void Delete(int id);

        PicFavDto Update(PicFavDto input);

        List<PicFavDto> GetUserAllFavs();

        List<PicFavDto> GetPicFavs(int picId);

        void AddPicToFav(int id, int picId);

        void RemovePicFromFav(int id, int picId);
    }
}
