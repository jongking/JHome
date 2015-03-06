using System.Collections.Generic;
using Domain.IRepository;
using Domain.Model;
using Domain.Repository;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using NUnit.Framework;

namespace JHelper.Tests.Domain
{
    [TestFixture]
    public class UsersRepository_Fixture
    {
        private ISessionFactory _sessionFactory;
        private Configuration _configuration;

        private readonly Users[] _userses = new[]
                 {
                     new Users {UserName = "Melon", PassWord = "Fruits"},
                     new Users {UserName = "Pear", PassWord = "Fruits"},
                     new Users {UserName = "Milk", PassWord = "Beverages"},
                     new Users {UserName = "Coca Cola", PassWord = "Beverages"},
                     new Users {UserName = "Pepsi Cola", PassWord = "Beverages"},
                 };

        private void CreateInitialData()
        {

            using (ISession session = _sessionFactory.OpenSession())
            using (ITransaction transaction = session.BeginTransaction())
            {
                foreach (var product in _userses)
                    session.Save(product);
                transaction.Commit();
            }
        }

        [SetUp]
        public void SetupContext()
        {
            new SchemaExport(_configuration).Execute(false, true, false);
            CreateInitialData();
        }
        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
            _configuration = new Configuration();
            _configuration.Configure();
            _configuration.AddAssembly(typeof(Users).Assembly);
            _sessionFactory = _configuration.BuildSessionFactory();
        }

        [Test]
        public void Can_add_new_product()
        {
            var user = new Users { UserName = "Jongking", PassWord = "sssaaa" };
            IUserRepository repository = new UserRepository();
            repository.Add(user);

            // use session to try to load the Users
            using (ISession session = _sessionFactory.OpenSession())
            {
                var fromDb = session.Get<Users>(user.Id);
                // Test that the Users was successfully inserted
                Assert.IsNotNull(fromDb);
                Assert.AreNotSame(user, fromDb);
                Assert.AreEqual(user.UserName, fromDb.UserName);
                Assert.AreEqual(user.PassWord, fromDb.PassWord);
            }
        }

        [Test]
        public void Can_update_existing_product()
        {
            var user = _userses[0];
            user.UserName = "Yellow Pear";
            IUserRepository repository = new UserRepository();
            repository.Update(user);

            // use session to try to load the Users
            using (ISession session = _sessionFactory.OpenSession())
            {
                var fromDb = session.Get<Users>(user.Id);
                Assert.AreEqual(user.UserName, fromDb.UserName);
            }
        }

        [Test]
        public void Can_remove_existing_product()
        {
            var user = _userses[0];
            IUserRepository repository = new UserRepository();
            repository.Remove(user);

            using (ISession session = _sessionFactory.OpenSession())
            {
                var fromDb = session.Get<Users>(user.Id);
                Assert.IsNull(fromDb);
            }
        }

        [Test]
        public void Can_get_existing_product_by_id()
        {
            IUserRepository repository = new UserRepository();
            var fromDb = repository.GetById(_userses[1].Id);
            Assert.IsNotNull(fromDb);
            Assert.AreNotSame(_userses[1], fromDb);
            Assert.AreEqual(_userses[1].UserName, fromDb.UserName);
        }

        [Test]
        public void Can_get_existing_product_by_name()
        {
            IUserRepository repository = new UserRepository();
            var fromDb = repository.GetByUserName(_userses[1].UserName);

            Assert.IsNotNull(fromDb);
            Assert.AreNotSame(_userses[1], fromDb);
            Assert.AreEqual(_userses[1].Id, fromDb.Id);
        }

        [Test]
        public void Can_get_existing_products_by_category()
        {
            IUserRepository repository = new UserRepository();
            var fromDb = repository.GetByPassWord("Fruits");

            Assert.AreEqual(2, fromDb.Count);
            Assert.IsTrue(IsInCollection(_userses[0], fromDb));
            Assert.IsTrue(IsInCollection(_userses[1], fromDb));
        }

        private bool IsInCollection(Users users, ICollection<Users> fromDb)
        {
            foreach (var item in fromDb)
                if (users.Id == item.Id)
                    return true;
            return false;
        }
    }
}
