using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HBS.Entities;

namespace HBS.Data.Abstract
{
    public interface ISecurityRepository
    {
        int AddUser(UserProfile user);
        bool UpdateUser(UserProfile user);
        UserProfile GetUser(int userId);
        UserProfile GetUser(string userName);
        List<UserProfile> GetUsers(int companyId);
        List<UserProfile> GetUsers(int companyId, string searchText);
        bool IsUserNameExists(int companyId, string searchText);
        bool AddUserInRole(int userId, int roleId);
        bool RemoveUserFromRole(int roleId, int userId);
        List<Role> GetUserRoles(int userId);
        bool IsUserInRole(int userId, int roleId);
        IQueryable<KendoDDL> GetAllUsers();

        #region ROLES

        int AddRole(Role role);
        bool UpdateRole(Role role);
        Role GetRole(int roleId);
        List<Role> GetRoles(int companyId, string roleName);
        bool RemoveRole(int roleId);

        #endregion

    }


}
