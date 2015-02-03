using System;
using System.Collections;
using System.Security.Principal;

namespace HBS.Data.Entities.TimeTracking.Models
{
    public class RoleManager
    {
        public static bool IsUserInRoles(IList roles, IPrincipal user)
        {
            // if roles is empty, return true
            if ((roles == null) || (roles.Count == 0))
                return true;

            foreach (string role in roles)
            {
                if (!string.Equals(role, "*", StringComparison.InvariantCultureIgnoreCase) &&
                   ((user == null) || !user.IsInRole(role)))
                {
                    continue;
                }

                return true;
            }

            return false;
        }

        public static bool IsUserInRoles(string roles, IPrincipal user)
        {
            string[] rolelist = null;
            if (!String.IsNullOrEmpty(roles))
                rolelist = roles.Split(new char[] { ',', ';' }, 512);

            return IsUserInRoles(rolelist, user);
        }

    }
}
