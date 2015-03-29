using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Domain.Model;

namespace Application.Dto
{
    public class UserDto
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string PassWord { get; set; }

        public UserDto(User user)
        {
            Id = user.Id;
            UserName = user.UserName;
            PassWord = user.PassWord;
        }
    }
}
