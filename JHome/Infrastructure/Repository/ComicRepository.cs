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
        public Comic GetByComicName(string comicName)
        {
            return DbHelper.GetModel<Comic>(
                SimpleSqlCreater.Select<Comic>()
                .Eq("ComicName", comicName)
                .ToString());
        }
    }
}
