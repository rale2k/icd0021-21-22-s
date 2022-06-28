using App.Contracts.BLL.Services;
using App.Contracts.DAL;
using Base.BLL;
using Base.Contracts;

namespace App.BLL.Services;

public class SectionService : BaseEntityService<App.BLL.DTO.Section, App.DAL.DTO.Section, ISectionRepository>, ISectionService
{
    public SectionService(ISectionRepository repo, IMapper<App.BLL.DTO.Section, App.DAL.DTO.Section> mapper) : base(repo, mapper)
    {
    }

    public bool IsHotelUserSection(Guid sectionId, Guid userId)
    {
        return Repository.IsHotelUserSection(sectionId, userId);
    }

    public IEnumerable<App.BLL.DTO.Section?> GetHotelSections(Guid hotelId, bool noTracking = true)
    {
        return Repository.GetHotelSections(hotelId, noTracking)
            .Select(e => Mapper.Map(e));
    }

    public async Task<IEnumerable<App.BLL.DTO.Section?>> GetHotelSectionsAsync(Guid hotelId, bool noTracking = true)
    {
        return (await Repository.GetHotelSectionsAsync(hotelId, noTracking))
            .Select(e => Mapper.Map(e));
    }

}