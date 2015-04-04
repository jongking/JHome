using JHelper.DB;
using NUnit.Framework;

namespace JHelper.Tests
{
    [TestFixture]
    internal class DbHelp_Fixture
    {
        [TestFixtureSetUp]
        public void SetDebug()
        {
            DbHelper.IsDebug = true;
        }

        [SetUp]
        public void ClearNUnit()
        {
            DbHelper.ExecuteNonQuery("DELETE FROM Test WHERE 1=1");
        }

        [Test]
        public void Can_RunSql()
        {
            DbHelper.ExecuteDataSet("SELECT 1");
            DbHelper.ExecuteDataTable("SELECT 1");
            DbHelper.ExecuteNonQuery("SELECT 1");
            DbHelper.ExecuteScalar("SELECT 1");
        }

        [Test]
        public void Can_TransactionScope_Complete()
        {
            using (var scope = DbHelper.GetTransactionScope())
            {
                DbHelper.ExecuteNonQuery("INSERT INTO Test (No) VALUES ('1')");
                DbHelper.ExecuteNonQuery("INSERT INTO Test (No) VALUES ('2')");
                scope.Complete();
            }
            var dt = DbHelper.ExecuteDataTable("SELECT * FROM Test");
            Assert.AreEqual(dt.Rows.Count, 2);
        }

        [Test]
        public void Can_TransactionScope_Not_Complete()
        {
            using (var scope = DbHelper.GetTransactionScope())
            {
                DbHelper.ExecuteNonQuery("INSERT INTO Test (No) VALUES ('1')");
                DbHelper.ExecuteNonQuery("INSERT INTO Test (No) VALUES ('2')");
            }
            var dt = DbHelper.ExecuteDataTable("SELECT * FROM Test");
            Assert.AreEqual(dt.Rows.Count, 0);
        }

        [TestFixtureTearDown]
        public void UnSetDebug()
        {
            DbHelper.IsDebug = false;
        }
    }
}