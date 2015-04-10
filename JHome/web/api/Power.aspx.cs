using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Application.Dto;
using JHelper;

public partial class api_Power : GloPage
{
    public void CheckPower()
    {
        var user = Helper.GetLoginUser(Page);
        if (user == null)
        {
            JsonResult.SetDateByClass(false);
            return;
        }

        var powerId = Convert.ToInt32(WebHelper.Request("PowerId", Page));
        JsonResult.SetDateByClass(user.CheckPower(powerId));
    }
}