using Abp.Domain.Entities;
using MediaShare.Authorization.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace MediaShare.Media.Pic
{
    public class PicFav :Entity
    {
        public string Name { get; set; }

        public virtual Picture Cover { get; set; }

        public virtual User User { get; set; }

        public virtual ICollection<PicFavRelation> PicFavRelation { get; set; }
    }
}
