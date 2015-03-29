using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Domain.Model;
using JHelper;

namespace Application.Dto
{
    public class ChatMessageDto
    {
        public int Id { get; set; }
        public int FromUserId { get; set; }
        public int ToUserId { get; set; }
        public string MsgContent { get; set; }
        public string HappenDate { get; set; }
        public bool IsBroadcast { get; set; }

        public ChatMessageDto(ChatMessage chatMessage)
        {
            Id = chatMessage.Id;
            FromUserId = chatMessage.ToUserId;
            ToUserId = chatMessage.ToUserId;
            MsgContent = chatMessage.MsgContent;
            HappenDate = DateHelper.DateFormat(chatMessage.HappenDate);
            IsBroadcast = chatMessage.IsBroadcast;
        }
    }
}
