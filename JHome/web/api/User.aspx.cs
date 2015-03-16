using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using Application.ApplicationImpl;
using Application.IApplication;
using Domain.Model;
using JHelper;

public partial class api_User : GloPage
{
    public void Reg()
    {
        var userName = WebHelper.Request("UserName", Page);
        var passWord = WebHelper.Request("PassWord", Page);
        var iUserApplication = Factory.ApplicationFactory.CreateInstance<IUserApplication>("User");
        iUserApplication.Reg(userName, passWord);

        var kv = new KeyValue
        {
            {"OK", "OK"},
        };
        JsonResult.SetDateByKeyValue(kv);
    }
}