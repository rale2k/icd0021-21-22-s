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
    /// Controller for API endpoint used for managing hotel amenities
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [Produces("application/json")]
    [Consumes("application/json")]
    public class AmenityController : ControllerBase
    {
        private readonly IAppBll _appBll;
        private readonly AmenityMapper _amenityMapper;

        public AmenityController(IAppBll appBll, IMapper mapper)
        {
            _amenityMapper = new AmenityMapper(mapper);
            _appBll = appBll;
        }


        // GET: api/Amenity/5
        /// <summary>
        /// Retrieve a single amenity
        /// </summary>
        /// <param name="id">ID of amenity</param>
        /// <returns>Requested amenity</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Amenity))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Amenity?>> GetAmenity(Guid id)
        {
            var amenity = await _appBll.Amenities.FirstOrDefaultAsync(id);

            if (amenity == null)
            {
                return NotFound();
            }
            
            if (!_appBll.UserHotels.IsHotelUser(amenity.HotelId, User.GetUserId()))
            {
                return Unauthorized();
            }

            return _amenityMapper.Map(amenity);
        }

        // PUT: api/Amenity/5
        /// <summary>
        /// Update an amenity
        /// </summary>
        /// <param name="id">ID of amenity</param>
        /// <param name="amenity">New data to insert</param>
        /// <returns>Nocontent</returns>
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PutAmenity(Guid id, Amenity amenity)
        {
            if (!_appBll.UserHotels.IsHotelUser(amenity.HotelId, User.GetUserId()))
            {
                return Unauthorized();
            }
            
            amenity.Id = id;

            try
            {
                _appBll.Amenities.Update(_amenityMapper.Map(amenity)!);
                await _appBll.SaveChangesAsync();
            }
            catch (Exception)
            {
                if (!_appBll.Amenities.Exists(id))
                {
                    return NotFound();
                }

                throw;
            }

            return NoContent();
        }

        // POST: api/Amenity
        /// <summary>
        /// Create a new amenity
        /// </summary>
        /// <param name="amenity">New amenity</param>
        /// <returns>Created amenity</returns>
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Amenity))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Amenity>> PostAmenity(Amenity amenity)
        {
            if (!_appBll.UserHotels.IsHotelUser(amenity.HotelId, User.GetUserId()))
            {
                return Unauthorized();
            }

            var newAmenity = _appBll.Amenities.Add(_amenityMapper.Map(amenity)!);
            await _appBll.SaveChangesAsync();

            return CreatedAtAction("GetAmenity", new
            {
                version = HttpContext.GetRequestedApiVersion()!.ToString(),
                id = newAmenity.Id
            }, newAmenity);
        }

        // DELETE: api/Amenity/5
        /// <summary>
        /// Delete an amenity
        /// </summary>
        /// <param name="id">ID of amenity</param>
        /// <returns>Nocontent</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteAmenity(Guid id)
        {
            var amenity = await _appBll.Amenities.FirstOrDefaultAsync(id);
            if (amenity == null || !_appBll.UserHotels.IsHotelUser(amenity.HotelId, User.GetUserId()))
            {
                return NotFound();
            }

            _appBll.Amenities.Remove(id);
            await _appBll.SaveChangesAsync();

            return NoContent();
        }
    }
}