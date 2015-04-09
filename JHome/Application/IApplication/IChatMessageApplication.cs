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
        IList<ChatMessageDto> GetLastMyChatMessages(string userName, int clientUpId = -1, int limit = 0);
        IList<ChatMessageDto> GetMidMyChatMessages(string userName, int clientDnId = -1, int limit = 0);
        int GetLastId(string userName);
    }
}
