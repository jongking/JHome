using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.Script.Serialization;

namespace JHelper
{
    /// <summary>
    /// web帮助方法
    /// </summary>
    public static class WebHelper
    {
        public static string Request(string key, Page page)
        {
            return page.Request[key] ?? "";
        }

        public static string JsonSerialize(object obj)
        {
            var jss = new JavaScriptSerializer();
            jss.MaxJsonLength = Int32.MaxValue;
            string jsonStr = jss.Serialize(obj);
            return jsonStr;
        }

        public static T JsonDeserialize<T>(string str)
        {
            var jss = new JavaScriptSerializer();
            T obj = jss.Deserialize<T>(str);
            return obj;
        }
    }
}
