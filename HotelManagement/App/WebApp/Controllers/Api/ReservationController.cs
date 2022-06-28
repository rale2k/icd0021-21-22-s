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
    /// Endpoint for managing client reservations
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [Produces("application/json")]
    [Consumes("application/json")]
    public class ReservationController : ControllerBase
    {
        private readonly IAppBll _appBll;
        private readonly ReservationMapper _mapper;
        private readonly ClientMapper _clientMapper;

        public ReservationController(IAppBll appBll, IMapper mapper)
        {
            _mapper = new ReservationMapper(mapper);
            _clientMapper = new ClientMapper(mapper);
            _appBll = appBll;
        }

        // GET: api/Reservation/5
        /// <summary>
        /// Retrieve a single reservation
        /// </summary>
        /// <param name="id">ID of reservation</param>
        /// <returns>Requested reservation</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Reservation>))]
        public async Task<ActionResult<Reservation?>> GetReservation(Guid id)
        {
            if (!_appBll.Reservations.IsHotelUserReservation(id, User.GetUserId()))
            {
                return Unauthorized();
            }

            var reservation = await _appBll.Reservations.FirstOrDefaultAsync(id);

            if (reservation == null)
            {
                return NotFound();
            }

            return _mapper.Map(reservation);
        }

        // PUT: api/Reservation/5
        /// <summary>
        /// Update a reservation
        /// </summary>
        /// <param name="id">ID of reservation</param>
        /// <param name="reservation">New data to insert</param>
        /// <returns>Nocontent</returns>
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PutReservation(Guid id, Reservation reservation)
        {
            if (!_appBll.Reservations.IsHotelUserReservation(id, User.GetUserId()))
            {
                return Unauthorized();
            }

            reservation.Id = id;

            try
            {
                _appBll.Reservations.Update(_mapper.Map(reservation)!);
                await _appBll.SaveChangesAsync();
            }
            catch (Exception)
            {
                if (!_appBll.Reservations.Exists(id))
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

        // POST: api/Reservation
        /// <summary>
        /// Create a new reservation
        /// </summary>
        /// <param name="reservation">New reservation</param>
        /// <returns>Created reservation</returns>
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Reservation))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [AllowAnonymous]
        public async Task<ActionResult<Reservation>> PostReservation(Reservation reservation)
        {
            var bllReservation = _mapper.Map(reservation)!;
            bllReservation.ClientId = _appBll.Clients.Add(_clientMapper.Map(reservation.Client)!).Id;
            
            var newReservation = _appBll.Reservations.Add(bllReservation);
            await _appBll.SaveChangesAsync();

            return CreatedAtAction("GetReservation", new
            {
                version = HttpContext.GetRequestedApiVersion()!.ToString(),
                id = newReservation.Id
            }, newReservation);
        }

        // DELETE: api/Reservation/5
        /// <summary>
        /// Delete a  reservation
        /// </summary>
        /// <param name="id">ID of reservation</param>
        /// <returns>Nocontent</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteReservation(Guid id)
        {
            if (!_appBll.Reservations.Exists(id))
            {
                return NotFound();
            }
            if (!_appBll.Reservations.IsHotelUserReservation(id, User.GetUserId()))
            {
                return Unauthorized();
            }

            _appBll.Reservations.Remove(id);
            await _appBll.SaveChangesAsync();

            return NoContent();
        }
    }
}