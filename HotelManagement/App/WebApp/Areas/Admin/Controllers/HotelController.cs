using App.BLL.DTO;
using App.Contracts.BLL;
using Base.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "admin")]
    public class HotelController : Controller
    {
        private readonly IAppBll _appBll;

        public HotelController(IAppBll appBll)
        {
            _appBll = appBll;
        }

        // GET: Hotel
        public async Task<IActionResult> Index()
        {
            var userHotels = await _appBll.UserHotels.GetAllUserHotelsAsync(User.GetUserId());
            
            return View(userHotels.Select(uh => uh.Hotel).ToList());
        }

        // GET: Hotel/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userHotel = await _appBll.UserHotels.GetUserHotelAsync(id.Value, User.GetUserId());
            
            if (userHotel == null)
            {
                return NotFound();
            }

            return View(userHotel.Hotel);
        }

        // GET: Hotel/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Hotel/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Description,Id")] Hotel hotel)
        {
            if (ModelState.IsValid)
            {
                var newHotel = _appBll.Hotels.Add(hotel);
                _appBll.UserHotels.Add(new UserHotel()
                {
                    HotelId = newHotel.Id,
                    UserId = User.GetUserId()
                });
                await _appBll.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(hotel);
        }

        // GET: Hotel/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userHotel = await _appBll.UserHotels.GetUserHotelAsync(id.Value, User.GetUserId());
            
            if (userHotel == null)
            {
                return NotFound();
            }
            return View(userHotel.Hotel);
        }

        // POST: Hotel/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Name,Description,Id")] Hotel hotel)
        {
            if (id != hotel.Id)
            {
                return NotFound();
            }
            
            _appBll.Hotels.Update(hotel);
            
            if (ModelState.IsValid)
            {
                try
                {
                    await _appBll.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_appBll.Hotels.Exists(hotel.Id))
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
            return View(hotel);
        }

        // GET: Hotel/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userHotel = await _appBll.UserHotels.GetUserHotelAsync(id.Value, User.GetUserId());
            
            if (userHotel == null)
            {
                return NotFound();
            }

            return View(userHotel.Hotel);
        }

        // POST: Hotel/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var userHotel = await _appBll.UserHotels.GetUserHotelAsync(id, User.GetUserId());
            
            if (userHotel == null)
            {
                return NotFound();
            }

            _appBll.UserHotels.Remove(userHotel);
            _appBll.Hotels.Remove(userHotel.Hotel!);
            
            await _appBll.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
