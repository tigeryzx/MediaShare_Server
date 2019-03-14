using System;
using System.Collections.Generic;
using System.Text;

namespace MediaShare.Media.Dto
{
    public class LuckVideoRequestDto
    {
        public int? FavId { get; set; }

        public bool? InAllVideo { get; set; }

        public bool? InAllFav { get; set; }

        public bool? InSingleFav { get; set; }
    }
}
