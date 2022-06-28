using App.BLL.DTO;
using Base.Contracts.BLL;

namespace App.Contracts.BLL.Services;

public interface ISectionService : IEntityService<Section>
{
    bool IsHotelUserSection(Guid sectionId, Guid userId);
    IEnumerable<Section?> GetHotelSections(Guid hotelId, bool noTracking = true);
    Task<IEnumerable<Section?>> GetHotelSectionsAsync(Guid hotelId, bool noTracking = true);
}