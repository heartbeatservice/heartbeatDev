using System.Collections.Generic;
using System.Linq;
using HBS.Data.Entities.TimeTracking.EF;

namespace HBS.Data.Entities.TimeTracking.Models
{
    public class WebMenuRole
    {
        public int MenuId { get; set; }
        public string MenuTitle { get; set; }
        public string Action { get; set; }
        public string Controller { get; set; }
        public int? OrderNumber { get; set; }
        //public Guid RoleId { get; set; }
        //public string RoleName { get; set; }

        public List<WebMenuRole> GetMenuItemsForRoles(List<string> roleNames)
        {
            using (var dbContext = new TimeTrackingEntities())
            {
                var menuRole =  (from wm in dbContext.WebMenus
                                from role in wm.aspnet_Roles
                                where roleNames.Contains(role.RoleName)
                                select new WebMenuRole
                                       {
                                           MenuId = wm.MenuId,
                                           MenuTitle = wm.Title,
                                           Action = wm.Action,
                                           Controller = wm.Controller,
                                           OrderNumber = wm.OrderNumber,
                                       }).Distinct().OrderBy(c=>c.OrderNumber).ToList();
                return menuRole;
            }
        }
    }
}
