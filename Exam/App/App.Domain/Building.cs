using Base.Domain;

namespace App.Domain;

public class Building : DomainEntityId
{
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;

    public string Address { get; set; } = default!;
    public int Floors { get; set; }
    
    public ICollection<Apartment>? Apartments { get; set; }
}