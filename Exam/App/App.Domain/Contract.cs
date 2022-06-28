using App.Domain.Identity;
using Base.Domain;

namespace App.Domain;

public class Contract: DomainEntityId
{
    public Guid UserId { get; set; }
    public AppUser? User { get; set; }
    
    public Guid ApartmentId { get; set; }
    public Apartment? Apartment { get; set; }
    
    public decimal Rent { get; set; }
    
    public DateTime PeriodStart { get; set; }
    public DateTime PeriodEnd { get; set; }
    
    public ICollection<Service>? Services { get; set; }
}