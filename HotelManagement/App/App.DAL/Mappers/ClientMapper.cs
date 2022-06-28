using AutoMapper;
using Base.DAL;

namespace App.DAL.Mappers;

public class ClientMapper : BaseMapper<App.DAL.DTO.Client, App.Domain.Client>
{
    public ClientMapper(IMapper mapper) : base(mapper)
    {
    }
}