using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace JHelper.Tests
{
    [TestFixture]
    public class ReflectionHelpe_Fixture
    {
        public bool Tof = false;
        public void SetTrue()
        {
            Tof = true;
        }

        [Test]
        public void Can_RunMethod_Only_By_String()
        {
            ReflectionHelper.RunMethod(this, "SetTrue");

            Assert.AreEqual(Tof, true);
        }
    }
}
