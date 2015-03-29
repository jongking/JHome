using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Domain.Exception;
using Domain.IRepository;
using Factory;

namespace Domain.Model
{
    public class ChatMessage
    {
        public readonly static IChatMessageRepository ChatMessageRepository = RepositoryFactory.CreateInstance<IChatMessageRepository>("ChatMessage");

        public int Id { get; set; }
        public int FromUserId { get; set; }
        public int ToUserId { get; set; }
        public string MsgContent { get; set; }
        public DateTime HappenDate { get; set; }
        public bool IsBroadcast { get; set; }

        public ChatMessage()
        {
        }

        public ChatMessage(int from, int to, string msg, bool bc)
        {
            FromUserId = from;
            ToUserId = to;
            MsgContent = msg;
            IsBroadcast = bc;
            HappenDate = DateTime.Now;
        }

        public void Send()
        {
            Check();

            if (!User.HasUser(FromUserId))
            {
                throw new JException("发出消息的用户信息错误,发送失败", ExceptionType.领域模型自检);
            }

            if (!IsBroadcast && !User.HasUser(ToUserId))
            {
                throw new JException("接收消息的用户信息错误,发送失败", ExceptionType.领域模型自检);
            }

            ChatMessageRepository.Add(this);
        }

        private void Check()
        {
            if (MsgContent == null || MsgContent.Trim().Length == 0)
            {
                throw new JException("ChatMessage.MsgContent Error", ExceptionType.领域模型自检);
            }
            if (FromUserId < 0)
            {
                throw new JException("ChatMessage.FromUserId Error", ExceptionType.领域模型自检);
            }
            if (ToUserId < 0)
            {
                throw new JException("ChatMessage.ToUserId Error", ExceptionType.领域模型自检);
            }
        }
    }
}
