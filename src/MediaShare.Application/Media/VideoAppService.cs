using System;
using System.Collections.Generic;
using System.Text;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.ObjectMapping;
using MediaShare.Media.Dto;
using System.Linq;
using Abp.Linq;
using Abp.Runtime.Session;
using MediaShare.Media.Manager;

namespace MediaShare.Media
{
    public class VideoAppService : IVideoAppService
    {
        private readonly IRepository<Video> _videoRepo;
        private readonly IObjectMapper _objectMapper;
        private readonly IAbpSession _abpSession;
        private readonly IResourceDirService _resourceDirManager;
        private readonly IVideoService _videoService;

        public VideoAppService(
            IRepository<Video> videoRepo,
            IObjectMapper objectMapper,
            IAbpSession abpSession,
            IResourceDirService resourceDirManager,
            IVideoService videoService
            )

        {
            this._videoRepo = videoRepo;
            this._objectMapper = objectMapper;
            this._abpSession = abpSession;
            this._resourceDirManager = resourceDirManager;
            this._videoService = videoService;
        }

        public int AddVideoViewRecord(VideoViewRequestDto input)
        {
            return this._videoService.AddVideoViewRecord(input.VideoId, input.IsPlay);
        }

        public void Delete(EntityDto input)
        {
            this._videoService.DeleteVideo(input.Id);
        }

        public PagedResultDto<VideoDto> GetAll(VideoPageRequestDto input)
        {
            var videoQuery = this._videoRepo
                .GetAll()
                .Where(x => x.IsSkip == false);
            
            if (input.FavId.HasValue)
            {
                videoQuery = videoQuery
                    .Where(x => x.FavRelations.Any(fav => fav.Favorite.Id == input.FavId));
            }

            if (!string.IsNullOrEmpty(input.VideoName))
            {
                videoQuery = videoQuery
                    .Where(x => x.FileName.Contains(input.VideoName));
            }

            // TODO:以下排序应该分开用户
            if (input.IsHotPlay == true || input.IsLatePlay == true)
            {
                videoQuery = videoQuery
                    .Where(x => x.ViewRecordHistory.Any(y => y.IsPlay == true && y.User.Id == this._abpSession.UserId));
            }
            else if (input.IsHistoryView == true)
            {
                videoQuery = videoQuery
                    .Where(x => x.ViewRecordHistory.Where(y => y.User.Id == this._abpSession.UserId).Count() > 0);
            }

            int count = videoQuery.Count();

            // 排序

            if (input.IsHotPlay == true) // 最多播放
                videoQuery = videoQuery
                    .OrderByDescending(x => x.ViewRecordHistory.Where(y => y.IsPlay == true).Count());
            else if (input.IsLatePlay == true) // 最近播放
                videoQuery = videoQuery
                    .OrderByDescending(x => x.ViewRecordHistory.Where(y => y.IsPlay == true).Max(y => y.ViewDate));
            else if (input.IsHistoryView == true) // 最近查看
                videoQuery = videoQuery
                    .OrderByDescending(x => x.ViewRecordHistory.Where(y => y.IsPlay != true).Max(y => y.ViewDate));
            else if (input.FavId.HasValue)
                videoQuery = videoQuery
                    .OrderByDescending(x => x.FavRelations
                    .Where(y => y.Favorite.User.Id == this._abpSession.UserId)
                    .Max(y => y.Id));
            else
                videoQuery = videoQuery.OrderByDescending(x => x.AppendDate);

            // 是否随机列表
            IQueryable<Video> videoList = null;
            if (input.IsRandomList == true)
            {
                var videoIdsList = videoQuery.Select(x => x.Id).ToArray();

                int[] videoIds = new int[input.MaxResultCount];
                Random rnd = new Random();
                for (var i = 0; i < videoIds.Length; i++)
                {
                    // 防止随机重复
                    do
                    {
                        int index = rnd.Next(videoIdsList.Length);
                        var rndVideoId = videoIdsList[index];
                        videoIds[i] = rndVideoId;
                    } while (videoIds.Count(x => x == videoIds[i]) > 1);
                }

                videoList = videoQuery
                    .Where(x => videoIds.Contains(x.Id));
            }
            else
            {
                videoList = videoQuery
                    .Skip(input.SkipCount)
                    .Take(input.MaxResultCount);
            }
                


            var items = this._objectMapper.Map<List<VideoDto>>(videoList);

            foreach (var video in videoList)
            {
                // 过滤收藏夹只取当前的用户
                var videoOutInfo = items.Single(x => x.Id == video.Id);
                videoOutInfo.FtpPath = this._resourceDirManager.GetVideoFtpPath(video);
                var myFav = video.FavRelations.Where(x => x.Favorite.User.Id == this._abpSession.UserId);
                if (myFav != null && myFav.Count() > 0)
                    videoOutInfo.Favorite = this._objectMapper.Map<List<FavoriteDto>>(myFav);
            }

            return new PagedResultDto<VideoDto>()
            {
                TotalCount = count,
                Items = items
            };
        }


        public VideoDto GetLuckVideo(LuckVideoRequestDto input)
        {
            Video video = null;
            if(input.InSingleFav == true)
            {
                video = this._videoService.GetLuckVideo(input.FavId.Value);
            }
            else if(input.InAllFav == true)
            {
                video = this._videoService.GetLuckVideoInMyFav();
            }
            else if(input.InAllVideo == true)
            {
                video = this._videoService.GetLuckVideoInAll();
            }

            var resultVideo = this._objectMapper.Map<VideoDto>(video);

            resultVideo.FtpPath = this._resourceDirManager.GetVideoFtpPath(video);
            var myFav = video.FavRelations.Where(x => x.Favorite.User.Id == this._abpSession.UserId);
            if (myFav != null && myFav.Count() > 0)
                resultVideo.Favorite = this._objectMapper.Map<List<FavoriteDto>>(myFav);

            return resultVideo;
        }


        public VideoDto SetVideoCover(VideoCoverSettingDto input)
        {
            var video = this._videoService.SetVideoCover(input.VideoId, input.ImageId);
            return this._objectMapper.Map<VideoDto>(video);
        }
    }
}
