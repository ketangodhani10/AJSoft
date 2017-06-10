using AJSoftEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AJSoftBAL
{
    public class RoleBL
    {
        #region Get Methods

        //Get User details by Name
        public List<Role> GetByRoleGroup(string RoleGroup)
        {
            try
            {
                using (var ctx = new DBAJEntities())
                {
                    return ctx.Roles.Where(p => p.RoleGroup == RoleGroup).OrderBy(c => c.RoleName).ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Role GetByRoleName(string RoleName)
        {
            try
            {
                using (var ctx = new DBAJEntities())
                {
                    return ctx.Roles.Where(p => p.RoleName == RoleName).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion
    }
}
