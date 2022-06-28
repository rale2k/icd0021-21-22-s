using System.ComponentModel.DataAnnotations;
using Base.Domain;

namespace App.Public.DTO;

public class Client : DomainEntityId
{
    [MinLength(1)]
    [MaxLength(128)]
    public string FirstName { get; set; } = default!;
    
    [MinLength(1)]
    [MaxLength(128)]
    public string LastName { get; set; } = default!;

    [MinLength(1)]
    [MaxLength(320)]
    public string Email { get; set; } = default!;
    
    [MinLength(1)]
    [MaxLength(64)]
    public string PhoneNumber { get; set; } = default!;
}
