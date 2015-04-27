using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using JHelper;
using Microsoft.ClearScript;
using Microsoft.ClearScript.V8;
using Microsoft.ClearScript.Windows;

public partial class api_Crawler : GloPage
{
    public void RunJs()
    {
        var js = WebHelper.Request("JS", Page);
        var jsResultWrap = new JsResultWrap();


        using (ScriptEngine engine = new JScriptEngine())
        {
            //添加用于返回的result对象
//            engine.AddHostObject("result", result);

            engine.AddHostObject("jsResultWrap", jsResultWrap);
            engine.Execute(js);
        }

        JsonResult.SetDateByClass(jsResultWrap);
    }
    public void GetJs()
    {
        var jsResultWrap = new JsResultWrap();
        var serpath = Server.MapPath("../temp/comicCrawler/1.txt");
        jsResultWrap.result = File.ReadAllText(serpath);
        JsonResult.SetDateByClass(jsResultWrap);
    }
    public void SaveJs()
    {
        var js = WebHelper.Request("JS", Page);
        var serpath = Server.MapPath("../temp/comicCrawler/1.txt");
        File.WriteAllText(serpath, js);
        JsonResult.SetDateByClass(new JsResultWrap());
    }
    public class JsResultWrap
    {
        public string result = "";
    }

    
}