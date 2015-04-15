using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JHelper.WebCrawler;
using NUnit.Framework;

namespace JHelper.Tests
{
    [TestFixture]
    class CrawlerHelper_Fixture
    {
        [Test]
        public void Can_CrawlOver_Html()
        {
            var result = CrawlerHelper.CrawlOver(new Uri("http://manhua.dmzj.com/"));

            Assert.AreNotEqual(result, null);
        }
    }
}
