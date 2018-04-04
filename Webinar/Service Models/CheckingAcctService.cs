using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Webinar.Models;

namespace Webinar.Service_Models
{
    public class CheckingAcctService
    {
        private ApplicationDbContext db { get; set; }

        public CheckingAcctService(ApplicationDbContext Db)
        {
            db = Db;
        }

        public void NewCheckingAcct(string firstname, string lastname, decimal balance, string id)
        {
            var acctno = ((123456 + db.CheckingAccts.Count()).ToString().PadRight(10, '0'));
            var checkingacct = new CheckingAcct
            {
                FirstName = firstname,
                LastName = lastname,
                Balance = balance,
                AccountNo = acctno,
                ApplicationUserId = id
            };

            db.CheckingAccts.Add(checkingacct);
            db.SaveChanges();
        }
    }
}