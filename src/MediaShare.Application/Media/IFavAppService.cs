using Abp.Application.Services;
using Abp.Application.Services.Dto;
using MediaShare.Media.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace MediaShare.Media
{
    public interface IFavAppService : IApplicationService
    {
        PagedResultDto<FavoriteDto> GetCurrentUserAllFav();

        void Delete(EntityDto input);

        FavoriteDto Create(string favName);

        FavoriteDto Update(FavoriteDto input);

        List<FavoriteDto> FavoriteVideo(FavoriteVideoDto input);

        List<FavoriteDto> UnFavoriteVideo(FavoriteVideoDto input);
     
    }
}
