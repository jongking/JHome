using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace JHelper.WebCrawler
{
    public static class CrawlerHelper
    {
        /// <summary>
        /// 下载图片
        /// </summary>
        public static void CrawlImage(string imageUrl, string path, string currentPage = "", string host = "images.dmzj.com")
        {
            System.Net.WebClient client = new System.Net.WebClient();
            client.Headers.Add("Accept", "image/webp,*/*;q=0.8");
            client.Headers.Add("User-Agent", "Microsoft Internet Explorer");
            client.Headers.Add("Host", host);
            client.Headers.Add("Referer", currentPage);   //core
            byte[] file = client.DownloadData(imageUrl);  //important
            //CreateDir
            FileInfo fi = new FileInfo(path);
            if (fi.Directory != null && !fi.Directory.Exists)
            {
                fi.Directory.Create();
            }
            File.WriteAllBytes(path, file);
            client.Dispose();
        }

        public static string CrawlOverToStr(Uri uri, HttpItem httpItem)
        {
            HttpHelper http = new HttpHelper();
            HttpItem item = httpItem;
            item.URL = uri.AbsoluteUri;
            HttpResult result = http.GetHtml(item);
            return result.Html;
        }
        public static string CrawlOverToStr(string uri, HttpItem httpItem)
        {
            return CrawlOverToStr(new Uri(uri), httpItem);
        }
        public static string CrawlOverToStr(Uri uri)
        {
            return CrawlOverToStr(uri, new HttpItem { URL = uri.AbsoluteUri });
        }
        public static string CrawlOverToStr(string uri)
        {
            return CrawlOverToStr(new Uri(uri));
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
