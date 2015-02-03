using System;
using System.Linq;
using System.Web.Mvc;
using HBS.Data.Entities.TimeTracking.Infrastructure;
using HBS.Data.Entities.TimeTracking.Models;

namespace HBS.TimeTracking.Controllers
{
    public class ReportController : Controller
    {
        //
        // GET: /Report/

        public ActionResult Get()
        {
            return View(new UserList(string.Empty));
            //return View();
        }
        [HttpPost]
        public PartialViewResult GetUserHistory(FormCollection collection)
        {
            var userList = MembershipUserExtended.GetFullNameUserNameList();
            string userName=collection["SelectedValue"];
            if (userList.ContainsValue(userName))
            {
                DateTime startDate;
                DateTime endDate;
                if (DateTime.TryParse(collection["txtStartDate"], out startDate) && DateTime.TryParse(collection["txtEndDate"], out endDate))
                {
                    var model = TimeTrackManager.GetUserTimeTrackHistoryForSpecifiedPeriod(userName, startDate, endDate);
                    return PartialView("_GetUserHistory", model);
                }                
            }

            return PartialView("_GetUserHistory", new CustomTimeTrack());
        }
        [HttpPost]
        public void ExportUserHistory(FormCollection collection)
        {
            var userList = MembershipUserExtended.GetFullNameUserNameList();
            string userName = collection["uname"];
            if (userList.ContainsValue(userName))
            {
                DateTime startDate;
                DateTime endDate;
                if (DateTime.TryParse(collection["startDate"], out startDate) && DateTime.TryParse(collection["endDate"], out endDate))
                {
                    var model = TimeTrackManager.GetUserTimeTrackHistoryForSpecifiedPeriod(userName, startDate, endDate);
                    //return PartialView("_GetUserHistory", model);
                    IExportPage export = new ExportPage();
                    var reportName = model.EmployeeName.Replace(" ", "_") + "_" + model.CustomStartEndDateDisplay.Replace(" ", "-");
                    export.ExportExcel(ExcelReportHelper.GetExcelString(model,reportName), reportName + ".xls");
                }
            }

            //return PartialView("_GetUserHistory", new CustomTimeTrack());
        }

        public ActionResult UserQuickSearch(string term)
        {
            var users = MembershipUserExtended.GetExtendedMembershipUserCollection(term).Select(c=> new {label=c.FullName});
            return Json(users, JsonRequestBehavior.AllowGet);
        }
    }
}
