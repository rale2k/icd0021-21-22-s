using AutoMapper;
using Base.DAL;

namespace App.Public.DTO.Mappers;

public class ReadingMapper : BaseMapper<App.Public.DTO.Reading, App.Domain.Reading>
{
    public ReadingMapper(IMapper mapper) : base(mapper)
    {
    }
}