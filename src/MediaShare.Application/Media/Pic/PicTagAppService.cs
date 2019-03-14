using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using MediaShare.Media.Pic.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace MediaShare.Media.Pic
{
    public class PicTagAppService : ApplicationService, IPicTagAppService
    {
        private readonly IRepository<PicTag> _picTagRepo;
        private readonly IRepository<PicTagRelation> _picTagRelationRepo;
        private readonly IRepository<Picture> _pictureRepo;

        public PicTagAppService(
            IRepository<PicTag> picTagRepo,
            IRepository<PicTagRelation> picTagRelationRepo,
            IRepository<Picture> pictureRepo
            )
        {
            this._picTagRepo = picTagRepo;
            this._picTagRelationRepo = picTagRelationRepo;
            this._pictureRepo = pictureRepo;
        }

        public void AddPicToTag(int id, int picId)
        {
            var tag = this._picTagRepo.Get(id);
            var pic = this._pictureRepo.Get(picId);

            this._picTagRelationRepo.Insert(new PicTagRelation()
            {
                Tag = tag,
                Picture = pic
            });
        }

        public PicTagDto Create(string tagName)
        {
            var tag = this._picTagRepo.Insert(new PicTag()
            {
                Name = tagName
            });
            return this.ObjectMapper.Map<PicTagDto>(tag);
        }

        public void Delete(int id)
        {
            this._picTagRelationRepo.Delete(x => x.Tag.Id == id);
            this._picTagRepo.Delete(id);
        }

        public List<PicTagDto> GetAll()
        {
            var tags = this._picTagRepo.GetAll();
            return this.ObjectMapper.Map<List<PicTagDto>>(tags);
        }

        public List<PicTagDto> GetPicTags(int picId)
        {
            var tags = this._picTagRelationRepo.GetAll()
                .Include(x => x.Tag)
                .Where(x => x.Picture.Id == picId)
                .Select(x => x.Tag);

            return this.ObjectMapper.Map<List<PicTagDto>>(tags);
        }

        public void RemovePicFromTag(int id, int picId)
        {
            this._picTagRelationRepo.Delete(x => x.Tag.Id == id && x.Picture.Id == picId);
        }

        public PicTagDto Update(PicTagDto input)
        {
            var tag = this._picTagRepo.Get(input.Id);
            tag = this.ObjectMapper.Map(input, tag);
            return this.ObjectMapper.Map<PicTagDto>(tag);
        }
    }
}
