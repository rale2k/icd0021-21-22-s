using AutoMapper;
using Base.DAL;

namespace App.BLL.Mappers;

public class AmenityMapper : BaseMapper<App.BLL.DTO.Amenity, App.DAL.DTO.Amenity>
{
    public AmenityMapper(IMapper mapper) : base(mapper)
    {
    }
}