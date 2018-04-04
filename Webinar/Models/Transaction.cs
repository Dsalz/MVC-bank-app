using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Webinar.Models
{
    public class Transaction
    { 

        public int Id { get; set; }
        public double Amount { get; set; }

        [Required]
        public int CheckingAcctId { get; set; }
        public virtual CheckingAcct CheckingAcct { get; set; }
    }
}