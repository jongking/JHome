using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Domain.IRepository;
using Domain.Model;
using Domain.Model.Role;
using JHelper.DB;

namespace Infrastructure.Repository
{
    public class RoleRepository : IRoleRepository
    {
        public RolePower GetByPowerIdWithRoleId(int powerId, int roleId)
        {
            return DbCacheHelper.GetCache<RolePower>().FirstOrDefault(p => p.RoleId == roleId && p.PowerId == powerId);
//            return DbHelper.GetModel<RolePower>(
//                SimpleSqlCreater.Select<RolePower>()
//                .Eq("RoleId", roleId.ToString())
//                .Eq("PowerId", powerId.ToString())
//                .ToString());
        }
    }
}
