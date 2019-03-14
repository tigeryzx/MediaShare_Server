using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using static System.Net.Mime.MediaTypeNames;
using System.Linq;

namespace MediaShare.Media
{
    public class Video : Entity
    {
        public void SetCover(Image cover)
        {
            if (this.Images.Count > 0)
            {
                foreach(var img in this.Images)
                {
                    if (img.Id == cover.Id)
                        img.IsCover = true;
                    else
                        img.IsCover = false;
                }
                
            }
        }

        public List<VideoFavRelation> GetUserFavRelation(long userId)
        {
            if (this.FavRelations.Count() <= 0)
                return null;
            return this.FavRelations.Where(x => x.Favorite.User.Id == userId).ToList();
        }


        public int GetPlayCount()
        {
            if (this.ViewRecordHistory == null)
                return 0;
            return this.ViewRecordHistory.Count(x => x.IsPlay == true);
        }

        public int GetViewCount()
        {
            if (this.ViewRecordHistory == null)
                return 0;
            return this.ViewRecordHistory.Count(x => x.IsPlay == false);
        }

        /// <summary>
        /// 截图信息
        /// </summary>
        public virtual ICollection<Image> Images { get; set; }

        /// <summary>
        /// 收藏信息
        /// </summary>
        public virtual ICollection<VideoFavRelation> FavRelations { get; set; }

        /// <summary>
        /// 观看记录
        /// </summary>
        public virtual ICollection<ViewRecord> ViewRecordHistory { get; set; }

        /// <summary>
        /// 文件名称
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// 物理路径
        /// </summary>
        public string PhysicalPath { get; set; }

        /// <summary>
        /// 添加日期
        /// </summary>
        public DateTime AppendDate { get; set; }

        /// <summary>
        /// 忽略的视频
        /// </summary>
        public bool IsSkip { get; set; }

    }
}
