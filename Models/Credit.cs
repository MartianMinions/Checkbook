using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace CheckBook.Models
{
    public class Credit
    {
        public int ID { get; set; }

        [Display(Name = "Description")]
        [StringLength(60, MinimumLength = 3)]
        public string description { get; set; }

        [Display(Name = "Amount")]
        [DataType(DataType.Currency)]
        public decimal amount { get; set; }

        [Display(Name = "Date")]
        [DataType(DataType.Date)]
        public DateTime date { get; set; }

        [Display(Name = "Account")]
        [StringLength(25, MinimumLength = 2)]
        public string paymentForm { get; set; }
    }
}
