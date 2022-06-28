using AutoMapper;
using Base.DAL;

namespace App.BLL.Mappers;

public class RoomTypeMapper : BaseMapper<App.BLL.DTO.RoomType, App.DAL.DTO.RoomType>
{
    public RoomTypeMapper(IMapper mapper) : base(mapper)
    {
    }
}