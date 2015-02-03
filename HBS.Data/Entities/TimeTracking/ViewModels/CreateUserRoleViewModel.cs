using System.Linq;
using HBS.Data.Entities.TimeTracking.Models;

namespace HBS.Data.Entities.TimeTracking.ViewModels
{
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