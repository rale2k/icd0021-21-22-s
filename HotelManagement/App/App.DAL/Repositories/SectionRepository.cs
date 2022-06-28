using App.Contracts.DAL;
using App.DAL.DTO;
using Base.Contracts;
using Base.DAL.EF;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.Repositories;

public class SectionRepository : BaseEntityRepository<App.DAL.DTO.Section, Domain.Section, AppDbContext>, ISectionRepository
{
    public SectionRepository(AppDbContext dbContext, IMapper<DTO.Section, Domain.Section> mapper) : base(dbContext, mapper)
    {
    }

    public bool IsHotelUserSection(Guid sectionId, Guid userId)
    {
        var section = CreateQuery()
            .FirstOrDefault(e => e.Id == sectionId);
        
        if (section != null)
        {
            return RepoDbContext.UserHotels.Any(e => e.HotelId == section!.HotelId && e.UserId == userId);
        }

        return false;
    }

    public IEnumerable<Section?> GetHotelSections(Guid hotelId, bool noTracking = true)
    {
        return CreateQuery(noTracking).Where(e => e.HotelId == hotelId)
            .Select(e => RepoMapper.Map(e))
            .ToList();
    }

    public async Task<IEnumerable<Section?>> GetHotelSectionsAsync(Guid hotelId, bool noTracking = true)
    {
        return await CreateQuery(noTracking).Where(e => e.HotelId == hotelId)
            .Select(e => RepoMapper.Map(e))
            .ToListAsync();
    }
}