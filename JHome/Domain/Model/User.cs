using System;
using System.Text.RegularExpressions;
using Domain.Exception;
using Domain.Factory;
using Domain.IRepository;

namespace Domain.Model
{
    [Serializable]
    public class User
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string PassWord { get; set; }

        public User()
        {
        }

        public User(string userName, string passWord)
        {
            UserName = userName;
            PassWord = passWord;
            Check();
        }

        public bool Reg()
        {
            Check();
            var userRepository = RepositoryFactory.CreateInstance<IUserRepository>("User");
            var nowUsers = userRepository.GetByUserName(UserName);
            if (nowUsers != null) { throw new JException("会员名称重复", ExceptionType.领域模型自检); }
            return true;
        }

        /// <summary>
        /// 领域模型自检
        /// </summary>
        private void Check()
        {
            if (UserName == null || UserName.Length <= 6)
            {
                throw new JException("User.UserName Error", ExceptionType.领域模型自检);
            }
            if (PassWord == null || PassWord.Length <= 6)
            {
                throw new JException("User.PassWord Error", ExceptionType.领域模型自检);
            }
        }
    }
}
