using App.DAL;
using App.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class StayController : Controller
    {
        private readonly AppDbContext _context;

        public StayController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Stay
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Stays.Include(s => s.Client).Include(s => s.Room);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Stay/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stay = await _context.Stays
                .Include(s => s.Client)
                .Include(s => s.Room)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (stay == null)
            {
                return NotFound();
            }

            return View(stay);
        }

        // GET: Stay/Create
        public IActionResult Create()
        {
            ViewData["ClientId"] = new SelectList(_context.Clients, "Id", "Email");
            ViewData["RoomId"] = new SelectList(_context.Rooms, "Id", "Name");
            return View();
        }

        // POST: Stay/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ClientId,RoomId,Start,End,Id")] Stay stay)
        {
            if (ModelState.IsValid)
            {
                stay.Id = Guid.NewGuid();
                _context.Add(stay);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClientId"] = new SelectList(_context.Clients, "Id", "Email", stay.ClientId);
            ViewData["RoomId"] = new SelectList(_context.Rooms, "Id", "Name", stay.RoomId);
            return View(stay);
        }

        // GET: Stay/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stay = await _context.Stays.FindAsync(id);
            if (stay == null)
            {
                return NotFound();
            }
            ViewData["ClientId"] = new SelectList(_context.Clients, "Id", "Email", stay.ClientId);
            ViewData["RoomId"] = new SelectList(_context.Rooms, "Id", "Name", stay.RoomId);
            return View(stay);
        }

        // POST: Stay/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("ClientId,RoomId,Start,End,Id")] Stay stay)
        {
            if (id != stay.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(stay);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StayExists(stay.Id))
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
            ViewData["ClientId"] = new SelectList(_context.Clients, "Id", "Email", stay.ClientId);
            ViewData["RoomId"] = new SelectList(_context.Rooms, "Id", "Name", stay.RoomId);
            return View(stay);
        }

        // GET: Stay/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stay = await _context.Stays
                .Include(s => s.Client)
                .Include(s => s.Room)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (stay == null)
            {
                return NotFound();
            }

            return View(stay);
        }

        // POST: Stay/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var stay = await _context.Stays.FindAsync(id);
            _context.Stays.Remove(stay!);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StayExists(Guid id)
        {
            return _context.Stays.Any(e => e.Id == id);
        }
    }
}
