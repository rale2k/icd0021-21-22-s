using AutoMapper;
using Base.DAL;

namespace App.Public.DTO.Mappers;

public class ContractMapper : BaseMapper<App.Public.DTO.Contract, App.Domain.Contract>
{
    public ContractMapper(IMapper mapper) : base(mapper)
    {
    }
}