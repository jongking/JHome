using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Domain.IRepository;
using Domain.Model;
using NHibernate;
using NHibernate.Criterion;

namespace Domain.Repository
{
    public class UserRepository : IUserRepository
    {
        public void Add(Users users)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            using (ITransaction transaction = session.BeginTransaction())
            {
                session.Save(users);
                transaction.Commit();
            }
        }

        public void Update(Users users)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            using (ITransaction transaction = session.BeginTransaction())
            {
                session.Update(users);
                transaction.Commit();
            }
        }

        public void Remove(Users users)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            using (ITransaction transaction = session.BeginTransaction())
            {
                session.Delete(users);
                transaction.Commit();
            }
        }

        public Users GetById(Guid productId)
        {
            using (ISession session = NHibernateHelper.OpenSession())
                return session.Get<Users>(productId);
        }

        public Users GetByUserName(string name)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                Users users = session
                    .CreateCriteria(typeof(Users))
                    .Add(Restrictions.Eq("UserName", name))
                    .UniqueResult<Users>();
                return users;
            }
        }

        public ICollection<Users> GetByPassWord(string category)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                var products = session
                    .CreateCriteria(typeof(Users))
                    .Add(Restrictions.Eq("PassWord", category))
                    .List<Users>();
                return products;
            }
        }
    }
}
