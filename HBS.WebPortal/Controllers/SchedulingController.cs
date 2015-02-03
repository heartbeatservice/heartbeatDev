using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HBS.WebPortal.Controllers
{
    public class SchedulingController : Controller
    {
        public ActionResult Index()
        {

            if (Session["user"] == null)
            {

                return RedirectToAction("Index", "Home",new { id = "You cannot access page without Logging In" });
            }
            return View();
        }

        public ActionResult Customer()
        {
            if (Session["user"] == null)
            {

                return RedirectToAction("Index", "Home", new { id = "You cannot access page without Logging In" });
            }
            ViewBag.companyid = getCompanyId();
            ViewBag.userid = getUserId();
            return View();
        }

        public ActionResult Daily()
        {
            if (Session["user"] == null)
            {
                
               


                return RedirectToAction("Index", "Home", new { id = "You cannot access page without Logging In" });
            }

            if (Request.QueryString["CustomerId"] != null)
                ViewBag.CustomerId = Request.QueryString["CustomerId"].ToString();
            else
                ViewBag.CustomerId = "0";
            if (Request.QueryString["ProfessionalId"] != null)
                ViewBag.ProfessionalId = Request.QueryString["ProfessionalId"].ToString();
            else
            ViewBag.ProfessionalId = "0";
            ViewBag.CurrentDate = DateTime.Now.ToShortDateString();
            ViewBag.companyid = getCompanyId();
            return View();
        }


        public ActionResult Weekly()
        {
            if (Session["user"] == null)
            {

                return RedirectToAction("Index", "Home", new { id = "You cannot access page without Logging In" });
            }
            return View();
        }

        int getCompanyId()
        {
            int companyid=0;
            if (Session["user"] != null)
                companyid = ((HBS.WebPortal.Models.User)Session["user"]).companyid;
            return companyid;
        }

        int getUserId()
        {
            int userid = 0;
            if(Session["user"]!=null)
                userid = ((HBS.WebPortal.Models.User)Session["user"]).userid;
            return userid;
        }

        public ActionResult Professional()
        {
            if (Session["user"] == null)
            {

                return RedirectToAction("Index", "Home", new { id = "You cannot access page without Logging In" });
            }
            ViewBag.companyid = getCompanyId();
            ViewBag.userid = getUserId();
                       
            return View();
        }


        public ActionResult Insurance()
        {
            if (Session["user"] == null)
            {

                return RedirectToAction("Index", "Home", new { id = "You cannot access page without Logging In" });
            }

            ViewBag.companyid = getCompanyId();
            ViewBag.userid = getUserId();

            return View();
        }

        public ActionResult Contact()
        {
            if (Session["user"] == null)
            {

                return RedirectToAction("Index", "Home", new { id = "You cannot access page without Logging In" });
            }

            ViewBag.companyid = getCompanyId();
            ViewBag.userid = getUserId();

            return View();
        }

        public ActionResult Project()
        {
            if (Session["user"] == null)
            {

                return RedirectToAction("Index", "Home", new { id = "You cannot access page without Logging In" });
            }

            ViewBag.companyid = getCompanyId();
            ViewBag.userid = getUserId();

            return View();
        }
    }
}
