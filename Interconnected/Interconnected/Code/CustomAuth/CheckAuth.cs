using Interconnected.Models;
using Interconnected.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Interconnected.Code.CustomAuth
{
    public class CheckAuth
    {
        RoleModels roleModels = new RoleModels();
        CustomPrincipal prin = (CustomPrincipal)HttpContext.Current.User;
        public bool checkUserAdd(USER userCheck)
        {
            ROLE Role=roleModels.GetItem(userCheck.ID_ROLE);
            if (prin.ROLE.Equals(ConstanAppkey.ADMIN()))
            {
                if (!Role.NAME.Equals(ConstanAppkey.ADMIN()))
                    return true;
            }
            else if (prin.ROLE.Equals(ConstanAppkey.MOD()))
            {
                if (Role.NAME.Equals(ConstanAppkey.USER()))
                    return true;
            }
            return false;
        }
        public bool checkUserEdit(USER userCheck)
        {
            ROLE Role = roleModels.GetItem(userCheck.ID_ROLE);
            if (prin.ID == userCheck.ID)
                return true;
            if (prin.ROLE.Equals(ConstanAppkey.ADMIN()))
            {
                if (!Role.NAME.Equals(ConstanAppkey.ADMIN()))
                    return true;
            }
            else if (prin.ROLE.Equals(ConstanAppkey.MOD()))
            {
                if (Role.NAME.Equals(ConstanAppkey.USER()))
                    return true;
            }
            return false;
        }
        public bool checkPostDelete(POST postCheck)
        {
            ROLE Role = roleModels.GetItem(postCheck.USER.ID_ROLE);
            if (postCheck.USER.ID == prin.ID)
                return true;
            if (prin.ROLE.Equals(ConstanAppkey.ADMIN()))
            {
                return true;
            }
            else if (prin.ROLE.Equals(ConstanAppkey.MOD()))
            {
                if (Role.NAME.Equals(ConstanAppkey.USER()))
                    return true;
            }
            return false;
        }
    }
}