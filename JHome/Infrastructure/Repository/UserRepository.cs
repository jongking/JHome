using System;
using System.Collections.Generic;
using Domain.IRepository;
using Domain.Model;
using JHelper.DB;

namespace Infrastructure.Repository
{
    public class UserRepository : BaseRepository<User>,IUserRepository
    {
        public User GetById(int productId)
        {
            return DbHelper.GetModel<User>(
                SimpleSqlCreater.Select<User>()
                .Eq("Id", productId.ToString())
                .ToString());
        }

        public User GetByUserName(string name)
        {
            return DbHelper.GetModel<User>(
                SimpleSqlCreater.Select<User>()
                .Eq("UserName", name)
                .ToString());
        }

        public ICollection<User> GetByPassWord(string category)
        {
            return DbHelper.GetList<User>(
                SimpleSqlCreater.Select<User>()
                .Eq("PassWord", category)
                .ToString());
        }
    }
}
