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
    public class VehicleBookingDetailsController : Controller
    {
        private readonly WorkShopContext _context;

        public VehicleBookingDetailsController(WorkShopContext context)
        {
            _context = context;
        }

        // GET: VehicleBookingDetails
        public async Task<IActionResult> Index()
        {
            var workShopContext = _context.VehicleBookingDetails.Include(v => v.Customer).Include(v => v.Vehicle);
            return View(await workShopContext.ToListAsync());
        }

        // GET: VehicleBookingDetails/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.VehicleBookingDetails == null)
            {
                return NotFound();
            }

            var vehicleBookingDetail = await _context.VehicleBookingDetails
                .Include(v => v.Customer)
                .Include(v => v.Vehicle)
                .FirstOrDefaultAsync(m => m.BookingId == id);
            if (vehicleBookingDetail == null)
            {
                return NotFound();
            }

            return View(vehicleBookingDetail);
        }

        // GET: VehicleBookingDetails/Create
        public IActionResult Create()
        {
            ViewData["CustomerId"] = new SelectList(_context.CustomerDetails, "CustomerId", "Name");
            ViewData["VehicleId"] = new SelectList(_context.VehicleDetails, "VehicleId", "VehicleName");
            return View();
        }

        // POST: VehicleBookingDetails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CustomerId,VehicleId,BookingDate,BookingTime")] VehicleBookingDetail vehicleBookingDetail)
        {
            if (ModelState.IsValid)
            {
                var vehicleBookingDetails = _context.VehicleBookingDetails
               .Where(v => v.BookingDate == vehicleBookingDetail.BookingDate).ToList();

                if (vehicleBookingDetails.Count >= 4)
                {
                    ModelState.AddModelError("BookingTime", "You can book only 4 slot per day");
                    ViewData["CustomerId"] = new SelectList(_context.CustomerDetails, "CustomerId", "Name", vehicleBookingDetail.CustomerId);
                    ViewData["VehicleId"] = new SelectList(_context.VehicleDetails, "VehicleId", "VehicleName", vehicleBookingDetail.VehicleId);
                    return View(vehicleBookingDetail);
                }

                if (vehicleBookingDetails.Count > 0)
                {
                    if (vehicleBookingDetails.Any(x => (TimeSpan)x.BookingTime == vehicleBookingDetail.BookingTime))
                    {
                        ModelState.AddModelError("BookingTime", "This slot already booked for " + vehicleBookingDetail.BookingDate.Value.ToString("MM/dd/yyyy"));
                        ViewData["CustomerId"] = new SelectList(_context.CustomerDetails, "CustomerId", "Name", vehicleBookingDetail.CustomerId);
                        ViewData["VehicleId"] = new SelectList(_context.VehicleDetails, "VehicleId", "VehicleName", vehicleBookingDetail.VehicleId);
                        return View(vehicleBookingDetail);
                    }
                }

                vehicleBookingDetail.CreatedDate = DateTime.Now;
                vehicleBookingDetail.UpdatedDate = DateTime.Now;
                _context.Add(vehicleBookingDetail);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CustomerId"] = new SelectList(_context.CustomerDetails, "CustomerId", "Name", vehicleBookingDetail.CustomerId);
            ViewData["VehicleId"] = new SelectList(_context.VehicleDetails, "VehicleId", "VehicleName", vehicleBookingDetail.VehicleId);
            return View(vehicleBookingDetail);
        }

        // GET: VehicleBookingDetails/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.VehicleBookingDetails == null)
            {
                return NotFound();
            }

            var vehicleBookingDetail = await _context.VehicleBookingDetails.FindAsync(id);
            if (vehicleBookingDetail == null)
            {
                return NotFound();
            }
            ViewData["CustomerId"] = new SelectList(_context.CustomerDetails, "CustomerId", "Name", vehicleBookingDetail.CustomerId);
            ViewData["VehicleId"] = new SelectList(_context.VehicleDetails, "VehicleId", "VehicleName", vehicleBookingDetail.VehicleId);
            return View(vehicleBookingDetail);
        }

        // POST: VehicleBookingDetails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BookingId,CustomerId,VehicleId,BookingDate,BookingTime")] VehicleBookingDetail vehicleBookingDetail)
        {
            if (id != vehicleBookingDetail.BookingId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vehicleBookingDetail);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VehicleBookingDetailExists(vehicleBookingDetail.BookingId))
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
            ViewData["CustomerId"] = new SelectList(_context.CustomerDetails, "CustomerId", "Name", vehicleBookingDetail.CustomerId);
            ViewData["VehicleId"] = new SelectList(_context.VehicleDetails, "VehicleId", "VehicleName", vehicleBookingDetail.VehicleId);
            return View(vehicleBookingDetail);
        }

        // GET: VehicleBookingDetails/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.VehicleBookingDetails == null)
            {
                return NotFound();
            }

            var vehicleBookingDetail = await _context.VehicleBookingDetails
                .Include(v => v.Customer)
                .Include(v => v.Vehicle)
                .FirstOrDefaultAsync(m => m.BookingId == id);
            if (vehicleBookingDetail == null)
            {
                return NotFound();
            }

            return View(vehicleBookingDetail);
        }

        // POST: VehicleBookingDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.VehicleBookingDetails == null)
            {
                return Problem("Entity set 'WorkShopContext.VehicleBookingDetails'  is null.");
            }
            var vehicleBookingDetail = await _context.VehicleBookingDetails.FindAsync(id);
            if (vehicleBookingDetail != null)
            {
                _context.VehicleBookingDetails.Remove(vehicleBookingDetail);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VehicleBookingDetailExists(int id)
        {
            return (_context.VehicleBookingDetails?.Any(e => e.BookingId == id)).GetValueOrDefault();
        }
    }
}
