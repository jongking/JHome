using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Domain.Exception;

namespace Domain.Model.Comic
{
    public class Comic
    {
        public int Id { get; set; }
        public string ComicName { get; set; }
        public string TitleName { get; set; }
        public string Description { get; set; }
        public string CoverImgPath { get; set; }
        public List<ComicVolume> ComicVolumes()
        {
            return new List<ComicVolume>();
        }
        public List<ComicPage> ComicPages()
        {
            return new List<ComicPage>();
        }

        /// <summary>
        /// 领域模型自检
        /// </summary>
        private void Check()
        {
            if (string.IsNullOrEmpty(ComicName))
            {
                throw new JException("Comic.ComicName Error", ExceptionType.领域模型自检);
            }
            if (string.IsNullOrEmpty(TitleName))
            {
                throw new JException("Comic.TitleName Error", ExceptionType.领域模型自检);
            }
        }
    }
}
