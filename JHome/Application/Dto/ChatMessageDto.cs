using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Domain.Model;
using JHelper;
using JHelper.DB;

namespace Application.Dto
{
    public class ChatMessageDto
    {
        public int Id { get; set; }
        public int FromUserId { get; set; }
        public int ToUserId { get; set; }
        public string MsgContent { get; set; }
        public DateTime HappenDate { get; set; }
        public string HappenDateStr { get { return DateHelper.DateFormat(HappenDate); } }
        public bool IsBroadcast { get; set; }
        public string FromUserName { get; set; }
        public string ToUserName { get; set; }

        public ChatMessageDto()
        {
            
        }

        public ChatMessageDto(ChatMessage chatMessage)
        {
            Id = chatMessage.Id;
            FromUserId = chatMessage.FromUserId;
            ToUserId = chatMessage.ToUserId;
            MsgContent = chatMessage.MsgContent;
            HappenDate = chatMessage.HappenDate;
            IsBroadcast = chatMessage.IsBroadcast;
            FromUserName = UserDto.GetById(FromUserId).UserName;
            ToUserName = UserDto.GetById(ToUserId).UserName;
        }

        internal static IList<ChatMessageDto> GetMyChatMessages(string userName)
        {
            SimpleSqlCreater ssc = SimpleSqlCreater
                .Select<ChatMessageDto>()
                .Eq("FromUserName", userName)
                .Or()
                .Eq("ToUserName", userName)
                .Or()
                .Eq("IsBroadcast", "1");
            return BaseDto.DtoRepository.GetList<ChatMessageDto>(ssc.ToString());
        }

        internal static IList<ChatMessageDto> GetMyChatMessages(string userName, int clientLastId)
        {
            SimpleSqlCreater ssc = SimpleSqlCreater
                .Select<ChatMessageDto>()
                .Combine(
                SimpleSqlCreater
                .Where()
                .Eq("FromUserName", userName)
                .Or()
                .Eq("ToUserName", userName)
                .Or()
                .Eq("IsBroadcast", "1")
                )
                .And()
                .Big("Id", clientLastId.ToString());
            return BaseDto.DtoRepository.GetList<ChatMessageDto>(ssc.ToString());
        }

        internal static int GetLastId(string userName)
        {
            SimpleSqlCreater ssc = SimpleSqlCreater
                .Select<ChatMessageDto>()
                .Eq("FromUserName", userName)
                .Or()
                .Eq("ToUserName", userName)
                .Or()
                .Eq("IsBroadcast", "1")
                .Limit(1)
                .OrderBy("Id", SimpleSqlCreater.OrderByType.Desc);
            return BaseDto.DtoRepository.GetModel<ChatMessageDto>(ssc.ToString()).Id;
        }
    }
}
