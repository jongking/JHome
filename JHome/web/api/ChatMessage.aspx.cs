using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Application.Dto;
using Application.IApplication;
using Factory;
using JHelper;

public partial class api_ChatMessage : GloPage
{
    private readonly IChatMessageApplication _chatMessageApplication = ApplicationFactory.CreateInstance<IChatMessageApplication>("ChatMessage");

    public void Send()
    {
        var user = Helper.GetLoginUser(Page);
        if (user == null)
        {
            JsonResult.Error("请先登录");
            return;
        }

        var fromUserName = user.UserName;
        var toUserName = WebHelper.Request("UserName", Page);
        var message = WebHelper.Request("Message", Page);

        _chatMessageApplication.Send(fromUserName, toUserName, message);
    }

    public void SendBroadcast()
    {
        var user = Helper.GetLoginUser(Page);
        if (user == null)
        {
            JsonResult.Error("请先登录");
            return;
        }

        var userName = user.UserName;
        var message = WebHelper.Request("Message", Page);

        _chatMessageApplication.SendBroadcast(userName, message);
    }

    public void GetMsg()
    {
        var user = Helper.GetLoginUser(Page);
        if (user == null) {
            JsonResult.SetDateByClass(new List<ChatMessageDto>());
            return;
        }
        var userName = user.UserName;

        var clientLastMsgId = Convert.ToInt32(WebHelper.Request("LastMsgId", Page));

        var serverLastMsgId = _chatMessageApplication.GetLastId(userName);

        if (serverLastMsgId > clientLastMsgId)
        {
            var msgs = _chatMessageApplication.GetMyChatMessages(userName, clientLastMsgId);

            var msgwrap = new MessageWrap()
            {
                CmList = msgs,
                LastId = serverLastMsgId
            };
            JsonResult.SetDateByClass(msgwrap);
        }
        else
        {
            JsonResult.SetDateByClass(new List<ChatMessageDto>());
        }
    }

    private class MessageWrap
    {
        public IList<ChatMessageDto> CmList;
        public int LastId = -1;
    }
}