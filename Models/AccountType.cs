using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CheckBook.Models
{
    public class AccountType
    {
        public int ID { get; set; }

        [Display(Name = "Account Name")]
        [StringLength(25, MinimumLength = 2)]
        public string accountName { get; set; }

        [Display(Name = "Description")]
        [StringLength(25, MinimumLength = 2)]
        public string description { get; set; }
    }
}
