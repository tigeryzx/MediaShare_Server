using Abp.Domain.Repositories;
using Abp.Domain.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace MediaShare.Media.Pic.Manager
{
    public class PicManager : DomainService
    {
        private readonly IRepository<Picture> _pictureRepo;
        private readonly IRepository<PicViewRecord> _picViewRecordRepo;
        private readonly IRepository<PicFavRelation> _picFavRelationRepo;
        private readonly IRepository<PicTagRelation> _picTagRelationRepo;

        public PicManager(
            IRepository<Picture> pictureRepo,
            IRepository<PicViewRecord> picViewRecordRepo,
            IRepository<PicFavRelation> picFavRelationRepo,
            IRepository<PicTagRelation> picTagRelationRepo
            )
        {
            this._pictureRepo = pictureRepo;
            this._picViewRecordRepo = picViewRecordRepo;
            this._picFavRelationRepo = picFavRelationRepo;
            this._picTagRelationRepo = picTagRelationRepo;
        }

        public void Delete(int id)
        {
            this._picViewRecordRepo.Delete(x => x.Picture.Id == id);
            this._picFavRelationRepo.Delete(x => x.Picture.Id == id);
            this._picTagRelationRepo.Delete(x => x.Picture.Id == id);
            this._pictureRepo.Delete(x => x.Id == id);
        }
    }
}
