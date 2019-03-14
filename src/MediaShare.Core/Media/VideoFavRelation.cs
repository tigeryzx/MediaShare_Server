using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MediaShare.Media
{
    public class VideoFavRelation :Entity
    {
        public virtual Video Video { get; set; }

        public virtual Favorite Favorite { get; set; }
    }
}
