using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using App.DAL;
using App.Domain.Identity;
using App.Public.DTO.Identity;
using Base.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Controllers.Api.identity;

/// <summary>
/// Controller for API endpoint used for managing user accounts
/// </summary>
[ApiVersion( "1.0" )]
[Route("api/v{version:apiVersion}/identity/[controller]/[action]")]
[ApiController]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
[Produces("application/json")]
[Consumes("application/json")]
public class AccountController : ControllerBase
{
    private readonly SignInManager<AppUser> _signInManager;
    private readonly UserManager<AppUser> _userManager;
    private readonly ILogger<AccountController> _logger;
    private readonly IConfiguration _configuration;
    private readonly Random _rnd = new();
    private readonly AppDbContext _accounts;

    public AccountController(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager,
        IConfiguration configuration, ILogger<AccountController> logger, AppDbContext accounts)
    {
        _signInManager = signInManager;
        _userManager = userManager;
        _configuration = configuration;
        _logger = logger;
        _accounts = accounts;
    }

    /// <summary>
    /// Login into the rest backend - generates JWT to be included in
    /// Authorize: Bearer xyz
    /// </summary>
    /// <param name="loginData">Supply email and password</param>
    /// <returns>JWT and refresh token</returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(JwtResponse))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Produces("application/json")]
    [Consumes("application/json")]
    public async Task<ActionResult<JwtResponse>> LogIn([FromBody] Login loginData)
    {
        // Verify username exists
        var appUser = await _userManager.FindByEmailAsync(loginData.Email);
        if (appUser == null)
        {
            _logger.LogWarning("WebApi login failed, email {} not found", loginData.Email);
            await Task.Delay(_rnd.Next(100, 1000));
            return Problem("User or Password invalid", null, 401);
        }

        // Verify that passwords match
        var result = await _signInManager.CheckPasswordSignInAsync(appUser, loginData.Password, false);
        if (!result.Succeeded)
        {
            _logger.LogWarning("WebApi login failed, password problem for user {}", loginData.Email);
            await Task.Delay(_rnd.Next(100, 1000));
            return Problem("User or Password invalid", null, 401);
        }

        // Get claims manager of user
        var claimsPrincipal = await _signInManager.CreateUserPrincipalAsync(appUser);
        if (claimsPrincipal == null)
        {
            _logger.LogWarning("Could not get ClaimsPrincipal for user {}", loginData.Email);
            await Task.Delay(_rnd.Next(100, 1000));
            return Problem("User or Password invalid", null, 401);
        }

        var jwt = IdentityExtensions.GenerateJwt(
            claimsPrincipal.Claims,
            _configuration["JWT:Key"],
            _configuration["JWT:Issuer"],
            _configuration["JWT:Issuer"],
            DateTime.Now.AddMinutes(_configuration.GetValue<int>("JWT:ExpireInMinutes"))
        );

        // Load user tokens
        await _accounts.Entry(appUser)
            .Collection(a => a.RefreshTokens!)
            .Query()
            .Where(r => r.AppUserId == appUser.Id)
            .LoadAsync();
    
        // Delete dated refresh tokens
        foreach (var userRefreshToken in appUser.RefreshTokens!)
        {
            if (userRefreshToken.TokenExpirationDateTime < DateTime.UtcNow &&
                (userRefreshToken.PreviousTokenExpirationDateTime < DateTime.UtcNow || 
                 userRefreshToken.PreviousTokenExpirationDateTime == null))
            {
                _accounts.RefreshTokens.Remove(userRefreshToken);
            }
        }

        var newRefreshToken = new RefreshToken
        {
            AppUserId = appUser.Id
        };
        
        _accounts.RefreshTokens.Add(newRefreshToken);
        
        await _accounts.SaveChangesAsync();
        
        var res = new JwtResponse()
        {
            Jwt = jwt,
            RefreshToken = newRefreshToken.Token
        };

        return Ok(res);
    }
    /// <summary>
    /// Create an account to access the rest backend 
    /// </summary>
    /// <param name="registrationData">Email, Password, FirstName and Lastname</param>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Produces("application/json")]
    [Consumes("application/json")]
    public async Task<ActionResult> Register(Register registrationData)
    {
        // Verify user with email doesn't already exist 
        var appUser = await _userManager.FindByEmailAsync(registrationData.Email);
        if (appUser != null)
        {
            _logger.LogWarning("User with email {} is already registered", registrationData.Email);
            return Problem("Email already registered", null, 400);
        }
        
        var refreshToken = new RefreshToken();
        appUser = new AppUser()
        {
            FirstName = registrationData.FirstName,
            LastName = registrationData.LastName,
            Email = registrationData.Email,
            UserName = registrationData.Email,
            RefreshTokens = new List<RefreshToken>()
            {
                refreshToken
            }
        };

        // Create user
        var result = await _userManager.CreateAsync(appUser, registrationData.Password);
        if (!result.Succeeded)
        {
            return Problem(result.ToString(), null, 400);
        }

        result = await _userManager.AddClaimAsync(appUser, new Claim(ClaimTypes.GivenName, appUser.FirstName));
        if (!result.Succeeded)
        {
            return Problem(result.ToString(), null, 400);
        }

        result = await _userManager.AddClaimAsync(appUser, new Claim(ClaimTypes.Surname, appUser.LastName));
        if (!result.Succeeded)
        {
            return Problem(result.ToString(), null, 400);
        }
        
        result = await _userManager.AddToRoleAsync(appUser, "user");
        if (!result.Succeeded)
        {
            return Problem(result.ToString(), null, 400);
        }
        
        // Verify that user actually got created
        appUser = await _userManager.FindByEmailAsync(appUser.Email);
        if (appUser == null)
        {
            _logger.LogWarning("User with email {} is not found after registration", registrationData.Email);
            return Problem($"User with email {registrationData.Email} is not found after registration",
                null, 400);
        }

        // Get claims manager of user
        var claimsPrincipal = await _signInManager.CreateUserPrincipalAsync(appUser);
        if (claimsPrincipal == null)
        {
            _logger.LogWarning("Could not get ClaimsPrincipal for user {}", registrationData.Email);
            return Problem($"User/Password problem",
                null, 404);
        }

        return Ok();
    }

    /// <summary>
    /// Refresh JWT token 
    /// </summary>
    /// <param name="refreshTokenDto">Old JWT token and refreshtoken</param>
    /// <returns>New JWT token and refreshtoken</returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(JwtResponse))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Produces("application/json")]
    [Consumes("application/json")]
    public async Task<ActionResult> RefreshToken([FromBody] RefreshTokenDto refreshTokenDto)
    {

        JwtSecurityToken jwtToken;
        // Parse JWT token
        try
        {
            jwtToken = new JwtSecurityTokenHandler().ReadJwtToken(refreshTokenDto.Jwt);
            if (jwtToken == null)
            {
                return Problem("No token.", null, 400);
            }
        }
        catch (Exception)
        {
            return Problem($"Error parsing the token.", null, 400);
        }
        
        // TODO: Validate token signature to prevent abuse of random tokens adhering to JWT structure
        // https://stackoverflow.com/questions/49407749/jwt-token-validation-in-asp-net

        var userEmail = jwtToken.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;
        if (userEmail == null)
        {
            return Problem($"Email claim not present in JWT token.", null, 400);
        }

        // Get user by email claim in recieved token
        var appUser = await _userManager.FindByEmailAsync(userEmail);
        if (appUser == null)
        {
            return Problem($"User with email \"{userEmail}\" not found", null, 404);
        }
        
        // Load user refresh tokens
        await _accounts.Entry(appUser).Collection(u => u.RefreshTokens!)
            .Query()
            .Where(x => 
                (x.Token == refreshTokenDto.RefreshToken && x.TokenExpirationDateTime > DateTime.UtcNow) ||
                (x.PreviousToken == refreshTokenDto.RefreshToken && x.PreviousTokenExpirationDateTime > DateTime.UtcNow))
            .LoadAsync();

        if (appUser.RefreshTokens == null)
        {
            return Problem("RefreshTokens collection is null");
        }
        
        if (appUser.RefreshTokens.Count == 0)
        {
            return Problem("RefreshTokens collection is empty, no valid refresh tokens found");
        }
        
        if (appUser.RefreshTokens.Count != 1)
        {
            return Problem("More than one valid refresh token found.");
        }
        
        // Get claims manager of user
        var claimsPrincipal = await _signInManager.CreateUserPrincipalAsync(appUser);
        if (claimsPrincipal == null)
        {
            _logger.LogWarning("Could not get ClaimsPrincipal for user {}", userEmail);
            return Problem("User/Password problem.");
        }

        // New JWT token
        var jwt = IdentityExtensions.GenerateJwt(
            claimsPrincipal.Claims,
            _configuration["JWT:Key"],
            _configuration["JWT:Issuer"],
            _configuration["JWT:Issuer"],
            DateTime.Now.AddMinutes(_configuration.GetValue<int>("JWT:ExpireInMinutes"))
        );

        // New refresh token, invalidate old one (with a small delay)
        var refreshToken = appUser.RefreshTokens.First();
        if (refreshToken.Token == refreshTokenDto.RefreshToken)
        {
            refreshToken.PreviousToken = refreshToken.Token;
            refreshToken.PreviousTokenExpirationDateTime = DateTime.UtcNow.AddMinutes(1);

            refreshToken.Token = Guid.NewGuid().ToString();
            refreshToken.TokenExpirationDateTime = DateTime.UtcNow.AddDays(7);

            await _accounts.SaveChangesAsync();
        }

        var res = new JwtResponse()
        {
            Jwt = jwt,
            RefreshToken = refreshToken.Token
        };

        return Ok(res);
    }
    
    // TODO: maybe a logout endpoint 
    // so when user logs out token in use for the session can be invalidated, idk
}