using AutoMapper;
using Base.DAL;

namespace App.DAL.Mappers;

public class AmenityMapper : BaseMapper<App.DAL.DTO.Amenity, App.Domain.Amenity>
{
    public AmenityMapper(IMapper mapper) : base(mapper)
    {
    }
}