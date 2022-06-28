using AutoMapper;
using Base.DAL;

namespace App.Public.DTO.Mappers;

public class ServiceMapper : BaseMapper<App.Public.DTO.Service, App.Domain.Service>
{
    public ServiceMapper(IMapper mapper) : base(mapper)
    {
    }
}