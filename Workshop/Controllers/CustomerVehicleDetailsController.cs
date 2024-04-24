using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Workshop.Data;
using Workshop.Models;

namespace Workshop.Controllers
{
    public class CustomerVehicleDetailsController : Controller
    {
        private readonly WorkShopContext _context;

        public CustomerVehicleDetailsController(WorkShopContext context)
        {
            _context = context;
        }

        // GET: CustomerVehicleDetails
        public async Task<IActionResult> Index()
        {
            var workShopContext = _context.CustomerVehicleDetails.Include(c => c.Customer).Include(c => c.Vehicle);
            return View(await workShopContext.ToListAsync());
        }

        // GET: CustomerVehicleDetails/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customerVehicleDetail = await _context.CustomerVehicleDetails
                .Include(c => c.Customer)
                .Include(c => c.Vehicle)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (customerVehicleDetail == null)
            {
                return NotFound();
            }

            return View(customerVehicleDetail);
        }

        // GET: CustomerVehicleDetails/Create
        public IActionResult Create()
        {
            ViewData["CustomerId"] = new SelectList(_context.CustomerDetails, "CustomerId", "CustomerId");
            ViewData["VehicleId"] = new SelectList(_context.VehicleDetails, "VehicleId", "VehicleId");
            return View();
        }

        // POST: CustomerVehicleDetails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CustomerId,VehicleId,VehicleNo,ServiceDueDate,IsServiceDone")] CustomerVehicleDetail customerVehicleDetail)
        {
            if (ModelState.IsValid)
            {
                _context.Add(customerVehicleDetail);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CustomerId"] = new SelectList(_context.CustomerDetails, "CustomerId", "CustomerId", customerVehicleDetail.CustomerId);
            ViewData["VehicleId"] = new SelectList(_context.VehicleDetails, "VehicleId", "VehicleId", customerVehicleDetail.VehicleId);
            return View(customerVehicleDetail);
        }

        // GET: CustomerVehicleDetails/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customerVehicleDetail = await _context.CustomerVehicleDetails.FindAsync(id);
            if (customerVehicleDetail == null)
            {
                return NotFound();
            }
            ViewData["CustomerId"] = new SelectList(_context.CustomerDetails, "CustomerId", "CustomerId", customerVehicleDetail.CustomerId);
            ViewData["VehicleId"] = new SelectList(_context.VehicleDetails, "VehicleId", "VehicleId", customerVehicleDetail.VehicleId);
            return View(customerVehicleDetail);
        }

        // POST: CustomerVehicleDetails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CustomerId,VehicleId,VehicleNo,ServiceDueDate,IsServiceDone")] CustomerVehicleDetail customerVehicleDetail)
        {
            if (id != customerVehicleDetail.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(customerVehicleDetail);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CustomerVehicleDetailExists(customerVehicleDetail.Id))
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
            ViewData["CustomerId"] = new SelectList(_context.CustomerDetails, "CustomerId", "CustomerId", customerVehicleDetail.CustomerId);
            ViewData["VehicleId"] = new SelectList(_context.VehicleDetails, "VehicleId", "VehicleId", customerVehicleDetail.VehicleId);
            return View(customerVehicleDetail);
        }

        // GET: CustomerVehicleDetails/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customerVehicleDetail = await _context.CustomerVehicleDetails
                .Include(c => c.Customer)
                .Include(c => c.Vehicle)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (customerVehicleDetail == null)
            {
                return NotFound();
            }

            return View(customerVehicleDetail);
        }

        // POST: CustomerVehicleDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var customerVehicleDetail = await _context.CustomerVehicleDetails.FindAsync(id);
            if (customerVehicleDetail != null)
            {
                _context.CustomerVehicleDetails.Remove(customerVehicleDetail);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CustomerVehicleDetailExists(int id)
        {
            return _context.CustomerVehicleDetails.Any(e => e.Id == id);
        }
    }
}
