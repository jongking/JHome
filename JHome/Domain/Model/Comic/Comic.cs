using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Domain.Exception;
using Domain.IRepository;
using Factory;

namespace Domain.Model.Comic
{
    public class Comic
    {
        public readonly static IComicRepository ComicRepository = RepositoryFactory.CreateInstance<IComicRepository>("Comic");

        public int Id { get; set; }
        public string ComicName { get; set; }
        public string TitleName { get; set; }
        public string ComicType { get; set; }
        public string ComicAuthor { get; set; }
        public string Description { get; set; }
        public string CoverImgPath { get; set; }
        public string OrginCoverImgPath { get; set; }
        public int ComicState { get; set; }
        public List<ComicVolume> ComicVolumes()
        {
            return new List<ComicVolume>();
        }
        public List<ComicPage> ComicPages()
        {
            return new List<ComicPage>();
        }
        public Comic()
        {
            ComicAuthor = "未知";
        }
        public bool Add()
        {
            Check();

            ComicRepository.Add(this);

            return true;
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

        public static bool HasComic(string comicName)
        {
            var comic = ComicRepository.GetByComicName(comicName);
            return comic != null && comic.Id > 0;
        }
    }
}
