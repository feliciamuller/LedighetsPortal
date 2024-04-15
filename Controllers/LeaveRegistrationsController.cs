using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LedighetsPortal.Data;
using LedighetsPortal.Models;

namespace LedighetsPortal.Controllers
{
    public class LeaveRegistrationsController : Controller
    {
        private readonly LeaveDbContext _context;

        public LeaveRegistrationsController(LeaveDbContext context)
        {
            _context = context;
        }

        // GET: LeaveRegistrations
        public async Task<IActionResult> Index()
        {
            var leaveDbContext = _context.LeaveRegistrations.Include(l => l.Employee);
            return View(await leaveDbContext.ToListAsync());
        }

        // GET: LeaveRegistrations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var leaveRegistration = await _context.LeaveRegistrations
                .Include(l => l.Employee)
                .FirstOrDefaultAsync(m => m.LeaveId == id);
            if (leaveRegistration == null)
            {
                return NotFound();
            }

            return View(leaveRegistration);
        }

        // GET: LeaveRegistrations/Create
        public IActionResult Create()
        {
            ViewData["FKEmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "EmployeeFirstName");
            return View();
        }

        // POST: LeaveRegistrations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LeaveId,LeaveType,StartDate,EndDate,RegistrationDate,FKEmployeeId")] LeaveRegistration leaveRegistration)
        {
            if (ModelState.IsValid)
            {
                leaveRegistration.RegistrationDate = DateTime.Now;
                _context.Add(leaveRegistration);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FKEmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "EmployeeFirstName", leaveRegistration.FKEmployeeId);
            return View(leaveRegistration);
        }

        // GET: LeaveRegistrations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var leaveRegistration = await _context.LeaveRegistrations.FindAsync(id);
            if (leaveRegistration == null)
            {
                return NotFound();
            }
            ViewData["FKEmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "EmployeeFirstName", leaveRegistration.FKEmployeeId);
            return View(leaveRegistration);
        }

        // POST: LeaveRegistrations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("LeaveId,LeaveType,StartDate,EndDate,RegistrationDate,FKEmployeeId")] LeaveRegistration leaveRegistration)
        {
            if (id != leaveRegistration.LeaveId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(leaveRegistration);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LeaveRegistrationExists(leaveRegistration.LeaveId))
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
            ViewData["FKEmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "EmployeeFirstName", leaveRegistration.FKEmployeeId);
            return View(leaveRegistration);
        }

        // GET: LeaveRegistrations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var leaveRegistration = await _context.LeaveRegistrations
                .Include(l => l.Employee)
                .FirstOrDefaultAsync(m => m.LeaveId == id);
            if (leaveRegistration == null)
            {
                return NotFound();
            }

            return View(leaveRegistration);
        }

        // POST: LeaveRegistrations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var leaveRegistration = await _context.LeaveRegistrations.FindAsync(id);
            if (leaveRegistration != null)
            {
                _context.LeaveRegistrations.Remove(leaveRegistration);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LeaveRegistrationExists(int id)
        {
            return _context.LeaveRegistrations.Any(e => e.LeaveId == id);
        }
    }
}
