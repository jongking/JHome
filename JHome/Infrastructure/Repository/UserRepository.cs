using System;
using System.Collections.Generic;
using Domain.IRepository;
using Domain.Model;
using NHibernate;
using NHibernate.Criterion;

namespace Infrastructure.Repository
{
    public class UserRepository : IUserRepository
    {
        public void Add(User user)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            using (ITransaction transaction = session.BeginTransaction())
            {
                session.Save(user);
                transaction.Commit();
            }
        }

        public void Update(User user)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            using (ITransaction transaction = session.BeginTransaction())
            {
                session.Update(user);
                transaction.Commit();
            }
        }

        public void Remove(User user)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            using (ITransaction transaction = session.BeginTransaction())
            {
                session.Delete(user);
                transaction.Commit();
            }
        }

        public User GetById(Guid productId)
        {
            using (ISession session = NHibernateHelper.OpenSession())
                return session.Get<User>(productId);
        }

        public User GetByUserName(string name)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                User user = session
                    .CreateCriteria(typeof(User))
                    .Add(Restrictions.Eq("UserName", name))
                    .UniqueResult<User>();
                return user;
            }
        }

        public ICollection<User> GetByPassWord(string category)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                var products = session
                    .CreateCriteria(typeof(User))
                    .Add(Restrictions.Eq("PassWord", category))
                    .List<User>();
                return products;
            }
        }
    }
}
