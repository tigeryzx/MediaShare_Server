using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using MediaShare.Media.Pic.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using MediaShare.Authorization.Users;

namespace MediaShare.Media.Pic
{
    public class PicFavAppService : ApplicationService, IPicFavAppService
    {
        private readonly IRepository<PicFav> _picFavRepo;
        private readonly IRepository<PicFavRelation> _picFavRelationRepo;
        private readonly IRepository<User, long> _userRepo;
        private readonly IRepository<Picture> _pictureRepo;

        public PicFavAppService(
            IRepository<PicFav> picFavRepo,
            IRepository<PicFavRelation> picFavRelationRepo,
            IRepository<User, long> userRepo,
            IRepository<Picture> pictureRepo
            )
        {
            this._picFavRepo = picFavRepo;
            this._picFavRelationRepo = picFavRelationRepo;
            this._userRepo = userRepo;
            this._pictureRepo = pictureRepo;
        }

        public PicFavDto Create(string favName)
        {
            var user = this._userRepo.Get(this.AbpSession.UserId.Value);
            var fav = this._picFavRepo.Insert(new PicFav()
            {
                Name = favName,
                User = user
            });
            return this.ObjectMapper.Map<PicFavDto>(fav);
        }

        public void Delete(int id)
        {
            this._picFavRelationRepo.Delete(x => x.Fav.Id == id);
            this._picFavRepo.Delete(x => x.Id == id);
        }

        public List<PicFavDto> GetUserAllFavs()
        {
            // TODO:处理封面路径
            var favs = this._picFavRepo.GetAll();
            return this.ObjectMapper.Map<List<PicFavDto>>(favs);
        }

        public List<PicFavDto> GetPicFavs(int picId)
        {
            var favs = this._picFavRelationRepo.GetAll()
                .Where(x => x.Picture.Id == picId && x.Fav.User.Id == this.AbpSession.UserId)
                .Select(x => x.Fav);
            return this.ObjectMapper.Map<List<PicFavDto>>(favs);
        }

        public PicFavDto Update(PicFavDto input)
        {
            var fav = this._picFavRepo.Get(input.Id);
            fav = this.ObjectMapper.Map(input, fav);

            if (input.CoverId.HasValue)
                fav.Cover = this._pictureRepo.Get(input.CoverId.Value);
            else
                fav.Cover = null;

            return this.ObjectMapper.Map<PicFavDto>(fav);
        }

        public void AddPicToFav(int id, int picId)
        {
            var fav = this._picFavRepo.Get(id);
            var pic = this._pictureRepo.Get(picId);
            this._picFavRelationRepo.Insert(new PicFavRelation()
            {
                Fav = fav,
                Picture = pic
            });
        }

        public void RemovePicFromFav(int id, int picId)
        {
            this._picFavRelationRepo.Delete(x => x.Fav.Id == id && x.Picture.Id == picId);
        }
    }
}
