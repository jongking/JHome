using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Domain.Exception;

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
        public string VolDetailUrl { get; set; }
        public int SortNo { get; set; }

        public List<ComicPage> ComicPages()
        {
            return new List<ComicPage>();
        }

        public bool Add()
        {
            Check();

            Comic.ComicRepository.AddComicVolume(this);

            return true;
        }

        /// <summary>
        /// 领域模型自检
        /// </summary>
        private void Check()
        {
            if (ComicId <= 0)
            {
                throw new JException("Comic.ComicId Error", ExceptionType.领域模型自检);
            }
            if (string.IsNullOrEmpty(VolumeName))
            {
                throw new JException("Comic.VolumeName Error", ExceptionType.领域模型自检);
            }
        }
    }
}
