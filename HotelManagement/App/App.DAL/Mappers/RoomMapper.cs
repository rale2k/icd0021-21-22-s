using AutoMapper;
using Base.DAL;

namespace App.DAL.Mappers;

public class RoomMapper : BaseMapper<App.DAL.DTO.Room, App.Domain.Room>
{
    public RoomMapper(IMapper mapper) : base(mapper)
    {
    }
}