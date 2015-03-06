using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;

namespace JHelper
{
    public static class WebHelper
    {
        public static string Request(string key, Page page)
        {
            return page.Request[key] ?? "";
        }
    }
}
