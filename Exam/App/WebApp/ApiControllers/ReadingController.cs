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
    public class ReadingController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly ReadingMapper _readingMapper;

        public ReadingController(AppDbContext context, IMapper mapper)
        {
            _readingMapper = new ReadingMapper(mapper);
            _context = context;
        }

        // GET: api/Reading
        [HttpGet]
        public async Task<ActionResult<IEnumerable<App.Public.DTO.Reading?>>> GetReadings()
        {
            return await _context.Readings.Select(e => _readingMapper.Map(e)).ToListAsync();
        }

        // GET: api/Reading/5
        [HttpGet("{id}")]
        public async Task<ActionResult<App.Public.DTO.Reading?>> GetReading(Guid id)
        {
            var reading = await _context.Readings.FindAsync(id);

            if (reading == null)
            {
                return NotFound();
            }

            return _readingMapper.Map(reading);
        }

        // PUT: api/Reading/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutReading(Guid id, App.Public.DTO.Reading reading)
        {
            reading.Id = id;
            
            _context.Entry(_readingMapper.Map(reading)!).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReadingExists(id))
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

        // POST: api/Reading
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<App.Public.DTO.Reading>> PostReading(App.Public.DTO.Reading reading)
        {
            var newReading = _context.Readings.Add(_readingMapper.Map(reading)!).Entity;
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetReading", new { id = reading.Id }, _readingMapper.Map(newReading));
        }

        // DELETE: api/Reading/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReading(Guid id)
        {
            var reading = await _context.Readings.FindAsync(id);
            if (reading == null)
            {
                return NotFound();
            }

            _context.Readings.Remove(reading);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ReadingExists(Guid id)
        {
            return (_context.Readings?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
