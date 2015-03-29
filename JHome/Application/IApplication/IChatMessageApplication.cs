using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Application.Dto;

namespace Application.IApplication
{
    public interface IChatMessageApplication
    {
        bool Send(string from, string to, string msg);
        bool SendBroadcast(string from, string msg);
        IList<ChatMessageDto> GetMyChatMessages(string userName);
    }
}
