using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JHelper.WebCrawler
{
    public static class CrawlerHelper
    {
        public static string CrawlOver(Uri uri)
        {
            HttpHelper http = new HttpHelper();
            HttpItem item = new HttpItem {URL = uri.AbsoluteUri};
            HttpResult result = http.GetHtml(item);
            return result.Html;
        }
    }
}
