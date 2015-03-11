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
    protected string TextResult = "";

    protected JsonResult JsonResult = new JsonResult();
    public string Action { get { return WebHelper.Request("action", Page); } }
    protected override void OnInit(EventArgs e)
    {
        if (Action == "")
        {
            Response.End();
        }

        try
        {
            ReflectionHelper.RunMethod(this, Action);
        }
        catch (NullReferenceException)
        {
            JsonResult.Code = JsonResult.ResultCode.错误;
            JsonResult.ErrorReson = string.Format("no action [{0}]", Action);
        }

        base.OnInit(e);
    }

    protected override void OnLoadComplete(EventArgs e)
    {
        Response.Expires = -1;
        Response.Clear();
        Response.ContentEncoding = Encoding.UTF8;
        if (TextResult.Length == 0)
        {
            Response.ContentType = "application/json";
            Response.Write(JsonResult.ToString());
        }
        else
        {
            Response.Write(TextResult);
        }
        Response.Flush();
        Response.End();
        base.OnLoadComplete(e);
    }
}