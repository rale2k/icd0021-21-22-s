using System.ComponentModel.DataAnnotations;

namespace App.Public.DTO.Identity;

public class Register
{
    [StringLength(maximumLength:128, MinimumLength = 5, ErrorMessage = "Email length must be in range 1-128")] 
    public string Email { get; set; } = default!;
    
    [StringLength(maximumLength:128, MinimumLength = 1, ErrorMessage = "Password length must be in range 1-128")] 
    public string Password { get; set; } = default!;


    [StringLength(maximumLength:128, MinimumLength = 1, ErrorMessage = "First name length must be in range 1-128")] 
    public string FirstName { get; set; } = default!;

    [StringLength(maximumLength:128, MinimumLength = 1, ErrorMessage = "Last name length must be in range 1-128")] 
    public string LastName { get; set; } = default!;
}