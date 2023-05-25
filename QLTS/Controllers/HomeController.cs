using QLTS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QLTS.Controllers
{
    [RequireHttps]
    public class HomeController : Controller
    {
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Dashboard(string item)
        {
            ViewBag.DashboardName = item != "" && item != null ? item : "dashboard1";
            return View();
        }

        public ActionResult Map()
        {
            return View();
        }

        public ActionResult MapF1()
        {
            return View();
        }
    }
}