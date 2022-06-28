using AutoMapper;
using Base.DAL;

namespace App.DAL.Mappers;

public class SectionMapper : BaseMapper<App.DAL.DTO.Section, App.Domain.Section>
{
    public SectionMapper(IMapper mapper) : base(mapper)
    {
    }
}