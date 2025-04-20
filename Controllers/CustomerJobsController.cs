using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CustomerJobApp.Data;
using CustomerJobApp.Models;

namespace CustomerJobWeb.Controllers
{
    public class CustomerJobsController : Controller
    {
        private readonly AppDbContext _context;

        public CustomerJobsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: CustomerJobs
        public async Task<IActionResult> Index()
        {
            return View(await _context.CustomerJobs.ToListAsync());
        }

        // GET: CustomerJobs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customerJob = await _context.CustomerJobs
                .FirstOrDefaultAsync(m => m.CustomerJobId == id);
            if (customerJob == null)
            {
                return NotFound();
            }

            return View(customerJob);
        }

        // GET: CustomerJobs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CustomerJobs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CustomerJobId,Name")] CustomerJob customerJob)
        {
            if (ModelState.IsValid)
            {
                _context.Add(customerJob);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(customerJob);
        }

        // GET: CustomerJobs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customerJob = await _context.CustomerJobs.FindAsync(id);
            if (customerJob == null)
            {
                return NotFound();
            }
            return View(customerJob);
        }

        // POST: CustomerJobs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CustomerJobId,Name")] CustomerJob customerJob)
        {
            if (id != customerJob.CustomerJobId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(customerJob);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CustomerJobExists(customerJob.CustomerJobId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(customerJob);
        }

        // GET: CustomerJobs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customerJob = await _context.CustomerJobs
                .FirstOrDefaultAsync(m => m.CustomerJobId == id);
            if (customerJob == null)
            {
                return NotFound();
            }

            return View(customerJob);
        }

        // POST: CustomerJobs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var customerJob = await _context.CustomerJobs.FindAsync(id);
            if (customerJob != null)
            {
                _context.CustomerJobs.Remove(customerJob);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CustomerJobExists(int id)
        {
            return _context.CustomerJobs.Any(e => e.CustomerJobId == id);
        }
    }
}
