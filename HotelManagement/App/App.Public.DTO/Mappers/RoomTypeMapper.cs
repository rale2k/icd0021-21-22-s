using AutoMapper;
using Base.DAL;

namespace App.Public.DTO.Mappers;

public class RoomTypeMapper : BaseMapper<App.Public.DTO.RoomType, App.BLL.DTO.RoomType>
{
    public RoomTypeMapper(IMapper mapper) : base(mapper)
    {
    }
}