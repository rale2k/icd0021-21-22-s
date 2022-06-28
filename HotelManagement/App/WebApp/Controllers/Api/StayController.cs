using App.Contracts.BLL;
using App.Public.DTO;
using App.Public.DTO.Mappers;
using AutoMapper;
using Base.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Controllers.Api
{
    /// <summary>
    /// Endpoint for managing visits of a hotel
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [Produces("application/json")]
    [Consumes("application/json")]
    public class StayController : ControllerBase
    {
        private readonly IAppBll _appBll;
        private readonly StayMapper _mapper;
        private readonly ClientMapper _clientMapper;

        public StayController(IAppBll appBll, IMapper mapper)
        {
            _clientMapper = new ClientMapper(mapper);
            _mapper = new StayMapper(mapper);
            _appBll = appBll;
        }

        // GET: api/Stay/5
        /// <summary>
        /// Retrieve a single stay
        /// </summary>
        /// <param name="id">ID of stay</param>
        /// <returns>Requested stay</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Stay))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Stay?>> GetStay(Guid id)
        {
            if (!_appBll.Stays.IsHotelUserStay(id, User.GetUserId()))
            {
                return Unauthorized();
            }

            var stay = await _appBll.Stays.FirstOrDefaultAsync(id);

            if (stay == null)
            {
                return NotFound();
            }

            return _mapper.Map(stay);
        }

        // PUT: api/Stay/5
        /// <summary>
        /// Update a stay
        /// </summary>
        /// <param name="id">ID of stay</param>
        /// <param name="stay">New data to insert</param>
        /// <returns>Nocontent</returns>
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PutStay(Guid id, Stay stay)
        {
            if (!_appBll.Stays.IsHotelUserStay(id, User.GetUserId()))
            {
                return Unauthorized();
            }

            stay.Id = id;
            _appBll.Stays.Update(_mapper.Map(stay)!);

            try
            {
                await _appBll.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _appBll.Stays.ExistsAsync(id))
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

        // POST: api/Stay
        /// <summary>
        /// Create a new stay
        /// </summary>
        /// <param name="stay">New stay</param>
        /// <returns>Created stay</returns>
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Stay))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Stay>> PostStay(Stay stay)
        {
            if (!_appBll.Rooms.IsHotelUserRoom(stay.RoomId, User.GetUserId()))
            {
                return Unauthorized();
            }

            App.BLL.DTO.Stay? newStay = null;
            var bllStay = _mapper.Map(stay)!;

            if (stay.Client.Id == Guid.Empty)
            {
                bllStay.ClientId = _appBll.Clients.Add(_clientMapper.Map(stay.Client)!).Id;
                newStay = _appBll.Stays.Add(bllStay);
            }
            else
            {
                bllStay.ClientId = stay.Client.Id;
                bllStay.Client = null;
                newStay = _appBll.Stays.Add(bllStay);
            }

            await _appBll.SaveChangesAsync();
            return CreatedAtAction("GetStay", new
            {
                version = HttpContext.GetRequestedApiVersion()!.ToString(),
                id = newStay.Id
            }, newStay);
        }

        // DELETE: api/Stay/5
        /// <summary>
        /// Delete a stay
        /// </summary>
        /// <param name="id">ID of stay</param>
        /// <returns>Nocontent</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteStay(Guid id)
        {
            if (!_appBll.Stays.IsHotelUserStay(id, User.GetUserId()))
            {
                return Unauthorized();
            }

            if (!await _appBll.Stays.ExistsAsync(id))
            {
                return NotFound();
            }

            await _appBll.Stays.RemoveAsync(id);
            await _appBll.SaveChangesAsync();

            return NoContent();
        }
    }
}