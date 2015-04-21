using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain.Model.Comic
{
    public class ComicPage
    {
        /// <summary>
        /// 连接Comic类中的Id
        /// </summary>
        public int ComicId { get; set; }
        public int VolumeId { get; set; }
        public int PageNumber { get; set; }
        public string PageImgPath { get; set; }
    }
}
