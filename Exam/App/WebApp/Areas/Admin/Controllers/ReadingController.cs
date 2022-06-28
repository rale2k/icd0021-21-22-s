using App.DAL;
using App.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Areas.Admin.Controllers
{
    public class ReadingController : Controller
    {
        private readonly AppDbContext _context;

        public ReadingController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Reading
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Readings.Include(r => r.Apartment).Include(r => r.Service);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Reading/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.Readings == null)
            {
                return NotFound();
            }

            var reading = await _context.Readings
                .Include(r => r.Apartment)
                .Include(r => r.Service)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reading == null)
            {
                return NotFound();
            }

            return View(reading);
        }

        // GET: Reading/Create
        public IActionResult Create()
        {
            ViewData["ApartmentId"] = new SelectList(_context.Apartments, "Id", "Description");
            ViewData["ServiceId"] = new SelectList(_context.Services, "Id", "Name");
            return View();
        }

        // POST: Reading/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ApartmentId,ServiceId,Value,PeriodStart,PeriodEnd,Id")] Reading reading)
        {
            if (ModelState.IsValid)
            {
                reading.Id = Guid.NewGuid();
                _context.Add(reading);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ApartmentId"] = new SelectList(_context.Apartments, "Id", "Description", reading.ApartmentId);
            ViewData["ServiceId"] = new SelectList(_context.Services, "Id", "Name", reading.ServiceId);
            return View(reading);
        }

        // GET: Reading/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.Readings == null)
            {
                return NotFound();
            }

            var reading = await _context.Readings.FindAsync(id);
            if (reading == null)
            {
                return NotFound();
            }
            ViewData["ApartmentId"] = new SelectList(_context.Apartments, "Id", "Description", reading.ApartmentId);
            ViewData["ServiceId"] = new SelectList(_context.Services, "Id", "Name", reading.ServiceId);
            return View(reading);
        }

        // POST: Reading/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("ApartmentId,ServiceId,Value,PeriodStart,PeriodEnd,Id")] Reading reading)
        {
            if (id != reading.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(reading);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReadingExists(reading.Id))
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
            ViewData["ApartmentId"] = new SelectList(_context.Apartments, "Id", "Description", reading.ApartmentId);
            ViewData["ServiceId"] = new SelectList(_context.Services, "Id", "Name", reading.ServiceId);
            return View(reading);
        }

        // GET: Reading/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.Readings == null)
            {
                return NotFound();
            }

            var reading = await _context.Readings
                .Include(r => r.Apartment)
                .Include(r => r.Service)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reading == null)
            {
                return NotFound();
            }

            return View(reading);
        }

        // POST: Reading/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.Readings == null)
            {
                return Problem("Entity set 'AppDbContext.Readings'  is null.");
            }
            var reading = await _context.Readings.FindAsync(id);
            if (reading != null)
            {
                _context.Readings.Remove(reading);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReadingExists(Guid id)
        {
          return (_context.Readings?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
