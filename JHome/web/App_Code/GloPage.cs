using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using JHelper;

/// <summary>
/// GloPage 的摘要说明
/// </summary>
public class GloPage : Page
{
    protected string Result = "";
    public string Action { get { return WebHelper.Request("action", Page); } }
    protected override void OnInit(EventArgs e)
    {
        if (Action == "")
        {
            Response.End();
        }
        base.OnInit(e);
    }

    protected override void OnLoadComplete(EventArgs e)
    {
        Response.Expires = -1;
        Response.Clear();
        Response.ContentEncoding = Encoding.UTF8;
        Response.ContentType = "application/json";
        Response.Write(Result);
        Response.Flush();
        Response.End();
        base.OnLoadComplete(e);
    }
}