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
    public class AmenityController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly AmenityMapper _amenityMapper;

        public AmenityController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _amenityMapper = new AmenityMapper(mapper);
        }

        // GET: api/Amenity
        [HttpGet]
        public async Task<ActionResult<IEnumerable<App.Public.DTO.Amenity?>>> GetAmenities()
        {
            return await _context.Amenities.Select(e => _amenityMapper.Map(e))
                .ToListAsync();
        }

        // GET: api/Amenity/5
        [HttpGet("{id}")]
        public async Task<ActionResult<App.Public.DTO.Amenity?>> GetAmenity(Guid id)
        {
            var amenity = await _context.Amenities.FindAsync(id);

            if (amenity == null)
            {
                return NotFound();
            }

            return _amenityMapper.Map(amenity);
        }

        // PUT: api/Amenity/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAmenity(Guid id, App.Public.DTO.Amenity amenity)
        {
            amenity.Id = id;
            _context.Entry(_amenityMapper.Map(amenity)!).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AmenityExists(id))
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

        // POST: api/Amenity
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<App.Public.DTO.Amenity>> PostAmenity(App.Public.DTO.Amenity amenity)
        {
            var newAmenity = _context.Amenities.Add(_amenityMapper.Map(amenity)!).Entity;
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAmenity", new { id = amenity.Id }, _amenityMapper.Map(newAmenity));
        }

        // DELETE: api/Amenity/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAmenity(Guid id)
        {
            var amenity = await _context.Amenities.FindAsync(id);
            if (amenity == null)
            {
                return NotFound();
            }

            _context.Amenities.Remove(amenity);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AmenityExists(Guid id)
        {
            return (_context.Amenities?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
