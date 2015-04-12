using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Application.Dto;
using Domain.Model;

namespace Application.IApplication
{
    public interface IUserApplication
    {
        bool Reg(string userName, string passWord);
        bool Login(string userName, string passWord);
        UserDto Get(string userName);
        UserDto Get(int id);
        List<UserDto> GetAll();
    }
}
