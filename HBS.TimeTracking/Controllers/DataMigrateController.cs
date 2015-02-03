using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;

namespace HeartbeatService.TimeTrackingSystem.Controllers
{
    public class DataMigrateController : NoCacheRavenController
    {
        //
        // GET: /DataMigrate/
        private List<MedicalUser> _legacyUsers;

        public List<MedicalUser> LegacyUsers
        {
            get
            {
                return _legacyUsers ??
                       (_legacyUsers = RavenSession.Query<MedicalUser>().OrderBy(c => c.Firstname).ToList());
            }
            set { _legacyUsers = value; }
        }

        private List<MembershipUser> _membershipUsers;

        public List<MembershipUser> MembershipUsers
        {
            get
            {
                return _membershipUsers
                       ??
                       (_membershipUsers = GetAllMemberShipUsers());
            }
            set { _membershipUsers = value; }
        }

        protected List<MembershipUser> GetAllMemberShipUsers()
        {
            return Membership.GetAllUsers().Cast<MembershipUser>().ToList();
        }

        public ActionResult Users()
        {
            //ViewBag.Message = !string.IsNullOrEmpty(Request["message"]) ? Request["message"] : string.Empty;
            return View(GetLegacyUsers());
        }

        protected List<MedicalUser> GetLegacyUsers()
        {
            foreach (MedicalUser user in LegacyUsers)
            {
                var userHistoryList =
                    RavenSession.Query<UserTimeStampsHistory>().Where(c => c.UserName == user.User).ToList();
                var efUHistoryList = UserTimeTrackHistory.GetUserTimeStampHistory(user.User);
                var dataNeedsToMigrate = false;
                if (userHistoryList.Count > 0)
                {
                    if (efUHistoryList.Count > 0)
                    {
                        foreach (var hist in userHistoryList.SelectMany(c => c.History))
                        {
                            var efHist =
                                efUHistoryList.FirstOrDefault(
                                    c => c.ClockInTime.Equals(hist.Start_time) && c.ClockOutTime.Equals(hist.End_time));

                            dataNeedsToMigrate = efHist == null;
                            if (dataNeedsToMigrate)
                                break;
                        }
                        user.UserHasDataToMigrate = dataNeedsToMigrate;
                    }
                    else
                        user.UserHasDataToMigrate = true;
                }
                user.UserHistoryList = userHistoryList;
                //Check If User has already been migrated
                user.IsUserMigrated = MembershipUsers.Any(c => c.UserName.ToLower().Equals(user.User.ToLower()));
            }
            return LegacyUsers;
        }

        public PartialViewResult MigrateUser(string id)
        {
            var legacyUser = LegacyUsers.FirstOrDefault(c => c.User == id);

            if (legacyUser != null && MembershipUsers.All(c => c.UserName != legacyUser.User))
            {
                try
                {
                    double hourlyRate;
                    MembershipCreateStatus createStatus;
                    var membershipUser = MembershipUserExtended.CreateUser(legacyUser.User, legacyUser.Pwd,
                                                                           legacyUser.User + "@mmc.com",
                                                                           legacyUser.Firstname, legacyUser.Lastname,
                                                                           string.Empty,
                                                                           !double.TryParse(legacyUser.Hourly_rate,
                                                                                            out hourlyRate)
                                                                               ? 0.0
                                                                               : hourlyRate,
                                                                           legacyUser.Address, string.Empty,
                                                                           string.Empty,
                                                                           string.Empty, legacyUser.Phonenumber,
                                                                           out createStatus);
                    if (createStatus == MembershipCreateStatus.Success)
                    {
                        _membershipUsers = GetAllMemberShipUsers();
                        if (!string.IsNullOrEmpty(legacyUser.Role) && Roles.GetAllRoles().Contains(legacyUser.Role))
                            Roles.AddUserToRole(membershipUser.UserName, legacyUser.Role);
                        else
                            Roles.AddUserToRole(membershipUser.UserName,
                                                Roles.GetAllRoles().FirstOrDefault(c => c.ToLower().Equals("user")));

                        MigrateUserData(membershipUser.UserName);

                        ViewBag.Message = string.Format("User {0} migrated successfully.", id);
                        return PartialView(viewName: "_LegacyUserList", model: GetLegacyUsers());
                    }
                }
                catch (Exception ex)
                {
                    ViewBag.Message = string.Format("{0} error occured while migrating user {1}.", ex.Message.ToString(),
                                                    id);
                    return PartialView("_LegacyUserList", GetLegacyUsers());
                }
            }

            ViewBag.Message = string.Format("Error occured while migrating user {0}.", id);
            return PartialView("_LegacyUserList", GetLegacyUsers());
        }

        protected bool MigrateUserData(string userName)
        {
            var userNameIdDic = MembershipUserExtended.GetUserIdUserNameList();

            try
            {
                var ravenUserHistory =
                    RavenSession.Query<UserTimeStampsHistory>().Where(c => c.UserName == userName).ToList();
                var efUHistoryList = UserTimeTrackHistory.GetUserTimeStampHistory(userName);
                if (ravenUserHistory.Count > 0)
                {
                    if (efUHistoryList.Count > 0)
                    {
                        foreach (var efUserTimeStampHistory
                            in from history in ravenUserHistory
                               from timeStamp in history.History
                               where !efUHistoryList.Any(
                                   c =>
                                   c.ClockInTime.Equals(timeStamp.Start_time) &&
                                   c.ClockOutTime.Equals(timeStamp.End_time))
                               select new UserTimeTrackHistory()
                                   {
                                       UserId = userNameIdDic[userName],
                                       UserName = userName,
                                       ClockInTime = DateTime.Parse(timeStamp.Start_time).TimeOfDay.ToString(),
                                       ClockOutTime = DateTime.Parse(timeStamp.End_time).TimeOfDay.ToString(),
                                       StampDate = DateTime.Parse(timeStamp.Stampdate),
                                       CreatedBy = HttpContext.User.Identity.Name,
                                       CreatedDate = DateTime.Now,
                                       UserIP = WebHelpers.GetIpAddress() + "~" + Request.UserHostName
                                   })
                        {
                            efUserTimeStampHistory.Save();
                        }
                    }
                    else
                    {
                        foreach (var efUserTimeStampHistory
                            in from history in ravenUserHistory
                               from timeStamp in history.History
                               select new UserTimeTrackHistory()
                                   {
                                       UserId = userNameIdDic[userName],
                                       UserName = userName,
                                       ClockInTime = DateTime.Parse(timeStamp.Start_time).TimeOfDay.ToString(),
                                       ClockOutTime = DateTime.Parse(timeStamp.End_time).TimeOfDay.ToString(),
                                       StampDate = DateTime.Parse(timeStamp.Stampdate),
                                       CreatedBy = HttpContext.User.Identity.Name,
                                       CreatedDate = DateTime.Now,
                                       UserIP =
                                           WebHelpers.GetIpAddress() + "~" + Request.UserHostName
                                   })
                        {
                            efUserTimeStampHistory.Save();
                        }
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public PartialViewResult MigrateData(string id)
        {
            if (MigrateUserData(id))
            {
                ViewBag.Message = string.Format("User {0}'s data migrated successfully.", id);
                return PartialView(viewName: "_LegacyUserList", model: GetLegacyUsers());
            }
            ViewBag.Message = string.Format("Error occured while migrating user {0}'s data.", id);
            return PartialView(viewName: "_LegacyUserList", model: GetLegacyUsers());
        }
    }
}
