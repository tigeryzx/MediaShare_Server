using System;
using System.Collections.Generic;
using System.Text;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.Runtime.Session;
using MediaShare.Authorization.Users;
using MediaShare.Media.Pic.Dto;
using MediaShare.Media.Pic.Manager;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Abp.ObjectMapping;
using Abp.Application.Services;

namespace MediaShare.Media.Pic
{
    public class PicAppService : ApplicationService, IPicAppService
    {
        private readonly IRepository<Picture> _pictureRepo;
        private readonly IRepository<User, long> _userRepo;
        private readonly IRepository<PicViewRecord> _picViewRecordRepo;
        private readonly IRepository<PicFavRelation> _picFavRelationRepo;
        private readonly PicManager _picManager;

        public PicAppService(
            IRepository<Picture> pictureRepo,
            IRepository<User, long> userRepo,
            IRepository<PicViewRecord> picViewRecordRepo,
            IRepository<PicFavRelation> picFavRelationRepo,
            PicManager picManager)
        {
            this._pictureRepo = pictureRepo;
            this._userRepo = userRepo;
            this._picViewRecordRepo = picViewRecordRepo;
            this._picManager = picManager;
            this._picFavRelationRepo = picFavRelationRepo;
        }

        public void AddViewRecord(int id)
        {
            var pic = this._pictureRepo.Get(id);
            var currentUser = this._userRepo.Get(this.AbpSession.UserId.Value);
            this._picViewRecordRepo.Insert(new PicViewRecord()
            {
                Picture = pic,
                User = currentUser,
                ViewDate = DateTime.Now
            });
        }

        public void Delete(EntityDto input)
        {
            this._picManager.Delete(input.Id);
        }

        public PagedResultDto<PicDto> GetAll(PicPageRequestDto input)
        {
            var pageQuery = this._pictureRepo.GetAll();

            var count = pageQuery.Count();

            var data = pageQuery
                .Include(x => x.PicFavRelation)
                .Skip(input.SkipCount)
                .Take(input.MaxResultCount)
                .ToList();
            
            var items = this.ObjectMapper.Map<List<PicDto>>(data);

            if (items != null)
            {
                items.ForEach(item =>
                {
                    item.HasFav = this._picFavRelationRepo
                        .Count(x => x.Picture.Id == item.Id && x.Fav.User.Id == this.AbpSession.UserId) > 0;

                    item.ViewCount = this._picViewRecordRepo
                        .Count(x => x.Picture.Id == item.Id);
                });
            }
      
            return new PagedResultDto<PicDto>()
            {
                TotalCount = count,
                Items = items
            };
        }

        public void Hidden(int id)
        {
            var pic = this._pictureRepo.Get(id);
            pic.SetHidden();
        }
    }
}
