using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
using JHelper.DB;
using NUnit.Framework;

namespace JHelper.Tests
{
    [TestFixture]
    class DbHelp_Fixture
    {
        [TestFixtureSetUp]
        public void SetDebug()
        {
            DbHelper.IsDebug = true;
        }
        [SetUp]
        public void ClearNUnit()
        {
            DbHelper.ExecuteNonQuery("DELETE FROM NUnitT WHERE 1=1");
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
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
            {
                DbHelper.ExecuteNonQuery("INSERT INTO NUnitT (No) VALUES ('1')");
                DbHelper.ExecuteNonQuery("INSERT INTO NUnitT (No) VALUES ('2')");
                scope.Complete();
            }
            var dt = DbHelper.ExecuteDataTable("SELECT * FROM NUnitT");
            Assert.AreEqual(dt.Rows.Count, 2);
        }
        [Test]
        public void Can_TransactionScope_Not_Complete()
        {
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew, TimeSpan.FromSeconds(30)))
            {
                DbHelper.ExecuteNonQuery("INSERT INTO NUnitT (No) VALUES ('1')");
                DbHelper.ExecuteNonQuery("INSERT INTO NUnitT (No) VALUES ('2')");
            }
            var dt = DbHelper.ExecuteDataTable("SELECT * FROM NUnitT");
            Assert.AreEqual(dt.Rows.Count, 0);
        }

        [TestFixtureTearDown]
        public void UnSetDebug()
        {
            DbHelper.IsDebug = false;
        }
    }
}
