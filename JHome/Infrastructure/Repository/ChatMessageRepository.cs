using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Domain.IRepository;
using Domain.Model;
using JHelper.DB;

namespace Infrastructure.Repository
{
    public class ChatMessageRepository : IChatMessageRepository
    {
        public void Add(ChatMessage chatMessage)
        {
            DbHelper.InsertModel(chatMessage);
        }

        public IList<ChatMessage> GetByBroadcast(bool isBroadcast)
        {
            return DbHelper.GetList<ChatMessage>(SimpleSqlCreater
                .Select<ChatMessage>()
                .Eq("IsBroadcast", isBroadcast.ToString())
                .ToString());
        }
    }
}
