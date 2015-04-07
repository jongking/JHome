﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Domain.Model;
using JHelper.DB;

namespace Application.Dto
{
    public class UserDto
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string PassWord { get; set; }

        public UserDto()
        {
        }
        public UserDto(User user)
        {
            Id = user.Id;
            UserName = user.UserName;
            PassWord = user.PassWord;
        }

        internal static UserDto GetById(int id)
        {
            return BaseDto.DtoRepository.GetModel<UserDto>(SimpleSqlCreater.Select<UserDto>().Eq("Id", id.ToString()).ToString());
        }
    }
}
