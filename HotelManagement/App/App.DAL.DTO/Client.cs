using Base.Domain;

namespace App.DAL.DTO;

public class Client : DomainEntityId
{
    public string FirstName { get; set; } = default!;

    public string LastName { get; set; } = default!;

    public string Email { get; set; } = default!;
    
    public string PhoneNumber { get; set; } = default!;
}
