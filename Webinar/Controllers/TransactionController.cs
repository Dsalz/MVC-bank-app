using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Webinar.Models;

namespace Webinar.Controllers
{
    [Authorize]
    public class TransactionController : Controller
    {
       
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Transaction
        public ActionResult Deposit(int checkingAcctId)
        {
            
            return View();
        }

        [HttpPost]
        public ActionResult Deposit(Transaction transaction)
        {
            if (ModelState.IsValid)
            {
                db.Transactions.Add(transaction);
                db.SaveChanges();
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

    } 
}