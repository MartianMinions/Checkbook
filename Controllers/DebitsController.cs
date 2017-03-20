using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CheckBook.Data;
using CheckBook.Models;

namespace CheckBook.Controllers
{
    public class DebitsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private DateTime ReturnDate;

        public DebitsController(ApplicationDbContext context)
        {
            _context = context;
            this.ReturnDate = DateTime.Now;
            ViewBag.ReturnDate = ReturnDate;
        }

        // GET: Debits
        public async Task<IActionResult> Index(string searchDebits)
        {
            var debits = from d in _context.Debit
                          select d;

            if (!String.IsNullOrEmpty(searchDebits))
            {
                debits = debits.Where(s => s.description.Contains(searchDebits));
            }
            
            //return View(await _context.Debit.ToListAsync());
            return View(await debits.ToListAsync());
        }

        // GET: Debits/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var debit = await _context.Debit.SingleOrDefaultAsync(m => m.ID == id);
            if (debit == null)
            {
                return NotFound();
            }

            return View(debit);
        }

        // GET: Debits/Create
        public IActionResult Create()
        {
            var accounts = from c in _context.AccountType
                           select c;
            ViewBag.accountsData = accounts;

            var now = DateTime.Now.ToString("MM-dd-yyyy");
            ViewBag.Date = now;

            return View();
        }

        // POST: Debits/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,date,description,paymentMethod,price,accountType")] Debit debit)
        {
            if (ModelState.IsValid)
            {
                _context.Add(debit);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(debit);
        }

        // GET: Debits/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var debit = await _context.Debit.SingleOrDefaultAsync(m => m.ID == id);
            if (debit == null)
            {
                return NotFound();
            }
            return View(debit);
        }

        // POST: Debits/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,date,description,paymentMethod,price")] Debit debit)
        {
            if (id != debit.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(debit);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DebitExists(debit.ID))
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
            return View(debit);
        }

        // GET: Debits/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var debit = await _context.Debit.SingleOrDefaultAsync(m => m.ID == id);
            if (debit == null)
            {
                return NotFound();
            }

            return View(debit);
        }

        // POST: Debits/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var debit = await _context.Debit.SingleOrDefaultAsync(m => m.ID == id);
            _context.Debit.Remove(debit);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool DebitExists(int id)
        {
            return _context.Debit.Any(e => e.ID == id);
        }
        
    }
}
