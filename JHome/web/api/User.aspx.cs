using System;
using System.Activities.Expressions;
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
    private readonly IUserApplication _iUserApplication = Factory.ApplicationFactory.CreateInstance<IUserApplication>("User");

    public void Reg()
    {
        var userName = WebHelper.Request("UserName", Page);
        var passWord = WebHelper.Request("PassWord", Page);
        
        _iUserApplication.Reg(userName, passWord);
    }

    public void Login()
    {
        var userName = WebHelper.Request("UserName", Page);
        var passWord = WebHelper.Request("PassWord", Page);

        Helper.SetAuthen(userName, Page);
    }

    public void Check()
    {
        if (Helper.CheckAuthen(Page))
        {
            JsonResult.SetDateByKeyValue(new KeyValue
            {
                { "Login", "true" },
            });
        }
    }
}