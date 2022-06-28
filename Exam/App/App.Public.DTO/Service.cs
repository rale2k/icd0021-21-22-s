using Base.Domain;

namespace App.Public.DTO;

public class Service: DomainEntityId
{
    public string Name { get; set; } = default!;
    public string ServiceType { get; set; } = default!;
    public decimal Price { get; set; }
}