using System.Collections.Generic;
using System.Web.Mvc;
using HBS.Data.Entities.TimeTracking.Models;

namespace HBS.Data.Entities.TimeTracking.Models
{
    public class UserList
    {
        public string SelectedValue { get; set; }
        public IEnumerable<SelectListItem> Users { get; set; }
        
        public UserList(string selectedValue)
        {
            SelectedValue = selectedValue;
            
            if(!string.IsNullOrEmpty(SelectedValue))
                Users=new SelectList(MembershipUserExtended.GetExtendedMembershipUserCollection(), "UserName", "FullName", selectedValue);
            else
                Users=new SelectList(MembershipUserExtended.GetExtendedMembershipUserCollection(), "UserName", "FullName");
        }
    }
}
