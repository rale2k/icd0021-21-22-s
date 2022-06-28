using Base.Domain;

namespace App.Domain;

public class Service: DomainEntityId
{
    public string Name { get; set; } = default!;
    public EServiceType ServiceType { get; set; }
    public decimal Price { get; set; }
    
    public ICollection<Contract>? Contracts { get; set; }
}