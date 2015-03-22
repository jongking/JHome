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

        public virtual void Update(T obj)
        {
            DbHelper.UpdateModel(obj);
        }

        public virtual void Remove(T obj)
        {
            DbHelper.Remove(obj);
        }
    }
}
