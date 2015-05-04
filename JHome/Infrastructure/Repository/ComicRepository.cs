using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Domain.IRepository;
using Domain.Model.Comic;
using JHelper.DB;

namespace Infrastructure.Repository
{
    public class ComicRepository : BaseRepository<Comic>,IComicRepository
    {
        public override void Add(Comic obj)
        {
            var ssc = SimpleSqlCreater.Insert<Comic>()
                .GetParamsFromClass(obj)
                .SetParam("ComicAuthor", string.Format("N'{0}'", obj.ComicAuthor));

            DbHelper.ExecuteScalar(ssc.ToString());
        }

        public Comic GetByComicName(string comicName)
        {
            return DbHelper.GetModel<Comic>(
                SimpleSqlCreater.Select<Comic>()
                .Eq("ComicName", comicName)
                .ToString());
        }

        public void AddComicVolume(ComicVolume comicVolume)
        {
            DbHelper.InsertModel(comicVolume);
        }

        public void AddComicPage(ComicPage comicPage)
        {
            DbHelper.InsertModel(comicPage);
        }
    }
}
