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
    /// Endpoint for managing hotel rooms
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [Produces("application/json")]
    [Consumes("application/json")]
    public class RoomController : ControllerBase
    {
        private readonly IAppBll _appBll;
        private readonly RoomMapper _mapper;
        private readonly TicketMapper _ticketMapper;
        private readonly StayMapper _stayMapper;

        public RoomController(IAppBll appBll, IMapper mapper)
        {
            _mapper = new RoomMapper(mapper);
            _ticketMapper = new TicketMapper(mapper);
            _stayMapper = new StayMapper(mapper);
            _appBll = appBll;
        }

        // GET: api/Room/Tickets/
        /// <summary>
        /// Retrieve all tickets of a room
        /// </summary>
        /// <param name="roomId">ID of room</param>
        /// <returns>List of room tickets</returns>
        [HttpGet("Tickets/{roomId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Ticket>))]
        public async Task<ActionResult<IEnumerable<Ticket?>>> GetRoomTickets(Guid roomId)
        {
            if (!_appBll.Rooms.IsHotelUserRoom(roomId, User.GetUserId()))
            {
                return Unauthorized();
            }

            var res = (await _appBll.Tickets.GetAllRoomTicketsAsync(roomId))
                .Select(e => _ticketMapper.Map(e))
                .ToList();

            return res;
        }
        
        // GET: api/Room/Stays/
        /// <summary>
        /// Retrieve all stays of a room
        /// </summary>
        /// <param name="roomId">ID of room</param>
        /// <returns>List of room stays</returns>
        [HttpGet("Stays/{roomId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Stay>))]
        public async Task<ActionResult<IEnumerable<Stay?>>> GetRoomStays(Guid roomId)
        {
            if (!_appBll.Rooms.IsHotelUserRoom(roomId, User.GetUserId()))
            {
                return Unauthorized();
            }

            var res = (await _appBll.Stays.GetAllRoomStaysAsync(roomId))
                .Select(e => _stayMapper.Map(e))
                .ToList();

            return res;
        }
        
        // GET: api/Room/5
        /// <summary>
        /// Retrieve a single room
        /// </summary>
        /// <param name="id">ID of room</param>
        /// <returns>Requested room</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Room))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Room?>> GetRoom(Guid id)
        {
            var room = await _appBll.Rooms.FirstOrDefaultAsync(id);

            if (room == null)
            {
                return NotFound();
            }
            
            if (!_appBll.Rooms.IsHotelUserRoom(id, User.GetUserId()))
            {
                return Unauthorized();
            }

            return _mapper.Map(room);
        }

        // PUT: api/Room/5
        /// <summary>
        /// Update a room
        /// </summary>
        /// <param name="roomId">ID of room</param>
        /// <param name="room">New data to insert</param>
        /// <returns>Nocontent</returns>
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{roomId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PutRoom(Guid roomId, Room room)
        {
            if (!_appBll.Rooms.IsHotelUserRoom(roomId, User.GetUserId()))
            {
                return Unauthorized();
            }

            room.Id = roomId;

            try
            {
                _appBll.Rooms.Update(_mapper.Map(room)!);
                await _appBll.SaveChangesAsync();
            }
            catch (Exception)
            {
                if (!await _appBll.Rooms.ExistsAsync(roomId))
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

        // POST: api/Room
        /// <summary>
        /// Create a new room
        /// </summary>
        /// <param name="room">New room</param>
        /// <returns>Created room</returns>
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Room))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Room>> PostRoom(Room room)
        {
            if (!_appBll.Sections.IsHotelUserSection(room.SectionId, User.GetUserId()))
            {
                return Unauthorized();
            }
            
            var newRoom = _appBll.Rooms.Add(_mapper.Map(room)!);
            await _appBll.SaveChangesAsync();

            return CreatedAtAction("GetRoom", new
            {
                version = HttpContext.GetRequestedApiVersion()!.ToString(),
                id = newRoom.Id
            }, newRoom);
        }

        // DELETE: api/Room/5
        /// <summary>
        /// Delete a room
        /// </summary>
        /// <param name="roomId">ID of room</param>
        /// <returns>Nocontent</returns>
        [HttpDelete("{roomId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteRoom(Guid roomId)
        {
            if (!await _appBll.Rooms.ExistsAsync(roomId))
            {
                return NotFound();
            }

            if (!_appBll.Rooms.IsHotelUserRoom(roomId, User.GetUserId()))
            {
                return Unauthorized();
            }

            await _appBll.Rooms.RemoveAsync(roomId);
            await _appBll.SaveChangesAsync();

            return NoContent();
        }
    }
}