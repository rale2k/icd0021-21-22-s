using AutoMapper;
using Base.DAL;

namespace App.Public.DTO.Mappers;

public class StayMapper : BaseMapper<App.Public.DTO.Stay, App.BLL.DTO.Stay>
{
    public StayMapper(IMapper mapper) : base(mapper)
    {
    }
}