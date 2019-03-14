using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MediaShare.Media.Pic
{
    public class PicTag :Entity
    {
        public string Name { get; set; }

        public virtual ICollection<PicTagRelation> PicTagRelation { get; set; }
    }
}
