using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Webinar.Models;

[assembly: log4net.Config.XmlConfigurator(Watch = true)]

namespace Webinar.Controllers
{
    [Authorize]
    public class TransactionController : Controller
    {
       
        private ApplicationDbContext db = new ApplicationDbContext();
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);



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

                    checking.Balance = db.Transactions.Where(c => c.CheckingAcctId == transaction.CheckingAcctId)
                    .Sum(c => c.Amount);
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
            var trueuserid = User.Identity.GetUserId();
            var trueuser = db.CheckingAccts.Where(c => c.ApplicationUserId == trueuserid).First();
            var trueusername = trueuser.Name;

            if (checkingAcctId == trueuser.Id)
            {
                var balance = db.CheckingAccts.Where(c => c.Id == checkingAcctId).First().Balance;
                var infolog = trueusername + " checked their balance";
                log.Info(infolog);
                ViewBag.Balance = balance;

                return View();
            }

            return RedirectToAction("Dashboard", "Home");
        }

        public ActionResult TransferFunds(int checkingAcctId)
        {


            return View();
        }


        [HttpPost]
        public ActionResult TransferFunds(Transaction transaction, string acctno)
        {
            var sender = db.CheckingAccts.Where(c => c.Id == transaction.CheckingAcctId).First();

            if (sender.Balance < transaction.Amount)
            {
                ModelState.AddModelError("Amount", "Your Account is too low for this transaction");
                 return PartialView("_TransferFunds", transaction);
                
            }


          

            if(!db.CheckingAccts.Any(c => c.AccountNo == acctno))
            {
                ModelState.AddModelError("", "Invalid Destination Account #");
                return PartialView("_TransferFunds", transaction);
             
            }


            
            
                var recepient = db.CheckingAccts.Where(c => c.AccountNo == acctno).First();
                //recepient.Transactions.Add(transaction);
                //db.SaveChanges();
                //recepient.Balance = recepient.Transactions.Sum(c => c.Amount);
                //db.SaveChanges();

                //transaction.Amount = -transaction.Amount;
                //sender.Transactions.Add(transaction);
                //db.SaveChanges();
                //sender.Balance = sender.Transactions.Sum(c => c.Amount);
                //db.SaveChanges();

                var reci = new Transaction
                {
                    Id = recepient.Id,
                    Amount = transaction.Amount,
                    CheckingAcctId = recepient.Id
                };

                var send = new Transaction
                {
                    Id = sender.Id,
                    Amount = -transaction.Amount,
                    CheckingAcctId = sender.Id
                };

                db.Transactions.Add(reci);
                db.Transactions.Add(send);
                db.SaveChanges();



            sender.Balance = db.Transactions.Where(c => c.CheckingAcctId == transaction.CheckingAcctId).Sum(c=>c.Amount);
                db.SaveChanges();

            var nullid = (int?)recepient.Id;
                recepient.Balance = db.Transactions.Where(c=> c.CheckingAcctId== nullid).Sum(c => c.Amount);
                db.SaveChanges();

                //Withdrawal(transaction);




                //sender.Balance -= transaction.Amount;
                //recepient.Balance += transaction.Amount;

                var uzer = User.Identity.GetUserId();

                ViewBag.Recepient = recepient.AccountNo;
                ViewBag.Balance = sender.Balance;
                ViewBag.CheckingAcctId = db.CheckingAccts.Where(c => c.ApplicationUserId == uzer).First().Id;

                return PartialView("_TransferSuccessful");

            




         
        }


    }
}