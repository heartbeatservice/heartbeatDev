using System.Collections.Generic;
using System.Linq;
using HeartbeatService.Data.Entities.TimeTracking.Models;
using HeartbeatService.Data.Entities.TimeTracking.ViewModels;


namespace HeartbeatService.TimeTrackingSystem.Models
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

        public virtual UserModel UserModel { get; set; }
        public List<string> Roles { get; set; }
        public WeeklyTimeTrack WeeklyTimeTrackModel { get; set; }

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
    public class CreateUserRoleViewModel: UserRoleViewModel
    {
        public new CreateUserModel UserModel { get; set; }
        
        public CreateUserRoleViewModel()
        {
            UserModel = new CreateUserModel();
            Roles = System.Web.Security.Roles.GetAllRoles().ToList();
        }
        

    }
}