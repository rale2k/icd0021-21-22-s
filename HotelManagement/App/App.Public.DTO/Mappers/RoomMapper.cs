using AutoMapper;
using Base.DAL;

namespace App.Public.DTO.Mappers;

public class RoomMapper : BaseMapper<App.Public.DTO.Room, App.BLL.DTO.Room>
{
    public RoomMapper(IMapper mapper) : base(mapper)
    {
    }
}