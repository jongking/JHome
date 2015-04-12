using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Domain.IRepository;
using JHelper.DB;

namespace Infrastructure.Repository
{
    public class DtoRepository : IDtoRepository
    {
        public List<T> GetList<T>(string msg)
        {
            return DbHelper.GetList<T>(msg);
        }

        public T GetModel<T>(string msg)
        {
            return DbHelper.GetModel<T>(msg);
        }
    }
}
