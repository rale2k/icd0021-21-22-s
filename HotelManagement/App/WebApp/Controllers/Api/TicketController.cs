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
    /// Endpoint for managing tickets of a hotel
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [Produces("application/json")]
    [Consumes("application/json")]
    public class TicketController : ControllerBase
    {
        private readonly IAppBll _appBll;
        private readonly TicketMapper _mapper;

        public TicketController(IAppBll appBll, IMapper mapper)
        {
            _mapper = new TicketMapper(mapper);
            _appBll = appBll;
        }
        
        // GET: api/Ticket/5
        /// <summary>
        /// Retrieve a single ticket
        /// </summary>
        /// <param name="id">ID of ticket</param>
        /// <returns>Requested ticket</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Ticket))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Ticket?>> GetTicket(Guid id)
        {
            var ticket = await _appBll.Tickets.FirstOrDefaultAsync(id);
            
            if (ticket == null)
            {
                return NotFound();
            }
            
            if (!_appBll.UserHotels.IsHotelUser(ticket.HotelId, User.GetUserId()))
            {
                return Unauthorized();
            }
            
            return _mapper.Map(ticket);
        }

        // PUT: api/Ticket/5
        /// <summary>
        /// Update a ticket
        /// </summary>
        /// <param name="id">ID of ticket</param>
        /// <param name="ticket">New data to insert</param>
        /// <returns>Nocontent</returns>
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PutTicket(Guid id, Ticket ticket)
        {
            if (!_appBll.UserHotels.IsHotelUser(ticket.HotelId, User.GetUserId()))
            {
                return Unauthorized();
            }

            ticket.Id = id;
            try
            {
                _appBll.Tickets.Update(_mapper.Map(ticket)!);
                await _appBll.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _appBll.Tickets.ExistsAsync(id))
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

        // POST: api/Ticket
        /// <summary>
        /// Create a new ticket
        /// </summary>
        /// <param name="ticket">New data to insert</param>
        /// <returns>Created ticket</returns>
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Ticket))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Ticket>> PostTicket(Ticket ticket)
        {
            var newTicket = _appBll.Tickets.Add(_mapper.Map(ticket)!);
            await _appBll.SaveChangesAsync();

            return CreatedAtAction("GetTicket", new
            {
                version = HttpContext.GetRequestedApiVersion()!.ToString(),
                id = newTicket.Id
            }, newTicket);
        }

        // DELETE: api/Ticket/5
        /// <summary>
        /// Delete a new ticket
        /// </summary>
        /// <param name="id">ID of ticket</param>
        /// <returns>Nocontent</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteTicket(Guid id)
        {
            var ticket = await _appBll.Tickets.FirstOrDefaultAsync(id);
            if (ticket == null || !_appBll.UserHotels.IsHotelUser(ticket.HotelId, User.GetUserId()))
            {
                return NotFound();
            }

            await _appBll.Tickets.RemoveAsync(id);
            await _appBll.SaveChangesAsync();

            return NoContent();
        }
    }
}