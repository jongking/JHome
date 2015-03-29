using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Domain.Model;

namespace Domain.IRepository
{
    public interface IChatMessageRepository
    {
        void Add(ChatMessage chatMessage);
        IList<ChatMessage> GetByBroadcast(bool isBroadcast);
    }
}
