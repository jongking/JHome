using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JHelper.WebCrawler
{
    public static class CrawlerHelper
    {
        public static string CrawlOverToStr(string uri)
        {
            return CrawlOverToStr(new Uri(uri));
        }

        public static string CrawlOverToStr(Uri uri)
        {
            HttpHelper http = new HttpHelper();
            HttpItem item = new HttpItem {URL = uri.AbsoluteUri};
            HttpResult result = http.GetHtml(item);
            return result.Html;
        }

        public static CrawlerResult CrawlOverToCrawlerResult(string uri)
        {
            return new CrawlerResult(CrawlOverToStr(uri));
        }
        public static CrawlerResult CrawlOverToCrawlerResult(Uri uri)
        {
            return new CrawlerResult(CrawlOverToStr(uri));
        }
    }

    public class CrawlerResult
    {
        private string _crawlStr;
        public CrawlerResult(string crawlStr)
        {
            _crawlStr = crawlStr;
        }
        /// <summary>
        /// 获取用left和right包围的内容,返回不包括left和right
        /// </summary>
        public CrawlerResult GetContextCover(string left, string right)
        {
            _crawlStr = RegexHelper.GetContextCoverS(_crawlStr, left, right);
            return this;
        }
        public CrawlerResult GetContextCoverBy(string left, string right)
        {
            _crawlStr = RegexHelper.GetContextCoverByS(_crawlStr, left, right);
            return this;
        }
        public override string ToString()
        {
            return _crawlStr;
        }
    }
}
