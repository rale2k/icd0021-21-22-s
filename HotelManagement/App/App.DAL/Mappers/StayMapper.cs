using AutoMapper;
using Base.DAL;

namespace App.DAL.Mappers;

public class StayMapper : BaseMapper<App.DAL.DTO.Stay, App.Domain.Stay>
{
    public StayMapper(IMapper mapper) : base(mapper)
    {
    }
}