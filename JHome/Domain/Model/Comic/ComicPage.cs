using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Domain.Exception;

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


        public bool Add()
        {
            Check();

            Comic.ComicRepository.AddComicPage(this);

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
            if (VolumeId <= 0)
            {
                throw new JException("Comic.VolumeId Error", ExceptionType.领域模型自检);
            }
            if (PageNumber <= 0)
            {
                throw new JException("Comic.PageNumber Error", ExceptionType.领域模型自检);
            }
            if (string.IsNullOrEmpty(PageImgPath))
            {
                throw new JException("Comic.PageImgPath Error", ExceptionType.领域模型自检);
            }
        }
    }
}
