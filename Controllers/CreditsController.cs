using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CheckBook.Data;
using CheckBook.Models;
using Microsoft.Extensions.Options;

namespace CheckBook.Controllers
{
    public class CreditsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CreditsController(ApplicationDbContext context)
        {
            _context = context;
            
        }

        // GET: Credits
        public async Task<IActionResult> Index(string searchString)
        {
            var credits = from c in _context.Credit
                          select c;

            if (!String.IsNullOrEmpty(searchString))
            {
                credits = credits.Where(s => s.description.Contains(searchString));
            }

            //return View(await _context.Credit.ToListAsync());
            return View(await credits.ToListAsync());
        }

        // GET: Credits/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var credit = await _context.Credit.SingleOrDefaultAsync(m => m.ID == id);
            if (credit == null)
            {
                return NotFound();
            }

            return View(credit);
        }

        // GET: Credits/Create
        public IActionResult Create()
        {
            var accounts = from c in _context.AccountType
                           select c;
            ViewBag.accountsData = accounts;

            var now = DateTime.Now.ToString("MM-dd-yyyy") ;
            ViewBag.Date = now;

            return View();
        }
        

        // POST: Credits/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,amount,date,description,paymentForm")] Credit credit)
        {


            if (ModelState.IsValid)
            {
                _context.Add(credit);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            var date = DateTime.Now.ToString("d");
            //ViewBag.date = date;

            return View(credit);
        }

        // GET: Credits/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var credit = await _context.Credit.SingleOrDefaultAsync(m => m.ID == id);
            if (credit == null)
            {
                return NotFound();
            }
            return View(credit);
        }

        // POST: Credits/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,amount,date,description,paymentForm")] Credit credit)
        {
            if (id != credit.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(credit);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CreditExists(credit.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            return View(credit);
        }

        // GET: Credits/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var credit = await _context.Credit.SingleOrDefaultAsync(m => m.ID == id);
            if (credit == null)
            {
                return NotFound();
            }

            return View(credit);
        }

        // POST: Credits/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var credit = await _context.Credit.SingleOrDefaultAsync(m => m.ID == id);
            _context.Credit.Remove(credit);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool CreditExists(int id)
        {
            return _context.Credit.Any(e => e.ID == id);
        }

        public async Task<IActionResult> Accounts(string searchString)
        {
            //string searchString = "Checking";
            var credits = from c in _context.Credit
                          select c;

            if(searchString != null)
            {
                ViewBag.Name = searchString;
                ViewBag.String = "Choose another option to limit to a different account";
            } else
            {
                ViewBag.Name = "Accounts";
                ViewBag.String = "Please select an option to limit down to an account";
            }

            var accounts = from c in _context.AccountType
                           select c;
            ViewBag.accountsData = accounts;

            if (!String.IsNullOrEmpty(searchString))
            {
                credits = credits.Where(s => s.paymentForm.Contains(searchString));
            }

            credits.OrderByDescending(s => s.date);

            var account = (from t in _context.Credit
                           join sc in _context.Debit
                            on t.paymentForm equals sc.accountType
                           select new { t.description, t.amount, t.date });
            account.OrderByDescending(s => s.date);
            var accountInfo = new List<Credit>();
            foreach (var i in account)
            {
                accountInfo.Add(new Credit()
                {
                    description = i.description,
                    amount = i.amount,
                    date = i.date
                });
            }

            //return View(await _context.Credit.ToListAsync());
            return View(accountInfo); //await credits.ToListAsync()
        }

        public async Task<IActionResult> Savings(string searchString)
        {
            //string searchString = "Savings";
            var credits = from c in _context.Credit
                          select c;
            var date = DateTime.Now.ToString("d");
            ViewBag.date = date;

            if (!String.IsNullOrEmpty(searchString))
            {
                credits = credits.Where(s => s.paymentForm.Contains(searchString));
            }

            credits.OrderByDescending(s => s.date);

            //return View(await _context.Credit.ToListAsync());
            return View(await credits.ToListAsync());
        }

        //protected void Button1_Click(object sender, EventArgs e)
        //{
        //    MyTextBox.Text = "Text";
        //}
    }
}
