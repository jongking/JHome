using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain.Model.Comic
{
    public class ComicVolume
    {
        public int Id { get; set; }
        /// <summary>
        /// 连接Comic类中的Id
        /// </summary>
        public int ComicId { get; set; }
        public string VolumeName { get; set; }

        public List<ComicPage> ComicPages()
        {
            return new List<ComicPage>();
        }
    }
}
