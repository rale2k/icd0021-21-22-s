namespace App.Public.DTO.Identity;

public class JwtResponse
{
    public string Jwt { get; set; } = default!;
    public string RefreshToken { get; set; } = default!;
}