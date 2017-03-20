using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CheckBook.Data;
using Microsoft.EntityFrameworkCore;
using CheckBook.Models;

namespace CheckBook.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public decimal debitTotal;
        public decimal creditTotal;
        public decimal TotalAmount;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            List<decimal> ListOfDebitAmounts = new List<decimal>();
            List<decimal> ListOfCreditAmounts = new List<decimal>();
            List<string> AccountNames = new List<string>();

            var accounts = from d in _context.AccountType
                           select d;
            

            string accountName;
            foreach (AccountType c in accounts)
            {
                var debit = from d in _context.Debit
                             select d;
                var credit = from cred in _context.Credit
                              select cred;
                accountName = (string) c.accountName;
                debit = debit.Where(s => s.accountType.Contains(accountName));
                ListOfDebitAmounts.Add(debit.Sum(s => s.price));

                credit = credit.Where(s => s.paymentForm.Contains(accountName));
                ListOfCreditAmounts.Add(credit.Sum(s => s.amount));
                AccountNames.Add(accountName);
            }

            ViewBag.DebitTotals = ListOfDebitAmounts;
            ViewBag.CreditTotals = ListOfCreditAmounts;
            ViewBag.AccountNames = AccountNames;

            var debits = from d in _context.Debit
                         select d;
            var credits = from c in _context.Credit
                          select c;

            // Total on the top of the screen
            debitTotal = debits.Sum(s => s.price);
            creditTotal = credits.Sum(s => s.amount);
            TotalAmount = creditTotal - debitTotal;

            ViewBag.CreditTotal = creditTotal;
            ViewBag.DebitTotal = debitTotal;
            ViewBag.TotalAmount = TotalAmount;

            var tuple = new Tuple<Debit, Credit>(new Debit(), new Credit());

            return View(tuple);
        }

        public async Task<IActionResult> About()
        {
            var creditChecking = from d in _context.Credit
                         select d;


            creditChecking = creditChecking.Where(s => s.paymentForm.Contains("Checking"));

            var creditSaving = from d in _context.Credit
                                 select d;


            creditSaving = creditSaving.Where(s => s.paymentForm.Contains("Savings"));

            return View(await creditChecking.ToListAsync(), await creditSaving.ToListAsync());
        }

        private IActionResult View(List<Credit> list1, List<Credit> list2)
        {
            throw new NotImplementedException();
        }

        public IActionResult Contact()
        {

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
