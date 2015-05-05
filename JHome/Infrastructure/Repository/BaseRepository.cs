using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JHelper.DB;

namespace Infrastructure.Repository
{
    public class BaseRepository<T>
    {
        public virtual void Add(T obj)
        {
            DbHelper.InsertModel(obj);
        }

        public virtual int Update(T obj)
        {
            return DbHelper.UpdateModel(obj);
        }

        public int Update(T obj, params string[] updateParams)
        {
            return DbHelper.UpdateModelByParams(obj, updateParams);
        }

        public virtual void Remove(T obj)
        {
            DbHelper.Remove(obj);
        }
    }
}
