using AutoMapper;
using Base.DAL;

namespace App.Public.DTO.Mappers;

public class AmenityMapper : BaseMapper<App.Public.DTO.Amenity, App.Domain.Amenity>
{
    public AmenityMapper(IMapper mapper) : base(mapper)
    {
    }
}