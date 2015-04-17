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
            var result = CrawlerHelper.CrawlOverToStr(new Uri("http://manhua.dmzj.com/"));

            Assert.AreNotEqual(result, null);
        }

        [Test]
        public void Can_CrawlerResult_GetContextCoverBy_Title()
        {
            var result = new CrawlerResult("d<title>在线漫画,火影忍者,海贼王,死神,动漫之家漫画网</title>d");
            result.GetContextCoverBy("<title>", "</title>");
            result.GetContextCoverBy(",", ",");

            Assert.AreEqual(result.ToString(), ",火影忍者,海贼王,死神,");
        }

        [Test]
        public void Can_CrawlerResult_GetContextCover_Title()
        {
            var result = new CrawlerResult("d<title>在线漫画,火影忍者,海贼王,死神,动漫之家漫画网</title>d");
            result.GetContextCover("<title>", "</title>");
            Assert.AreEqual(result.ToString(), "在线漫画,火影忍者,海贼王,死神,动漫之家漫画网");
            result.GetContextCover(",", ",");
            Assert.AreEqual(result.ToString(), "火影忍者,海贼王,死神");
        }

        [Test]
        public void Can_CrawlOverToCrawlerResult_And_GetContextCoverBy_Title()
        {
            var result = CrawlerHelper.CrawlOverToCrawlerResult(new Uri("http://manhua.dmzj.com/"));
            result.GetContextCoverBy("<title>", "</title>");

            Assert.AreEqual(result.ToString(), "<title>在线漫画,火影忍者,海贼王,死神,动漫之家漫画网</title>");
        }
    }
}
