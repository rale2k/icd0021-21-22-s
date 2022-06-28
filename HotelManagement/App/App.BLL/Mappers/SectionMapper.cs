using AutoMapper;
using Base.DAL;

namespace App.BLL.Mappers;

public class SectionMapper : BaseMapper<App.BLL.DTO.Section, App.DAL.DTO.Section>
{
    public SectionMapper(IMapper mapper) : base(mapper)
    {
    }
}