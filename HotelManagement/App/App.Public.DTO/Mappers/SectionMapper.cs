using AutoMapper;
using Base.DAL;

namespace App.Public.DTO.Mappers;

public class SectionMapper : BaseMapper<App.Public.DTO.Section, App.BLL.DTO.Section>
{
    public SectionMapper(IMapper mapper) : base(mapper)
    {
    }
}