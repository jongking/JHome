using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Application.Dto;
using Application.IApplication;
using Domain.Model;

namespace Application.ApplicationImpl
{
    public class ChatMessageApplication : IChatMessageApplication
    {
        public bool Send(string fromName, string toName, string msg)
        {
            var from = User.UserRepository.GetByUserName(fromName).Id;
            var to = User.UserRepository.GetByUserName(toName).Id;

            var chatMsg = new ChatMessage(from, to, msg, false);

            chatMsg.Send();

            return true;
        }

        public bool SendBroadcast(string fromName, string msg)
        {
            var from = User.UserRepository.GetByUserName(fromName).Id;

            var chatMsg = new ChatMessage(from, 0, msg, true);

            chatMsg.Send();

            return true;
        }

        public IList<ChatMessageDto> GetMyChatMessages(string userName)
        {
            return ChatMessageDto.GetMyChatMessages(userName);
        }

        public IList<ChatMessageDto> GetMyChatMessages(string userName, int clid)
        {
            return ChatMessageDto.GetMyChatMessages(userName, clid);
        }

        public int GetLastId(string userName)
        {
            return ChatMessageDto.GetLastId(userName);
        }
    }
}
