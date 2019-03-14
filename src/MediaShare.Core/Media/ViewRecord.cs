using Abp.Domain.Entities;
using MediaShare.Authorization.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace MediaShare.Media
{
    public class ViewRecord : Entity
    {
        public DateTime ViewDate { get; set; }

        public virtual Video Video { get; set; }

        public virtual User User { get; set; }

        public bool? IsPlay { get; set; }
    }
}
