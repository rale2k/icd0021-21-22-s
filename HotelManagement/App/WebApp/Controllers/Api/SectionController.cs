using App.Contracts.BLL;
using App.Public.DTO;
using App.Public.DTO.Mappers;
using AutoMapper;
using Base.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers.Api
{
    /// <summary>
    /// Endpoint for managing sections and subsections of a hotel
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [Produces("application/json")]
    [Consumes("application/json")]
    public class SectionController : ControllerBase
    {
        private readonly IAppBll _appBll;
        private readonly SectionMapper _mapper;
        private readonly RoomMapper _roomMapper;
        public SectionController(IAppBll appBll, IMapper mapper)
        {
            _mapper = new SectionMapper(mapper);
            _roomMapper = new RoomMapper(mapper);
            _appBll = appBll;
        }

        // GET: api/Section/Rooms/
        /// <summary>
        /// Retrieve all rooms of a section
        /// </summary>
        /// <param name="sectionId">ID of section</param>
        /// <returns>List of section rooms</returns>
        [HttpGet("Rooms/{sectionId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Room>))]
        public async Task<ActionResult<IEnumerable<Room?>>> GetSectionRooms(Guid sectionId)
        {
            var section = await _appBll.Sections.FirstOrDefaultAsync(sectionId);

            if (section == null)
            {
                return NotFound();
            }

            if (!_appBll.UserHotels.IsHotelUser(section.HotelId, User.GetUserId()))
            {
                return Unauthorized();
            }

            var res = (await _appBll.Rooms.GetSectionRoomsAsync(sectionId))
                .Select(e => _roomMapper.Map(e))
                .ToList();

            return res;
        }
        
        // GET: api/Section/5
        /// <summary>
        /// Retrieve a single section
        /// </summary>
        /// <param name="id">ID of section</param>
        /// <returns>Requested section</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Section))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Section?>> GetSection(Guid id)
        {
            var section = await _appBll.Sections.FirstOrDefaultAsync(id);

            if (section == null)
            {
                return NotFound();
            }

            if (!_appBll.UserHotels.IsHotelUser(section.HotelId, User.GetUserId()))
            {
                return Unauthorized();
            }

            return _mapper.Map(section);
        }
        
        // PUT: api/Section/5
        /// <summary>
        /// Update a section
        /// </summary>
        /// <param name="id">ID of section</param>
        /// <param name="section">New data to insert</param>
        /// <returns>Nocontent</returns>
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PutSection(Guid id, Section section)
        {
            var dbSection = await _appBll.Sections.FirstOrDefaultAsync(id);
            if (!_appBll.UserHotels.IsHotelUser(dbSection!.HotelId, User.GetUserId()))
            {
                return Unauthorized();
            }
            
            section.Id = id;
            section.HotelId = dbSection.HotelId;

            try
            {
                _appBll.Sections.Update(_mapper.Map(section)!);
                await _appBll.SaveChangesAsync();
            }
            catch (Exception)
            {
                if (!await _appBll.Sections.ExistsAsync(id))
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

        // POST: api/Section
        /// <summary>
        /// Create a new section
        /// </summary>
        /// <param name="section">New section</param>
        /// <returns>Created section</returns>
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Section))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Section>> PostSection(Section section)
        {
            if (!_appBll.UserHotels.IsHotelUser(section.HotelId, User.GetUserId()))
            {
                return Unauthorized();
            }

            var newSection = _appBll.Sections.Add(_mapper.Map(section)!);
            await _appBll.SaveChangesAsync();

            return CreatedAtAction("GetSection", new
            {
                version = HttpContext.GetRequestedApiVersion()!.ToString(),
                id = newSection.Id
            }, _mapper.Map(newSection));
        }

        // DELETE: api/Section/5
        /// <summary>
        /// Delete a section
        /// </summary>
        /// <param name="id">ID of section</param>
        /// <returns>Nocontent</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteSection(Guid id)
        {
            var section = await _appBll.Sections.FirstOrDefaultAsync(id);
            if (section == null || !_appBll.UserHotels.IsHotelUser(section.HotelId, User.GetUserId()))
            {
                return NotFound();
            }
                        
            await _appBll.Sections.RemoveAsync(id);
            await _appBll.SaveChangesAsync();

            return NoContent();
        }
    }
}