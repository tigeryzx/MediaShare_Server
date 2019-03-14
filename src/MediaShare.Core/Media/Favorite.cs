using Abp.Domain.Entities;
using MediaShare.Authorization.Users;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace MediaShare.Media
{
    public class Favorite : Entity
    {
        public int GetVideoCount()
        {
            if (this.VideoFavRelations == null || this.VideoFavRelations.Count <= 0)
                return 0;
            return this.VideoFavRelations.Count;
        }

        public string CategoryName { get; set; }

        public virtual User User { get; set; }

        public virtual ICollection<VideoFavRelation> VideoFavRelations { get; set; }
    }
}
