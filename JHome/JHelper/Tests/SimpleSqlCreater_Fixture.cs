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
    class SimpleSqlCreater_Fixture
    {
        [TestFixtureSetUp]
        public void SetDebug()
        {
            DbHelper.IsDebug = true;
        }

        [SetUp]
        [TearDown]
        public void ClearNUnit()
        {
            DbHelper.ExecuteNonQuery("DELETE FROM NUnitT WHERE 1=1");
        }

        [Test]
        public void Can_Select()
        {
            var ssc = SimpleSqlCreater.Select("NUnitT");
            var dt = DbHelper.ExecuteDataTable(ssc.ToString());

            Assert.AreEqual(dt.Rows.Count, 0);
        }

        [Test]
        public void Can_Select_Model()
        {
            var ssc = SimpleSqlCreater.Select<NUnit>();
            var dt = DbHelper.ExecuteDataTable(ssc.ToString());

            Assert.AreEqual(dt.Rows.Count, 0);
        }

        [Test]
        public void Can_Insert()
        {
            var effectRow = DbHelper.ExecuteNonQuery(SimpleSqlCreater.Insert("NUnitT").AddParam("No", "1").ToString());
            Assert.AreEqual(effectRow, 1);

            effectRow = DbHelper.ExecuteNonQuery(SimpleSqlCreater.Insert("NUnitT").AddParam("No", "2").ToString());
            Assert.AreEqual(effectRow, 1);
        }

        [Test]
        public void Can_Insert_Model()
        {
            var effectRow = DbHelper.ExecuteNonQuery(SimpleSqlCreater.Insert<NUnit>().AddParam("No", "1").ToString());
            Assert.AreEqual(effectRow, 1);

            effectRow = DbHelper.ExecuteNonQuery(SimpleSqlCreater.Insert<NUnit>().AddParam("No", "2").ToString());
            Assert.AreEqual(effectRow, 1);
        }

        [Test]
        public void Can_Update()
        {
            Can_Insert_Model();

            var ssc = SimpleSqlCreater.Update("NUnitT");
            ssc.AddParam("No", "1");
            ssc.Eq("No", "2");

            var effectRow = DbHelper.ExecuteNonQuery(ssc.ToString());

            Assert.AreEqual(effectRow, 1);
        }
        [Test]
        public void Can_Update_Model()
        {
            Can_Insert_Model();

            var ssc = SimpleSqlCreater.Update<NUnit>();
            ssc.AddParam("No", "1");
            ssc.Eq("No", "2");

            var effectRow = DbHelper.ExecuteNonQuery(ssc.ToString());

            Assert.AreEqual(effectRow, 1);
        }

        [Test]
        public void Can_Delete()
        {
            Can_Insert_Model();

            var ssc = SimpleSqlCreater.Delete("NUnitT");
            ssc.Eq("No", "2");

            var effectRow = DbHelper.ExecuteNonQuery(ssc.ToString());

            Assert.AreEqual(effectRow, 1);
        }

        [Test]
        public void Can_Delete_Model()
        {
            Can_Insert_Model();

            var ssc = SimpleSqlCreater.Delete<NUnit>();
            ssc.Eq("No", "2");

            var effectRow = DbHelper.ExecuteNonQuery(ssc.ToString());

            Assert.AreEqual(effectRow, 1);
        }

        [TestFixtureTearDown]
        public void UnSetDebug()
        {
            DbHelper.IsDebug = false;
        }

    }
}
