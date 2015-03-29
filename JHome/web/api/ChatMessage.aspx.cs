using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Application.IApplication;
using Factory;
using JHelper;

public partial class api_ChatMessage : GloPage
{
    private readonly IChatMessageApplication _chatMessageApplication = ApplicationFactory.CreateInstance<IChatMessageApplication>("ChatMessage");

    public void Send()
    {
        var fromUserName = Helper.GetLoginUser(Page).UserName;
        var toUserName = WebHelper.Request("UserName", Page);
        var message = WebHelper.Request("Message", Page);

        _chatMessageApplication.Send(fromUserName, toUserName, message);
    }

    public void SendBroadcast()
    {
        var userName = Helper.GetLoginUser(Page).UserName;
        var message = WebHelper.Request("Message", Page);

        _chatMessageApplication.SendBroadcast(userName, message);
    }

    public void GetMsg()
    {
        var userName = Helper.GetLoginUser(Page).UserName;

        var msgs = _chatMessageApplication.GetMyChatMessages(userName);

        JsonResult.SetDateByClass(msgs);
    }
}