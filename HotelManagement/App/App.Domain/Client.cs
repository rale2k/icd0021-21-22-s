using System.ComponentModel.DataAnnotations;
using Base.Domain;

namespace App.Domain;

[Display(ResourceType = typeof(Resources.Domain.Entity.Name), Name = nameof(Client))]
public class Client : DomainEntityId
{
    [MaxLength(128)]
    [Display(ResourceType = typeof(Resources.Domain.Entity.Property), Name = nameof(FirstName))]
    public string FirstName { get; set; } = default!;

    [MaxLength(128)]
    [Display(ResourceType = typeof(Resources.Domain.Entity.Property), Name = nameof(LastName))]
    public string LastName { get; set; } = default!;

    [MaxLength(320)]
    [Display(ResourceType = typeof(Resources.Domain.Entity.Property), Name = nameof(Email))]
    public string Email { get; set; } = default!;
    
    [MaxLength(64)]
    [Display(ResourceType = typeof(Resources.Domain.Entity.Property), Name = nameof(PhoneNumber))]
    public string PhoneNumber { get; set; } = default!;
}
