using AutoMapper;
using Base.DAL;

namespace App.Public.DTO.Mappers;

public class ApartmentMapper : BaseMapper<App.Public.DTO.Apartment, App.Domain.Apartment>
{
    public ApartmentMapper(IMapper mapper) : base(mapper)
    {
    }
}