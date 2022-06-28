using AutoMapper;
using Base.DAL;

namespace App.BLL.Mappers;

public class StayMapper : BaseMapper<App.BLL.DTO.Stay, App.DAL.DTO.Stay>
{
    public StayMapper(IMapper mapper) : base(mapper)
    {
    }
}