using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using JHelper;

/// <summary>
/// GloPage 的摘要说明
/// </summary>
public class GloPage : Page
{
    public string Action { get { return WebHelper.Request("action", Page); } }
    protected override void OnInit(EventArgs e)
    {
        if (Action == "")
        {
            Response.End();
        }
        base.OnInit(e);
    }
}