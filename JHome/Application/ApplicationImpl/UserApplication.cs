using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Application.Dto;
using Application.IApplication;
using Domain.Model;

namespace Application.ApplicationImpl
{
    public class UserApplication : IUserApplication
    {
        public bool Reg(string userName, string passWord)
        {
            var user = new User(userName, passWord);
            return user.Reg();
        }

        public bool Login(string userName, string passWord)
        {
            var user = new User(userName, passWord);
            return user.Login();
        }

        public UserDto Get(string userName)
        {
            return new UserDto(User.UserRepository.GetByUserName(userName));
        }

        public UserDto Get(int id)
        {
            return UserDto.GetById(id);
        }

        public List<UserDto> GetAll()
        {
            return UserDto.GetAll();
        }
    }
}
