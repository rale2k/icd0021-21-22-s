using Base.Domain;

namespace App.Public.DTO;

public class Contract: DomainEntityId
{
    public Guid UserId { get; set; }
    
    public Guid ApartmentId { get; set; }
    
    public decimal Rent { get; set; }
    
    public DateTime PeriodStart { get; set; }
    public DateTime PeriodEnd { get; set; }
    
    public ICollection<Service>? Services { get; set; }
}