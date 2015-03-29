using System;
using System.Text.RegularExpressions;
using Domain.Exception;
using Domain.IRepository;
using Factory;

namespace Domain.Model
{
    [Serializable]
    public class User
    {
        public readonly static IUserRepository UserRepository = RepositoryFactory.CreateInstance<IUserRepository>("User");

        public int Id { get; set; }
        public string UserName { get; set; }
        public string PassWord { get; set; }

        public User()
        {
        }

        public User(string userName, string passWord)
        {
            UserName = userName;
            PassWord = passWord;
        }

        public bool Reg()
        {
            Check();

            CheckUserRepeat(UserName);

            UserRepository.Add(this);

            return true;
        }

        private void CheckUserRepeat(string userName)
        {
            if (HasUser(userName))
            {
                throw new JException("会员名称重复", ExceptionType.领域模型自检);
            }
        }

        /// <summary>
        /// 领域模型自检
        /// </summary>
        private void Check()
        {
            if (UserName == null || UserName.Length < 6)
            {
                throw new JException("User.UserName Error", ExceptionType.领域模型自检);
            }
            if (PassWord == null || PassWord.Length < 8)
            {
                throw new JException("User.PassWord Error", ExceptionType.领域模型自检);
            }
        }

        public bool Login()
        {
            Check();

            var user = UserRepository.GetByUserName(UserName);

            return user.Id != 0 && user.PassWord == PassWord;
        }

        public static bool HasUser(int userId)
        {
            var user = UserRepository.GetById(userId);
            return user != null && user.Id > 0;
        }
        public static bool HasUser(string userName)
        {
            var user = UserRepository.GetByUserName(userName);
            return user != null && user.Id > 0;
        }
    }
}
