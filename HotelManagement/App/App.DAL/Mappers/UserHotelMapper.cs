using AutoMapper;
using Base.DAL;

namespace App.DAL.Mappers;

public class UserHotelMapper : BaseMapper<App.DAL.DTO.UserHotel, App.Domain.UserHotel>
{
    public UserHotelMapper(IMapper mapper) : base(mapper)
    {
    }
}