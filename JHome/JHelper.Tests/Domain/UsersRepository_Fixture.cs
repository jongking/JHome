using System.Collections.Generic;
using Domain.IRepository;
using Domain.Model;
using Infrastructure.Repository;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using NUnit.Framework;

namespace JHelper.Tests.Domain
{
    [TestFixture]
    public class UsersRepository_Fixture
    {
        [SetUp]
        public void SetupContext()
        {
            new SchemaExport(_configuration).Execute(false, true, false);
            CreateInitialData();
        }

        private ISessionFactory _sessionFactory;
        private Configuration _configuration;

        private readonly User[] _userses =
        {
            new User {UserName = "Melon", PassWord = "Fruits"},
            new User {UserName = "Pear", PassWord = "Fruits"},
            new User {UserName = "Milk", PassWord = "Beverages"},
            new User {UserName = "Coca Cola", PassWord = "Beverages"},
            new User {UserName = "Pepsi Cola", PassWord = "Beverages"}
        };

        private void CreateInitialData()
        {
            using (ISession session = _sessionFactory.OpenSession())
            using (ITransaction transaction = session.BeginTransaction())
            {
                foreach (User user in _userses)
                    session.Save(user);
                transaction.Commit();
            }
        }

        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
            _configuration = new Configuration();
            _configuration.Configure();
            _configuration.AddAssembly(typeof (User).Assembly);
            _sessionFactory = _configuration.BuildSessionFactory();
        }

        private bool IsInCollection(User user, ICollection<User> fromDb)
        {
            foreach (User item in fromDb)
                if (user.Id == item.Id)
                    return true;
            return false;
        }

        [Test]
        public void Can_add_new_user()
        {
            var user = new User {UserName = "Jongking", PassWord = "sssaaa"};
            IUserRepository repository = new UserRepository();
            repository.Add(user);

            // use session to try to load the User
            using (ISession session = _sessionFactory.OpenSession())
            {
                var fromDb = session.Get<User>(user.Id);
                // Test that the User was successfully inserted
                Assert.IsNotNull(fromDb);
                Assert.AreNotSame(user, fromDb);
                Assert.AreEqual(user.UserName, fromDb.UserName);
                Assert.AreEqual(user.PassWord, fromDb.PassWord);
            }
        }

        [Test]
        public void Can_get_existing_user_by_id()
        {
            IUserRepository repository = new UserRepository();
            User fromDb = repository.GetById(_userses[1].Id);
            Assert.IsNotNull(fromDb);
            Assert.AreNotSame(_userses[1], fromDb);
            Assert.AreEqual(_userses[1].UserName, fromDb.UserName);
        }

        [Test]
        public void Can_get_existing_user_by_name()
        {
            IUserRepository repository = new UserRepository();
            User fromDb = repository.GetByUserName(_userses[1].UserName);

            Assert.IsNotNull(fromDb);
            Assert.AreNotSame(_userses[1], fromDb);
            Assert.AreEqual(_userses[1].Id, fromDb.Id);
        }

        [Test]
        public void Can_get_existing_users_by_category()
        {
            IUserRepository repository = new UserRepository();
            ICollection<User> fromDb = repository.GetByPassWord("Fruits");

            Assert.AreEqual(2, fromDb.Count);
            Assert.IsTrue(IsInCollection(_userses[0], fromDb));
            Assert.IsTrue(IsInCollection(_userses[1], fromDb));
        }

        [Test]
        public void Can_remove_existing_user()
        {
            User user = _userses[0];
            IUserRepository repository = new UserRepository();
            repository.Remove(user);

            using (ISession session = _sessionFactory.OpenSession())
            {
                var fromDb = session.Get<User>(user.Id);
                Assert.IsNull(fromDb);
            }
        }

        [Test]
        public void Can_update_existing_user()
        {
            User user = _userses[0];
            user.UserName = "Yellow Pear";
            IUserRepository repository = new UserRepository();
            repository.Update(user);

            // use session to try to load the User
            using (ISession session = _sessionFactory.OpenSession())
            {
                var fromDb = session.Get<User>(user.Id);
                Assert.AreEqual(user.UserName, fromDb.UserName);
            }
        }
    }
}