﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Application.IApplication;
using Factory;
using JHelper;
using JHelper.WebCrawler;
using Microsoft.ClearScript;
using Microsoft.ClearScript.V8;
using Microsoft.ClearScript.Windows;

public partial class api_Crawler : GloPage
{
    private readonly IComicApplication _comicApplication = ApplicationFactory.CreateInstance<IComicApplication>("Comic");

    public void RunJs()
    {
        var js = WebHelper.Request("JS", Page);
        var jsResultWrap = new JsResultWrap();

        using (ScriptEngine engine = new JScriptEngine())
        {
            //添加用于返回的result对象
            engine.AddHostObject("Console", jsResultWrap);
            //爬虫帮助类,用于获取web页面信息
            engine.AddHostType("CrawlerHelper", typeof(CrawlerHelper));
            //正则帮助类,用于提取信息
            engine.AddHostType("RegexHelper", typeof(RegexHelper));
            //剖析器帮助类,用于解释提取页面的信息
            engine.AddHostType("ParserHelper", typeof(ParserHelper));
            //添加foreach遍历的等方法
            engine.AddHostType("Helper", typeof(Extensions));
            
            //添加漫画的应用程序类
            engine.AddHostObject("ComicApp", _comicApplication);

            //执行js
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

        public void Write(string str, int mode = 0)
        {
            if (mode == 0)
            {
                result += "<span class='text-primary'>Console -> </span>" + WebHelper.HtmlEncode(str) + "<br/>";
            }
            else if (mode == 1)
            {
                result += "<span class='text-primary'>Console -> </span><span class='text-danger'>" + WebHelper.HtmlEncode(str) + "</span><br/>";
            }
        }
    }
    public static class Extensions
    {
        public static void ForEach(IEnumerable collection, dynamic action)
        {
            int i = 0;
            foreach (var item in collection)
            {
                action(item, i);
                i++;
            }
        }
    }
}