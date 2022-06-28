using Base.Domain;

namespace App.Public.DTO;

public class Building : DomainEntityId
{
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;

    public string Address { get; set; } = default!;
    public int Floors { get; set; }
}