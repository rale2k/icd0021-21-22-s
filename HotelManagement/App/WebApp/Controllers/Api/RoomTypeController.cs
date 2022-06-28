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
    /// Endpoint for managing a hotel room type
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [Produces("application/json")]
    [Consumes("application/json")]
    public class RoomTypeController : ControllerBase
    {
        private readonly IAppBll _appBll;
        private readonly RoomTypeMapper _mapper;

        public RoomTypeController(IAppBll appBll, IMapper mapper)
        {
            _mapper = new RoomTypeMapper(mapper);
            _appBll = appBll;
        }
        
        // GET: api/RoomType/5
        /// <summary>
        /// Retrieve a single roomtype 
        /// </summary>
        /// <param name="id">ID of requested roomtype</param>
        /// <returns>Roomtype</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(RoomType))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<RoomType?>> GetRoomType(Guid id)
        {
            var roomType = await _appBll.RoomTypes.FirstOrDefaultAsync(id);

            if (roomType == null)
            {
                return NotFound();
            }

            if (!_appBll.UserHotels.IsHotelUser(roomType.HotelId, User.GetUserId()))
            {
                return Unauthorized();
            }
            
            return _mapper.Map(roomType);
        }

        // PUT: api/RoomType/5
        /// <summary>
        /// Update a roomtype 
        /// </summary>
        /// <param name="id">ID of roomtype</param>
        /// <param name="roomType">New data to insert</param>
        /// <returns>Nocontent</returns>
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PutRoomType(Guid id, RoomType roomType)
        {
            if (!_appBll.UserHotels.IsHotelUser(roomType.HotelId, User.GetUserId()))
            {
                return Unauthorized();
            }

            roomType.Id = id;
            try
            {
                _appBll.RoomTypes.Update(_mapper.Map(roomType)!);
                await _appBll.SaveChangesAsync();
            }
            catch (Exception)
            {
                if (!_appBll.RoomTypes.Exists(id))
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

        // POST: api/RoomType
        /// <summary>
        /// Create a new roomtype 
        /// </summary>
        /// <param name="roomType">New roomtype</param>
        /// <returns>Created roomtype</returns>
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(RoomType))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<RoomType>> PostRoomType(RoomType roomType)
        {
            if (!_appBll.UserHotels.IsHotelUser(roomType.HotelId, User.GetUserId()))
            {
                return Unauthorized();
            }

            var newRoomType = _appBll.RoomTypes.Add(_mapper.Map(roomType)!);
            await _appBll.SaveChangesAsync();

            return CreatedAtAction("GetRoomType", new
            {
                version = HttpContext.GetRequestedApiVersion()!.ToString(),
                id = newRoomType.Id
            }, newRoomType);
        }

        // DELETE: api/RoomType/5
        /// <summary>
        /// Delete a roomtype
        /// </summary>
        /// <param name="id">ID of roomtype</param>
        /// <returns>Nocontent</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteRoomType(Guid id)
        {
            var roomType = await _appBll.RoomTypes.FirstOrDefaultAsync(id);
            if (roomType == null)
            {
                return NotFound();
            }
            
            if (!_appBll.UserHotels.IsHotelUser(roomType.HotelId, User.GetUserId()))
            {
                return Unauthorized();
            }

            _appBll.RoomTypes.Remove(roomType);
            await _appBll.SaveChangesAsync();

            return NoContent();
        }
    }
}