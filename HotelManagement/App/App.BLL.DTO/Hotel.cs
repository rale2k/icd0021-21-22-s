using Base.Domain;

namespace App.BLL.DTO;

public class Hotel : DomainEntityId
{    
    public string Name { get; set; } = default!;
    
    public string Description { get; set; } = default!;
    
    public ICollection<Section>? HotelSections { get; set; }
}