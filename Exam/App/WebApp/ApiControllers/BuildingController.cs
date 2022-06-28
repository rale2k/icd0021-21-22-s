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
    public class BuildingController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly BuildingMapper _buildingMapper;
        private readonly ApartmentMapper _apartmentMapper;

        public BuildingController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _buildingMapper = new BuildingMapper(mapper);
            _apartmentMapper = new ApartmentMapper(mapper);
        }
        
        // GET: api/Building
        [HttpGet("{id}/apartments")]
        public async Task<ActionResult<IEnumerable<App.Public.DTO.Apartment?>>> GetBuildingApartments(Guid id)
        {
            var domainBuildingApartments = await _context.Buildings
                .Include(e => e.Apartments)!
                    .ThenInclude(e => e.Amenities)
                .FirstOrDefaultAsync(e => e.Id == id);
            
            var publicBuildingApartments =
                domainBuildingApartments!.Apartments!.Select(e => _apartmentMapper.Map(e)!).ToList();

            // disaster
            foreach (var publicApt in publicBuildingApartments)
            {
                var apartmentContracts = _context.Contracts.Where(e => e.ApartmentId == publicApt.Id);

                foreach (var contract in apartmentContracts)
                {
                    if (contract.PeriodStart < DateTime.Now &&
                        contract.PeriodEnd > DateTime.Now)
                    {
                        publicApt.ContractId = contract.Id;
                    }
                }

            }

            return publicBuildingApartments;
        }
        
        // GET: api/Building
        [HttpGet]
        public async Task<ActionResult<IEnumerable<App.Public.DTO.Building?>>> GetBuildings()
        {
            return await _context.Buildings
                .Select(e => _buildingMapper.Map(e))
                .ToListAsync();
        }

        // GET: api/Building/5
        [HttpGet("{id}")]
        public async Task<ActionResult<App.Public.DTO.Building>> GetBuilding(Guid id)
        {
            var building = await _context.Buildings.FindAsync(id);

            if (building == null)
            {
                return NotFound();
            }

            return _buildingMapper.Map(building)!;
        }

        // PUT: api/Building/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBuilding(Guid id, App.Public.DTO.Building building)
        {
            building.Id = id;

            _context.Entry(_buildingMapper.Map(building)!).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BuildingExists(id))
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

        // POST: api/Building
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<App.Public.DTO.Amenity>> PostBuilding(App.Public.DTO.Building building)
        {
            var newBuilding = _context.Buildings.Add(_buildingMapper.Map(building)!).Entity;
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBuilding", new { id = building.Id },
                _buildingMapper.Map(newBuilding));
        }

        // DELETE: api/Building/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBuilding(Guid id)
        {
            var building = await _context.Buildings.FindAsync(id);
            if (building == null)
            {
                return NotFound();
            }

            _context.Buildings.Remove(building);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BuildingExists(Guid id)
        {
            return (_context.Buildings?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
