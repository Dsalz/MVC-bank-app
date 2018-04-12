using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Webinar.Models;

namespace Webinar.Controllers
{
    public class CheckingAcctController : Controller
    {
        // GET: CheckingAcct
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Statement(int Id)
        {
            var statementoa = db.CheckingAccts.Find(Id);


            ViewBag.Balance = statementoa.Balance;

            return View(statementoa.Transactions.ToList());
        }
    }
}