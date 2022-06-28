using App.Contracts.DAL;
using App.DAL.DTO;
using Base.Contracts;
using Base.DAL.EF;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.Repositories;

public class AmenityRepository : BaseEntityRepository<App.DAL.DTO.Amenity, Domain.Amenity, AppDbContext>, IAmenityRepository
{
    public AmenityRepository(AppDbContext dbContext, IMapper<DTO.Amenity, Domain.Amenity> mapper) : base(dbContext, mapper)
    {
    }
    
    public override DTO.Amenity Update(DTO.Amenity entity)
    {
        var domainEntity = RepoDbSet.AsNoTracking()
            .First(x => x.Id == entity.Id);

        var newEntity = RepoMapper.Map(entity)!;
        newEntity.Name = domainEntity.Name;
        newEntity.Name.SetTranslation(entity.Name);
        newEntity.Description = domainEntity.Description;
        newEntity.Description.SetTranslation(entity.Description);
            
        var updatedEntity = RepoDbSet.Update(newEntity!).Entity;
        return RepoMapper.Map(updatedEntity)!;
    }

    public IEnumerable<Amenity?> GetHotelAmenities(Guid hotelId, bool noTracking = true)
    {
        return CreateQuery().Where(e => e.HotelId == hotelId)
            .Select(e => RepoMapper.Map(e))
            .ToList();
    }

    public async Task<IEnumerable<Amenity?>> GetHotelAmenitiesAsync(Guid hotelId, bool noTracking = true)
    {
        return await CreateQuery().Where(e => e.HotelId == hotelId)
            .Select(e => RepoMapper.Map(e))
            .ToListAsync();
    }
}