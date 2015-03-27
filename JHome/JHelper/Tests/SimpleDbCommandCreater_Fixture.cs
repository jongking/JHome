using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JHelper.DB;
using Microsoft.Practices.EnterpriseLibrary.Data;
using NUnit.Framework;

namespace JHelper.Tests
{
    class SimpleDbCommandCreater_Fixture
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
        public void Can_Select()
        {
            var ssc = SimpleDbCommandCreater.Select("NUnitT");
            var dt = DbHelper.ExecuteDataSet(ssc.ToDbCommand()).Tables[0];

            Assert.AreEqual(dt.Rows.Count, 0);
        }

        [Test]
        public void Can_Insert()
        {
            var effectRow = DbHelper.ExecuteNonQuery(SimpleDbCommandCreater.Insert("NUnitT").AddParam("No", "1").ToDbCommand());
            Assert.AreEqual(effectRow, 1);

            effectRow = DbHelper.ExecuteNonQuery(SimpleDbCommandCreater.Insert("NUnitT").AddParam("No", "2").ToDbCommand());
            Assert.AreEqual(effectRow, 1);
        }

        [Test]
        public void Can_Update()
        {
            Can_Insert();

            var ssc = SimpleDbCommandCreater.Update("NUnitT");
            ssc.AddParam("No", "1");
            ssc.Eq("No", "2");

            var effectRow = DbHelper.ExecuteNonQuery(ssc.ToDbCommand());

            Assert.AreEqual(effectRow, 1);
        }

        [Test]
        public void Can_Delete()
        {
            Can_Insert();

            var ssc = SimpleDbCommandCreater.Delete("NUnitT");
            ssc.Eq("No", "2");

            var effectRow = DbHelper.ExecuteNonQuery(ssc.ToDbCommand());
            DbHelper.GetDatabase().ExecuteSqlStringAccessor<NUnit>("");
            Assert.AreEqual(effectRow, 1);
        }

        [TestFixtureTearDown]
        public void UnSetDebug()
        {
            DbHelper.IsDebug = false;
        }

        public class NUnit
        {
            public string No { get; set; }
        }
    }
}
