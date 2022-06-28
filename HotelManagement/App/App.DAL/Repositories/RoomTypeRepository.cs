using App.Contracts.DAL;
using App.Domain;
using Base.Contracts;
using Base.DAL.EF;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.Repositories;

public class RoomTypeRepository : BaseEntityRepository<App.DAL.DTO.RoomType, Domain.RoomType, AppDbContext>, IRoomTypeRepository
{
    public RoomTypeRepository(AppDbContext dbContext, IMapper<DTO.RoomType, Domain.RoomType> mapper) : base(dbContext, mapper)
    {
    }

    protected override IQueryable<RoomType> CreateQuery(bool noTracking = true)
    {
        return base.CreateQuery(noTracking).Include(e => e.Amenities);
    }

    public override DTO.RoomType Add(DTO.RoomType entity)
    {
        Domain.RoomType domainRoomType = RepoMapper.Map(entity)!;

        domainRoomType.Amenities = domainRoomType.Amenities!.Select(e =>
            {
                var amentity = RepoDbContext.Amenities.FirstOrDefault(f => f == e);
                return amentity;
            })
            .Where(e => e != null && e.HotelId == domainRoomType.HotelId)
            .ToList()!;
        
        return RepoMapper.Map(RepoDbSet.Add(domainRoomType).Entity)!;
    }
    
    // .Where(e => e != null && e.HotelId == domainRoomType.HotelId)
    //
    // Adding nonexistant amenities or amenities of another hotel doesn't
    // cause any errors but they silently just don't get added instead.
    
    public override DTO.RoomType Update(DTO.RoomType entity)
    {
        var domainRoomtype = CreateQuery()
            .First(x => x.Id == entity.Id);

        var updatedRoomType = RepoMapper.Map(entity)!;
        updatedRoomType.Name = domainRoomtype.Name;
        updatedRoomType.Name.SetTranslation(entity.Name);
        updatedRoomType.Description = domainRoomtype.Description;
        updatedRoomType.Description.SetTranslation(entity.Description);
        
        // ghetto many-to-many relation update
        // removing all existing m-m relations
        foreach (var domainEntityAmenity in domainRoomtype.Amenities!)
        {
            RepoDbContext.Remove(domainEntityAmenity);
        }
        // and adding new ones
        updatedRoomType.Amenities = updatedRoomType.Amenities!.Select(e =>
            {
                var amentity = RepoDbContext.Amenities.FirstOrDefault(f => f == e);
                return amentity;
            })
            .Where(e => e != null && e.HotelId == updatedRoomType.HotelId)
            .ToList()!;
        
        // domainEntity is queried as noTracking, but (probably) when the ghetto relation 
        // update is done it becomes tracked again as it's amenities get loaded.
        // So it should be explcitly detached again
        RepoDbContext.Entry(domainRoomtype).State = EntityState.Detached;
        
        var updatedEntity = RepoDbSet.Update(updatedRoomType!).Entity;
        return RepoMapper.Map(updatedEntity)!;
    }
    public IEnumerable<DTO.RoomType?> GetHotelRoomTypes(Guid hotelId, bool noTracking = true)
    {
        return CreateQuery().Where(e => e.HotelId == hotelId)
            .Select(e => RepoMapper.Map(e))
            .ToList();
    }

    public async Task<IEnumerable<DTO.RoomType?>> GetHotelRoomTypesAsync(Guid hotelId, bool noTracking = true)
    {
        return await CreateQuery().Where(e => e.HotelId == hotelId)
            .Select(e => RepoMapper.Map(e))
            .ToListAsync();
    }

}