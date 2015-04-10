using Domain.Exception;
using Domain.IRepository;
using Factory;

namespace Domain.Model.Role
{
    public class Role
    {
        public readonly static IRoleRepository RoleRepository = RepositoryFactory.CreateInstance<IRoleRepository>("Role");

        public int Id { get; set; }
        public string RoleName { get; set; }

        public bool CheckPower(int powerId)
        {
            if (Id == 0)
            {
                return false;
            }
            
            var power = RoleRepository.GetByPowerIdWithRoleId(powerId, Id);

            return power.PowerId == powerId && power.RoleId == Id;
        }

        /// <summary>
        /// 领域模型自检
        /// </summary>
        private void Check()
        {
            if (string.IsNullOrEmpty(RoleName))
            {
                throw new JException("Role.RoleName Error", ExceptionType.领域模型自检);
            }
        }
    }
}
