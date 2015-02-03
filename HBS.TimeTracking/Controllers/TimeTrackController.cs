using System;
using System.Web.Configuration;
using System.Web.Mvc;
using HBS.Data.Entities.TimeTracking.Infrastructure;
using HBS.Data.Entities.TimeTracking.Models;
using HBS.Data.Entities.TimeTracking.ViewModels;

namespace HBS.TimeTracking.Controllers
{
    [Authorize]
    public class TimeTrackController : NoCacheController
    {
        public string LoggedInUserName
        {
            get { return HttpContext.User.Identity.Name; }
        }
        //
        // GET: /TimeTrack/
        [Authorize]
        public ActionResult Index()
        {
            return RedirectToAction("Daily");
        }
        [Authorize]
        public ActionResult Daily()
        {
            return View(TimeTrackManager.GetCurrentDayClockInOutTime(LoggedInUserName));
        }
        [Authorize]
        public PartialViewResult DailyPartial()
        {
            return PartialView("_DailyTimeTrack", TimeTrackManager.GetCurrentDayClockInOutTime(LoggedInUserName));
        }


        [Authorize]
        public ActionResult Weekly(string id)
        {
            return PartialView("_WeeklyTimeTrack", TimeTrackManager.GetCurrentWeekClockInOutTime(id ?? LoggedInUserName));
        }

        [Authorize]
        public ActionResult WeeklyByDate(string id,string startDate, FormCollection collection)
        {
            DateTime weekStartDate;
            if (DateTime.TryParse(collection["SelectedValue"], out weekStartDate))
            {
                return PartialView("_WeeklyTimeTrack",
                                   TimeTrackManager.GetClockInOutTime(id ?? LoggedInUserName, weekStartDate,
                                                                      weekStartDate.AddDays(6)));
            }
            return PartialView("_WeeklyTimeTrack", new WeeklyTimeTrackWeekListViewModel());
        }

        [Authorize]
        public PartialViewResult Track()
        {
            TimeTrackManager.TrackClockInOutTime(LoggedInUserName);
            return PartialView("_DailyTimeTrack", TimeTrackManager.GetCurrentDayClockInOutTime(LoggedInUserName));
        }

        [Authorize]
        public ActionResult UpdateClockInOutTime(int timeTrackId,string stampDate,string selectedUser, string clockInTime, string clockOutTime)
        {
            var userTimeTrackRecord = TimeTrackManager.UpdateClockInOutTime(timeTrackId, stampDate, selectedUser, clockInTime, clockOutTime, LoggedInUserName);
            return Json(userTimeTrackRecord);
        }

        [Authorize]
        public ActionResult Create()
        {
            ViewBag.Message =string.Empty;
            return View(new UserTimeTrackHistoryMapped());
        }

        [HttpPost]
        [Authorize]
        public ActionResult Create(UserTimeTrackHistoryMapped utth,FormCollection collection)
        {
           
            var userList = MembershipUserExtended.GetFullNameUserNameList();
            string userName=collection["UserName"];
            if (userList.ContainsValue(userName))
            {
                utth.UserName = userName;

                DateTime clockInDt;
                DateTime clockOutDt;
                if (DateTime.TryParse(utth.ClockInTime, out clockInDt) && DateTime.TryParse(utth.ClockOutTime, out clockOutDt))
                {
                    if (clockOutDt.TimeOfDay.CompareTo(clockInDt.TimeOfDay) != -1) // if clock out time is earlier than clock in time than error
                    {
                        utth.ClockInTime = string.Format("{0:t}", clockInDt);
                        utth.ClockOutTime = string.Format("{0:t}", clockOutDt);
                    
                        utth.UserId = MembershipUserExtended.GetUserIdByUserName(userName);
                        utth.CreatedBy = LoggedInUserName;
                        utth.CreatedDate = WebHelpers.GetCurrentDateTimeByTimeZoneId(WebConfigurationManager.AppSettings["UserTimeZoneId"]);
                        utth.IsDeleted = false;
                        var tth = utth.Get(utth.Save());
                        tth.UserName = userList.FindKeyByValue(userName);
                        ViewBag.Message = "Record inserted successfully.";
                        return View(tth);
                    }
                    ViewBag.Message = "Clock Out time can not be earlier than Clock In time.";
                    return View(utth);
                }
                ViewBag.Message = "Not a valid Clock In/Out time, please make sure time is in correct format.";
                return View(utth);
            }
            ViewBag.Message = "Error inserting record.";
            return View(new UserTimeTrackHistoryMapped());
        }

        public ActionResult Edit()
        {
            //return View();
            return View(new UserList(string.Empty));
        }
        
        [HttpPost]
        public PartialViewResult GetTimeTrackForEdit(FormCollection collection)
        {
            var userList = MembershipUserExtended.GetFullNameUserNameList();
            string userName = collection["SelectedValue"];
            if (userList.ContainsValue(userName))
            {
                DateTime startDate;
                if (DateTime.TryParse(collection["txtStartDate"], out startDate))
                {
                    var endDate = startDate;
                    var model = TimeTrackManager.GetDailyClockInOutTimeByDate(userName, startDate, endDate);
                    
                    ViewBag.UserName = userName;
                    //ViewBag.UserFullName = collection["user"];
                    return PartialView("_TimeTrackForEdit", new DailyTimeTrackViewModel(model, userList.FindKeyByValue(userName),userName));
                }
            }

            return PartialView("_TimeTrackForEdit", new DailyTimeTrackViewModel());
        }
        [HttpPost]
        public JsonResult DeleteTimeTrackRecord(int timeTrackId)
        {
            var userTimeTrackRecord = TimeTrackManager.DeleteTimeTrackRecord(timeTrackId, LoggedInUserName);
            return Json(userTimeTrackRecord,JsonRequestBehavior.AllowGet);
        }
    }
}
