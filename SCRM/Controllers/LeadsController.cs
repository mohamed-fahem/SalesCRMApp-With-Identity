using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SCRM.Data;
using SCRM.Models;

namespace SCRM.Controllers
{
    
    public class LeadsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LeadsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Leads
        [Authorize(Roles = "Admin,Sales")]
        public async Task<IActionResult> Index()
        {
              return _context.salesLead != null ? 
                          View(await _context.salesLead.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.salesLead'  is null.");
        }

        // GET: Leads/Details/5
        [Authorize(Roles = "Admin,Sales")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.salesLead == null)
            {
                return NotFound();
            }

            var salesLeadEntity = await _context.salesLead
                .FirstOrDefaultAsync(m => m.Id == id);
            if (salesLeadEntity == null)
            {
                return NotFound();
            }

            return View(salesLeadEntity);
        }



        // GET: Leads/Create
        [Authorize(Roles = "Admin,Sales")]
        public IActionResult Create()
        {
            return View();
        }

        [Authorize(Roles = "Admin,Sales")]
        // POST: Leads/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FirstName,LastName,Mobile,Email,Source")] SalesLeadEntity salesLeadEntity)
        {
            if (ModelState.IsValid)
            {
                _context.Add(salesLeadEntity);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(salesLeadEntity);
        }




        // GET: Leads/Edit/5
        [Authorize(Roles = "Admin,Sales")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.salesLead == null)
            {
                return NotFound();
            }

            var salesLeadEntity = await _context.salesLead.FindAsync(id);
            if (salesLeadEntity == null)
            {
                return NotFound();
            }
            return View(salesLeadEntity);
        }

        [Authorize(Roles = "Admin,Sales")]

        // POST: Leads/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FirstName,LastName,Mobile,Email,Source")] SalesLeadEntity salesLeadEntity)
        {
            if (id != salesLeadEntity.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(salesLeadEntity);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SalesLeadEntityExists(salesLeadEntity.Id))
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
            return View(salesLeadEntity);
        }




        // GET: Leads/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.salesLead == null)
            {
                return NotFound();
            }

            var salesLeadEntity = await _context.salesLead
                .FirstOrDefaultAsync(m => m.Id == id);
            if (salesLeadEntity == null)
            {
                return NotFound();
            }

            return View(salesLeadEntity);
        }




        // POST: Leads/Delete/5
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.salesLead == null)
            {
                return Problem("Entity set 'ApplicationDbContext.salesLead'  is null.");
            }
            var salesLeadEntity = await _context.salesLead.FindAsync(id);
            if (salesLeadEntity != null)
            {
                _context.salesLead.Remove(salesLeadEntity);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SalesLeadEntityExists(int id)
        {
          return (_context.salesLead?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
