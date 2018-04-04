using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Webinar.Models
{
    public class CheckingAcct
    {

        public int Id { get; set; }

        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [DataType(DataType.Currency)]
        public double Balance { get; set; }

        [Display (Name = "Account #")]
        [StringLength(10)]
        [Column (TypeName = "varchar")]
        [Required]
        public string AccountNo { get; set; }

        public string Name
        {
            get { return String.Format(FirstName + " " + LastName); }
        }

      
        [Required]
        public virtual ApplicationUser User { get; set; }
        
        
        public string ApplicationUserId { get; set; }

       public virtual ICollection<Transaction> Transactions { get; set; }
        
    }
}