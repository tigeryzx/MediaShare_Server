using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MediaShare.Media.Pic
{
    public class PicTagRelation : Entity
    {
        public virtual PicTag Tag { get; set; }

        public virtual Picture Picture { get; set; }
    }
}
