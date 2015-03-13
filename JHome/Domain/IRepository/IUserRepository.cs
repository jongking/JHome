using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Domain.Model;

namespace Domain.IRepository
{
    public interface IUserRepository
    {
        void Add(User user);
        void Update(User user);
        void Remove(User user);
        User GetById(Guid productId);
        User GetByUserName(string name);
        ICollection<User> GetByPassWord(string category);
    }
}
