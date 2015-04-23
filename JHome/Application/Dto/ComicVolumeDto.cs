using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Application.Dto
{
    public class ComicVolumeDto
    {
        public int Id { get; set; }
        public int ComicId { get; set; }
        public string VolumeName { get; set; }
        public int SortNo { get; set; }
    }
}
