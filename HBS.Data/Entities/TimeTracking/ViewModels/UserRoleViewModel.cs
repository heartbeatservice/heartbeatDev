using System.Collections.Generic;
using System.Linq;
using HBS.Data.Entities.TimeTracking.Models;

namespace HBS.Data.Entities.TimeTracking.ViewModels
{
    public class UserRoleViewModel
    {
        public UserRoleViewModel()
        {
            UserModel = new UserModel();
            Roles = System.Web.Security.Roles.GetAllRoles().ToList();
        }

        public UserRoleViewModel(string userName)
        {
            UserModel = new UserModel(MembershipUserExtended.GetUser(userName, false));
            Roles = System.Web.Security.Roles.GetAllRoles().ToList();
            WeeklyTimeTrackModel = TimeTrackManager.GetCurrentWeekClockInOutTime(userName);
        }

        public UserModel UserModel { get; set; }
        public List<string> Roles { get; set; }
        public WeeklyTimeTrackWeekListViewModel WeeklyTimeTrackModel { get; set; }

        public Dictionary<string, bool> RolesUserBelongs 
        { 
            get
            {
                var roles = (from r in Roles
                                select r).ToDictionary(c => c, d => false);
                if (UserModel != null && UserModel.UserRoles != null)
                    foreach (var userRole in UserModel.UserRoles.Where(roles.ContainsKey))
                    {
                        roles[userRole] = true;
                    }
                return roles;
            }
        }
        
    }
}