using System;

namespace Domain.Model
{
    [Serializable]
    public class Users
    {
        public virtual Guid Id { get; set; }
        public virtual string UserName { get; set; }
        public virtual string PassWord { get; set; }
    }
}
