using Abp.Domain.Entities;
using MediaShare.Authorization.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace MediaShare.Media.Pic
{
    public class PicViewRecord : Entity
    {
        public DateTime ViewDate { get; set; }

        public virtual Picture Picture { get; set; }

        public virtual User User { get; set; }
    }
}
