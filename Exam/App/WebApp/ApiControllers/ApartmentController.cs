using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using App.DAL;
using App.Domain;
using App.Public.DTO.Mappers;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace WebApp.ApiControllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [Produces("application/json")]
    [Consumes("application/json")]
    public class ApartmentController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly ApartmentMapper _apartmentMapper;

        public ApartmentController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _apartmentMapper = new ApartmentMapper(mapper);
        }

        // GET: api/Apartment/5
        [HttpGet("{id}")]
        public async Task<ActionResult<App.Public.DTO.Apartment>> GetApartment(Guid id)
        {
            var domainApartment = await _context.Apartments
                .Include(e => e.Amenities)
                .FirstOrDefaultAsync(e => e.Id == id)!;
            
            if (domainApartment == null)
            {
                return NotFound();
            }

            var publicApartment = _apartmentMapper.Map(domainApartment)!;
            
            // ghetto af, do not reccommend
            var apartmentContract = _context.Contracts.FirstOrDefault(e => e.ApartmentId == id);

            if (apartmentContract != null && 
                apartmentContract.PeriodStart < DateTime.Now &&
                apartmentContract.PeriodEnd > DateTime.Now)
            {
                publicApartment.ContractId = apartmentContract.Id;
            }
            
            return _apartmentMapper.Map(domainApartment)!;
        }

        // PUT: api/Apartment/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutApartment(Guid id, App.Public.DTO.Apartment apartment)
        {
            apartment.Id = id;
            _context.Entry(_apartmentMapper.Map(apartment)!).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ApartmentExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Apartment
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<App.Public.DTO.Apartment>> PostApartment(App.Public.DTO.Apartment apartment)
        {
            var domainApartment = _apartmentMapper.Map(apartment)!;
            domainApartment.Amenities = domainApartment.Amenities!.Select(e =>
                {
                    var amentity = _context.Amenities.FirstOrDefault(f => f == e);
                    return amentity;
                })
                .Where(e => e != null)
                .ToList()!;
            
            var newApartment = _context.Apartments.Add(domainApartment).Entity;
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetApartment", new { id = apartment.Id }, _apartmentMapper.Map(newApartment)!);
        }

        // DELETE: api/Apartment/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteApartment(Guid id)
        {
            var apartment = await _context.Apartments.FindAsync(id);
            if (apartment == null)
            {
                return NotFound();
            }

            _context.Apartments.Remove(apartment);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ApartmentExists(Guid id)
        {
            return (_context.Apartments?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
