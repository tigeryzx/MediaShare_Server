using Abp.Domain.Repositories;
using Abp.Domain.Services;
using Abp.Runtime.Session;
using MediaShare.Authorization.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace MediaShare.Media.Manager
{
    public class FavService : DomainService, IFavService
    {
        private IRepository<Favorite> _favoriteRepo;
        private IAbpSession _abpSession;
        private IRepository<User, long> _userRepo;
        private IRepository<VideoFavRelation> _videoFavRelationRepo;

        public FavService(
            IRepository<Favorite> favoriteRepo,
            IAbpSession abpSession,
            IRepository<User, long> userRepo,
            IRepository<VideoFavRelation> videoFavRelationRepo
            )
        {
            this._favoriteRepo = favoriteRepo;
            this._abpSession = abpSession;
            this._userRepo = userRepo;
            this._videoFavRelationRepo = videoFavRelationRepo;
        }

        public Favorite AddNewFavorite(string favName)
        {
            var currentUser = this._userRepo.Get(this._abpSession.UserId.Value);
            return this._favoriteRepo.Insert(new Favorite()
            {
                CategoryName = favName,
                User = currentUser
            });
        }

        public void DeleteFav(int id)
        {
            this._videoFavRelationRepo.Delete(x => x.Favorite.Id == id);
            this._favoriteRepo.Delete(id);
        }

        public void LikeVideo(Favorite favorite, Video video)
        {
            favorite.VideoFavRelations.Add(new VideoFavRelation()
            {
                Favorite = favorite,
                Video = video
            });
        }

        public void UnLikeVideo(Favorite favorite, Video video)
        {
            this._videoFavRelationRepo.Delete(x => x.Favorite.Id == favorite.Id && x.Video.Id == video.Id);
        }
    }
}
