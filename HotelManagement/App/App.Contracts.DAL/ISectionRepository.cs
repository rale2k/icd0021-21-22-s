using App.DAL.DTO;
using Base.Contracts.DAL;

namespace App.Contracts.DAL;

public interface ISectionRepository : IEntityRepository<Section>
{
    bool IsHotelUserSection(Guid sectionId, Guid userId);
    IEnumerable<Section?> GetHotelSections(Guid sectionId, bool noTracking = true);
    Task<IEnumerable<Section?>> GetHotelSectionsAsync(Guid sectionId, bool noTracking = true);
}