using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace CheckBook.Models
{
    public class Debit
    {
        public int ID { get; set; }

        [Display(Name = "Description")]
        [StringLength(60, MinimumLength = 3)]
        public string description { get; set; }

        [Display(Name = "Price")]
        [DataType(DataType.Currency)]
        public decimal price { get; set; }

        [Display(Name = "Date")]
        [DataType(DataType.Date)]
        public DateTime date { get; set; }

        [Display(Name = "Payment Method")]
        [StringLength(25, MinimumLength = 2)]
        public string paymentMethod { get; set; }

        [Display(Name = "Account Type")]
        [StringLength(25, MinimumLength = 2)]
        public string accountType { get; set; }
        
    }
}
