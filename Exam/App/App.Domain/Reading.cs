using Base.Domain;

namespace App.Domain;

public class Reading: DomainEntityId
{
    public Guid ApartmentId { get; set; }
    public Apartment? Apartment { get; set; }
    
    public Guid ServiceId { get; set; }
    public Service? Service { get; set; }
    
    public decimal Value { get; set; }
    
    public DateTime PeriodStart { get; set; }
    public DateTime PeriodEnd { get; set; }
}