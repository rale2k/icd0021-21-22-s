using AutoMapper;
using Base.DAL;

namespace App.Public.DTO.Mappers;

public class BuildingMapper : BaseMapper<App.Public.DTO.Building, App.Domain.Building>
{
    public BuildingMapper(IMapper mapper) : base(mapper)
    {
    }
}