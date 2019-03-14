using Abp.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Abp.Linq;
using Abp.Runtime.Session;
using System.IO;
using MediaShare.Authorization.Users;

namespace MediaShare.Media.Manager
{
    public class VideoService : IVideoService
    {
        private IRepository<Video> _videoRepo;
        private IAbpSession _abpSession;
        IRepository<User, long> _userRepo;
        private IRepository<Image> _imageRepo;
        private IRepository<VideoFavRelation> _videoFavRelationRepo;
        private IRepository<ViewRecord> _viewRecordRepo;

        public VideoService(
            IRepository<Video> videoRepo,
            IAbpSession abpSession,
            IRepository<User, long> userRepo,
            IRepository<Image> imageRepo,
            IRepository<VideoFavRelation> videoFavRelationRepo,
            IRepository<ViewRecord> viewRecordRepo
            )
        {
            this._videoRepo = videoRepo;
            this._abpSession = abpSession;
            this._userRepo = userRepo;

            this._imageRepo = imageRepo;
            this._videoFavRelationRepo = videoFavRelationRepo;
            this._viewRecordRepo = viewRecordRepo;
        }

        protected Video GetRandomVideo(int[] videoIds)
        {
            if (videoIds != null && videoIds.Count() > 0)
            {
                Random rnd = new Random();
                int index = rnd.Next(videoIds.Length);
                var vid = videoIds[index];
                return this._videoRepo.Get(vid);
            }
            return null;
        }

        public Video GetLuckVideo(int favId)
        {
            var videoIds = this._videoRepo.GetAll()
                .Where(x => x.IsSkip == false && x.FavRelations.Any(y => y.Favorite.Id == favId))
                .Select(x => x.Id)
                .ToArray();

            return GetRandomVideo(videoIds);
        }

        public Video GetLuckVideoInAll()
        {
            var videoIds = this._videoRepo.GetAll()
                .Where(x => x.IsSkip == false)
                .Select(x => x.Id)
                .ToArray();

            return GetRandomVideo(videoIds);
        }

        public Video GetLuckVideoInMyFav()
        {
            var videoIds = this._videoRepo.GetAll()
                .Where(x => x.IsSkip == false && x.FavRelations
                .Any(y => y.Favorite.User.Id == this._abpSession.UserId))
                .Select(x => x.Id)
                .ToArray();

            return GetRandomVideo(videoIds);
        }

        public void DeleteVideo(int videoId)
        {
            var video = this._videoRepo.Get(videoId);
            if (File.Exists(video.PhysicalPath))
                File.Delete(video.PhysicalPath);

            this._viewRecordRepo.Delete(x => x.Video.Id == videoId);
            this._videoFavRelationRepo.Delete(x => x.Video.Id == videoId);
            this._imageRepo.Delete(x => x.Video.Id == videoId);
            this._videoRepo.Delete(videoId);
        }

        public Video SetVideoCover(int videoId, int imageId)
        {
            var videoInfo = this._videoRepo.Get(videoId);
            foreach(var img in videoInfo.Images)
            {
                if (img.Id == imageId)
                {
                    img.IsCover = true;
                    continue;
                }
                img.IsCover = false;
            }
            return videoInfo;
        }

        public int AddVideoViewRecord(int videoId, bool isPlay)
        {
            var video = this._videoRepo.Get(videoId);
            var currentUser = this._userRepo.Get(this._abpSession.UserId.Value);
            video.ViewRecordHistory.Add(new ViewRecord()
            {
                ViewDate = DateTime.Now,
                User = currentUser,
                IsPlay = isPlay
            });

            return video.ViewRecordHistory.Count(x=>x.IsPlay == isPlay);
        }
    }
}
