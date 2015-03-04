using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Domain.Model;

namespace Domain.IRepository
{
    public interface IUserRepository
    {
        void Add(Users users);
        void Update(Users users);
        void Remove(Users users);
        Users GetById(Guid productId);
        Users GetByUserName(string name);
        ICollection<Users> GetByPassWord(string category);
    }
}
