﻿using System;
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

                var checking = db.CheckingAccts.Where(c=> c.Id == transaction.CheckingAcctId).First();
                checking.Balance = db.Transactions.Where(c => c.CheckingAcctId == transaction.CheckingAcctId)
                    .Sum(c => c.Amount);
                db.SaveChanges();
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        public ActionResult Withdrawal(int checkingAcctId)
        {

            return View();
        }



        [HttpPost]
        public ActionResult Withdrawal(Transaction transaction)
        {
            if (ModelState.IsValid)
            {
                var checking = db.CheckingAccts.Where(c => c.Id == transaction.CheckingAcctId).First();
                
                if (transaction.Amount <= checking.Balance)
                {

                    var amtwithdrawed = transaction.Amount * -1;
                    transaction.Amount = amtwithdrawed;
                    db.Transactions.Add(transaction);
                    db.SaveChanges();

                    var totaltransactions = db.Transactions.Where(c => c.CheckingAcctId == transaction.CheckingAcctId)
                        .Sum(c => c.Amount);

                    checking.Balance += totaltransactions;
                    db.SaveChanges();
;
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.LowBalance = "Your account is too low for this transaction";
                    return View();
                }
            }

            return View();
        }


        public ActionResult Balance(int checkingAcctId)
        {
            var balance = db.CheckingAccts.Where(c => c.Id == checkingAcctId).First().Balance;
            ViewBag.Balance = balance;

            return View();
        }

      

    } 
}