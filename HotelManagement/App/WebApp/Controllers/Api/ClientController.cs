using App.Contracts.BLL;
using App.Public.DTO;
using App.Public.DTO.Mappers;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// unused
namespace WebApp.Controllers.Api
{
    /// <summary>
    /// Endpoint for managing hotel clients
    /// </summary>
    /*
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [Produces("application/json")]
    [Consumes("application/json")]
    */
    public class ClientController : ControllerBase
    {
        private readonly IAppBll _appBll;
        private readonly ClientMapper _mapper;

        public ClientController(IAppBll appBll, IMapper mapper)
        {
            _mapper = new ClientMapper(mapper);
            _appBll = appBll;
        }

        // GET: api/Client
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Client>))]
        public async Task<ActionResult<IEnumerable<Client?>>> GetClients()
        {
            var res = (await _appBll.Clients.GetAllAsync())
                .Select(e => _mapper.Map(e))
                .ToList();

            return res;
        }

        // GET: api/Client/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Client))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Client?>> GetClient(Guid id)
        {
            var client = await _appBll.Clients.FirstOrDefaultAsync(id);

            if (client == null)
            {
                return NotFound();
            }

            return _mapper.Map(client);
        }

        // PUT: api/Client/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PutClient(Guid id, Client client)
        {
            client.Id = id;
            _appBll.Clients.Update(_mapper.Map(client)!);

            try
            {
                await _appBll.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_appBll.Clients.Exists(id))
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

        // POST: api/Client
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Client))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Client>> PostClient(Client client)
        {
            var newClient = _appBll.Clients.Add(_mapper.Map(client)!);
            await _appBll.SaveChangesAsync();

            return CreatedAtAction("GetClient", new
            {
                version = HttpContext.GetRequestedApiVersion()!.ToString(),
                id = newClient.Id
            }, newClient);
        }

        // DELETE: api/Client/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteClient(Guid id)
        {
            if (!_appBll.Clients.Exists(id))
            {
                return NotFound();
            }

            _appBll.Clients.Remove(id);
            await _appBll.SaveChangesAsync();

            return NoContent();
        }
    }
}