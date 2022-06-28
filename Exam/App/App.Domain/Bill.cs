using Base.Domain;

namespace App.Domain;

public class Bill: DomainEntityId
{
    public Guid ContractId { get; set; }
    public Contract? Contract { get; set; }

    public decimal AmountDue { get; set; }
    public bool Paid { get; set; }
}