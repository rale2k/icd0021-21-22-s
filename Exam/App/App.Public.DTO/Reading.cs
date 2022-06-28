using Base.Domain;

namespace App.Public.DTO;

public class Reading: DomainEntityId
{
    public Guid ApartmentId { get; set; }
    
    public Guid ServiceId { get; set; }
    
    public decimal Value { get; set; }
    
    public DateTime PeriodStart { get; set; }
    public DateTime PeriodEnd { get; set; }
}