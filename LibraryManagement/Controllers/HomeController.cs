using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LibraryManagement.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Official site of Andheri Library";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Official site of Andheri Library";

            return View();
        }
    }
}