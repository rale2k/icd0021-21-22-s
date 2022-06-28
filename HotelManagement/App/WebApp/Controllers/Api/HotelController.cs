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
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [Produces("application/json")]
    [Consumes("application/json")]
    public class HotelController : ControllerBase
    {
        private readonly IAppBll _appBll;
        
        private readonly SectionMapper _sectionMapper;
        private readonly HotelMapper _hotelMapper;
        private readonly UserHotelMapper _userHotelMapper;
        private readonly RoomTypeMapper _roomTypeMapper;
        private readonly TicketMapper _ticketMapper;
        private readonly ReservationMapper _reservationMapper;
        private readonly RoomMapper _roomMapper;
        private readonly AmenityMapper _amenityMapper;

        public HotelController(IAppBll appBll, IMapper mapper)
        {
            _sectionMapper = new SectionMapper(mapper);
            _hotelMapper = new HotelMapper(mapper);
            _userHotelMapper = new UserHotelMapper(mapper);
            _roomTypeMapper = new RoomTypeMapper(mapper);
            _ticketMapper = new TicketMapper(mapper);
            _reservationMapper = new ReservationMapper(mapper);
            _roomMapper = new RoomMapper(mapper);
            _amenityMapper = new AmenityMapper(mapper);
            _appBll = appBll;
        }
        
        // GET: api/Hotels/Amenities
        /// <summary>
        /// Retrieve all amenities of a hotel
        /// </summary>
        /// <param name="hotelId">ID of hotel</param>
        /// <returns>List of hotel amenities</returns>
        [HttpGet("Amenities/{hotelId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Amenity>))]
        public async Task<ActionResult<IEnumerable<Amenity?>>> GetHotelAmenities(Guid hotelId)
        {
            if (!_appBll.UserHotels.IsHotelUser(hotelId, User.GetUserId()))
            {
                return Unauthorized();
            }

            var res = (await _appBll.Amenities.GetHotelAmenitiesAsync(hotelId))
                .Select(e => _amenityMapper.Map(e))
                .ToList();

            return res;
        }
        
        // GET: api/Hotel/Rooms/
        /// <summary>
        /// Retrieve all rooms of a hotel
        /// </summary>
        /// <param name="hotelId">ID of hotel</param>
        /// <returns>List of hotel rooms</returns>
        [HttpGet("Rooms/{hotelId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Room>))]
        public async Task<ActionResult<IEnumerable<Room?>>> GetHotelRooms(Guid hotelId)
        {
            if (!_appBll.UserHotels.IsHotelUser(hotelId, User.GetUserId()))
            {
                return Unauthorized();
            }

            var res = (await _appBll.Rooms.GetHotelRoomsAsync(hotelId))
                .Select(e => _roomMapper.Map(e))
                .ToList();

            return res;
        }
        
        // GET: api/Hotel/Reservations/
        /// <summary>
        /// Retrieve all reservations of a hotel
        /// </summary>
        /// <param name="hotelId">ID of hotel</param>
        /// <returns>List of hotel reservations</returns>
        [HttpGet("Reservations/{hotelId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Reservation>))]
        public async Task<ActionResult<IEnumerable<Reservation?>>> GetHotelReservations(Guid hotelId)
        {
            if (!_appBll.UserHotels.IsHotelUser(hotelId, User.GetUserId()))
            {
                return Unauthorized();
            }

            var res = (await _appBll.Reservations.GetHotelReservationsAsync(hotelId))
                .Select(e => _reservationMapper.Map(e))
                .ToList();

            return res;
        }
        
        // GET: api/Hotel/Tickets/
        /// <summary>
        /// Retrieve all tickets of a hotel
        /// </summary>
        /// <param name="hotelId">ID of hotel</param>
        /// <returns>List of hotel tickets</returns>
        [HttpGet("Tickets/{hotelId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Ticket>))]
        public async Task<ActionResult<IEnumerable<Ticket?>>> GetHotelTickets(Guid hotelId)
        {
            if (!_appBll.UserHotels.IsHotelUser(hotelId, User.GetUserId()))
            {
                return Unauthorized();
            }

            var res = (await _appBll.Tickets.GetAllHotelTicketsAsync(hotelId))
                .Select(e => _ticketMapper.Map(e))
                .ToList();

            return res;
        }
        
        // GET: api/Hotel/Sections/
        /// <summary>
        /// Retrieve all sections of a hotel
        /// </summary>
        /// <param name="hotelId">ID of hotel</param>
        /// <returns>List of hotel sections</returns>
        [HttpGet("Sections/{hotelId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Section>))]
        public async Task<ActionResult<IEnumerable<Section?>>> GetHotelSections(Guid hotelId)
        {
            if (!_appBll.UserHotels.IsHotelUser(hotelId, User.GetUserId()))
            {
                return Unauthorized();
            }

            var res = (await _appBll.Sections.GetHotelSectionsAsync(hotelId))
                .Select(e => _sectionMapper.Map(e))
                .ToList();

            return res;
        }

        // GET: api/Hotel/RoomTypes
        /// <summary>
        /// Retrieve all roomtypes of a hotel
        /// </summary>
        /// <param name="hotelId">ID of hotel</param>
        /// <returns>List of hotel roomtypes</returns>
        [HttpGet("RoomTypes/{hotelId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<RoomType>))]
        public async Task<ActionResult<IEnumerable<RoomType?>>> GetHotelRoomTypes(Guid hotelId)
        {
            if (!_appBll.UserHotels.IsHotelUser(hotelId, User.GetUserId()))
            {
                return Unauthorized();
            }

            var res = (await _appBll.RoomTypes.GetHotelRoomTypesAsync(hotelId))
                .Select(e => _roomTypeMapper.Map(e))
                .ToList();

            return res;
        }
        
        // GET: api/Hotel
        /// <summary>
        /// Retrieve all hotels an user has access to
        /// </summary>
        /// <returns>List of hotels</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Hotel>))]
        public async Task<ActionResult<IEnumerable<Hotel?>>> GetUserHotels()
        {
            var res = (await _appBll.UserHotels.GetAllUserHotelsAsync(User.GetUserId()))
                .Select(uh => _hotelMapper.Map(uh.Hotel!))
                .ToList();

            return res;
        }

        // GET: api/Hotel/5
        /// <summary>
        /// Retrieve a single hotel
        /// </summary>
        /// <param name="id">ID of hotel</param>
        /// <returns>Requested hotel</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Hotel))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [AllowAnonymous]
        public async Task<ActionResult<Hotel?>> GetHotel(Guid id)
        {
            var hotel = await _appBll.Hotels.FirstOrDefaultAsync(id);

            if (hotel == null)
            {
                return NotFound();
            }

            return _hotelMapper.Map(hotel);
        }

        // PUT: api/Hotel/5
        /// <summary>
        /// Update a hotel
        /// </summary>
        /// <param name="id">ID of hotel</param>
        /// <param name="hotel">New data to insert</param>
        /// <returns>Nocontent</returns>
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize(Roles = "user, admin")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PutHotel(Guid id, Hotel hotel)
        {
            if (!_appBll.UserHotels.IsHotelUser(id, User.GetUserId()))
            {
                return Unauthorized();
            }

            hotel.Id = id;
            _appBll.Hotels.Update(_hotelMapper.Map(hotel)!);

            try
            {
                await _appBll.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _appBll.Hotels.ExistsAsync(id))
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

        // POST: api/Hotel
        /// <summary>
        /// Create a new hotel
        /// </summary>
        /// <param name="hotel">New hotel</param>
        /// <returns>Created hotel</returns>
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize(Roles = "user")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Hotel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Hotel>> PostHotel(Hotel hotel)
        {
            var newHotel = _appBll.Hotels.Add(_hotelMapper.Map(hotel)!);
            _appBll.UserHotels.Add(_userHotelMapper.Map(new UserHotel()
            {
                HotelId = newHotel.Id,
                UserId = User.GetUserId()
            })!);

            await _appBll.SaveChangesAsync();

            return CreatedAtAction("GetHotel", new
            {
                version = HttpContext.GetRequestedApiVersion()!.ToString(),
                id = newHotel.Id
            }, newHotel);
        }

        // DELETE: api/Hotel/5
        /// <summary>
        /// Delete a new hotel
        /// NB: !!! DELETES EVERYTHING RELATED TO THE HOTEL ASWELL !!!
        /// </summary>
        /// <param name="id">ID of hotel</param>
        /// <returns>Nocontent</returns>
        [HttpDelete("{id}")]
        [Authorize(Roles = "user")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteHotel(Guid id)
        {
            if (!_appBll.UserHotels.IsHotelUser(id, User.GetUserId()))
            {
                return Unauthorized();
            }

            if (!await _appBll.Hotels.ExistsAsync(id))
            {
                return NotFound();
            }

            _appBll.UserHotels.RemoveAllHotelUsers(id);
            await _appBll.Hotels.RemoveAsync(id);
            await _appBll.SaveChangesAsync();

            return NoContent();
        }
    }
}