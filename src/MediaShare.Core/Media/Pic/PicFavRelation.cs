using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MediaShare.Media.Pic
{
    public class PicFavRelation :Entity
    {
        public virtual PicFav Fav { get; set; }

        public virtual Picture Picture { get; set; }
    }
}
