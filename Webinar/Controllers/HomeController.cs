using System;
using System.Collections.Generic;
using System.EnterpriseServices;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Webinar.Models;

namespace Webinar.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

       public ActionResult Index()
       {
           if (Request.IsAuthenticated)
           {
               var userid = User.Identity.GetUserId();
               var check = db.CheckingAccts.Where(c => c.ApplicationUserId == userid).First().Id;
               ViewBag.CheckingId = check;

           }

            return View();
        }

        

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}