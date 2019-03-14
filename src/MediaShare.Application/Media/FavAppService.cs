using System;
using System.Collections.Generic;
using System.Text;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.ObjectMapping;
using MediaShare.Media.Dto;
using MediaShare.Media.Manager;
using Abp.Linq;
using System.Linq;
using Abp.Runtime.Session;

namespace MediaShare.Media
{
    public class FavAppService : IFavAppService
    {
        private readonly IFavService _favService;
        private readonly IObjectMapper _objectMapper;
        private readonly IRepository<Favorite> _favoriteRepo;
        private readonly IAbpSession _abpSession;
        private readonly IRepository<Video> _videoRepo;

        public FavAppService(
            IFavService favService,
            IObjectMapper objectMapper,
            IRepository<Favorite> favoriteRepo,
            IAbpSession abpSession,
            IRepository<Video> videoRepo
            )
        {
            this._favService = favService;
            this._objectMapper = objectMapper;
            this._favoriteRepo = favoriteRepo;
            this._abpSession = abpSession;
            this._videoRepo = videoRepo;
        }

        public FavoriteDto Create(string favName)
        {
            var fav = this._favService.AddNewFavorite(favName);
            return this._objectMapper.Map<FavoriteDto>(fav);
        }

        public void Delete(EntityDto input)
        {
            this._favService.DeleteFav(input.Id);
        }

        public List<FavoriteDto> FavoriteVideo(FavoriteVideoDto input)
        {
            var video = this._videoRepo.Get(input.VideoId);
            var favList = this._favoriteRepo
                .GetAll()
                .Where(x => input.FavIds.Contains(x.Id));

            foreach (var fav in favList)
                this._favService.LikeVideo(fav, video);

            return this._objectMapper.Map<List<FavoriteDto>>(favList);
        }

        public PagedResultDto<FavoriteDto> GetCurrentUserAllFav()
        {
            var favList = this._favoriteRepo
                .GetAll()
                .Where(x => x.User.Id == this._abpSession.UserId);

            return new PagedResultDto<FavoriteDto>()
            {
                TotalCount = favList.Count(),
                Items = this._objectMapper.Map<List<FavoriteDto>>(favList)
            };
        }

        public List<FavoriteDto> UnFavoriteVideo(FavoriteVideoDto input)
        {
            var video = this._videoRepo.Get(input.VideoId);
            var favList = this._favoriteRepo
                .GetAll()
                .Where(x => input.FavIds.Contains(x.Id));

            foreach (var fav in favList)
                this._favService.UnLikeVideo(fav, video);
            return this._objectMapper.Map<List<FavoriteDto>>(favList);
        }

        public FavoriteDto Update(FavoriteDto input)
        {
            var fav = this._objectMapper.Map<Favorite>(input);
            return this._objectMapper.Map<FavoriteDto>(this._favoriteRepo.Update(fav));
        }
    }
}
