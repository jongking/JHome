using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CsQuery;
using HtmlAgilityPack;

namespace JHelper
{
    public class ParserHelper
    {
        public static XPathDoc GetXPathParserDoc(string parserStr)
        {
            var document = new XPathDoc();

            document.LoadHtml(parserStr);

            return document;
        }
        
        public static HtmlNode GetXPathParserNode(string parserStr)
        {
            return HtmlNode.CreateNode(parserStr);
        }

        public static QueryDom GetCsQueryParserDoc(string parserStr)
        {
            QueryDom dom = new QueryDom(parserStr);
            return dom;
        }
    }

    public class XPathDoc : HtmlDocument
    {
        
    }

    public class QueryDom : CQ
    {
        public QueryDom(string parserStr) : base(parserStr, HtmlParsingMode.Document, HtmlParsingOptions.Default, DocType.Default)
        {
        }
    }
}
