using AutoMapper;
using Base.DAL;

namespace App.BLL.Mappers;

public class RoomMapper : BaseMapper<App.BLL.DTO.Room, App.DAL.DTO.Room>
{
    public RoomMapper(IMapper mapper) : base(mapper)
    {
    }
}