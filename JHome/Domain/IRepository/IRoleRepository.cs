using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Domain.Model.Role;

namespace Domain.IRepository
{
    public interface IRoleRepository
    {
        RolePower GetByPowerIdWithRoleId(int powerId, int roleId);
    }
}
