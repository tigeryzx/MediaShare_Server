using Abp.Domain.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace MediaShare.Media.Manager
{
    public interface IFavService :IDomainService
    {
        Favorite AddNewFavorite(string favName);

        void UnLikeVideo(Favorite favorite, Video video);

        void LikeVideo(Favorite favorite, Video video);

        void DeleteFav(int id);
    }
}
