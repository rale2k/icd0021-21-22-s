using App.Contracts.DAL;
using App.DAL.DTO;
using Base.Contracts;
using Base.DAL.EF;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.Repositories;

public class UserHotelRepository : BaseEntityRepository<App.DAL.DTO.UserHotel, Domain.UserHotel, AppDbContext>, IUserHotelRepository
{
    public UserHotelRepository(AppDbContext dbContext, IMapper<DTO.UserHotel, Domain.UserHotel> mapper) : base(dbContext, mapper)
    {
    }
    
    public UserHotel? GetUserHotel(Guid hotelId, Guid userId, bool noTracking = true)
    {
        return RepoMapper.Map(CreateQuery(noTracking)
            .Include(u => u.Hotel)
            .FirstOrDefault(a => a.UserId.Equals(userId) && a.HotelId.Equals(hotelId)));
    }
    public async Task<UserHotel?> GetUserHotelAsync(Guid hotelId, Guid userId, bool noTracking = true)
    {
        return RepoMapper.Map(await CreateQuery(noTracking)
            .Include(u => u.Hotel)
            .FirstOrDefaultAsync(a => a.UserId.Equals(userId) && a.HotelId.Equals(hotelId)));
    }
    public IEnumerable<UserHotel> GetAllUserHotels(Guid userId, bool noTracking = true)
    {
        return CreateQuery(noTracking).Include(u => u.Hotel)
            .Where(h => h.UserId.Equals(userId))
            .ToList()
            .Select(e => RepoMapper.Map(e)!);
    }
    public async Task<IEnumerable<UserHotel>> GetAllUserHotelsAsync(Guid userId, bool noTracking = true)
    {
        return (await CreateQuery(noTracking).Include(u => u.Hotel)
                .Where(h => h.UserId.Equals(userId))
                .ToListAsync())
            .Select(e => RepoMapper.Map(e)!);
    }
    public IEnumerable<UserHotel?> GetAllHotelUsers(Guid hotelId, bool noTracking = true)
    {
        return CreateQuery().Where(e => e.HotelId == hotelId)
            .ToList()
            .Select(e => RepoMapper.Map(e));
    }
    public IEnumerable<UserHotel?> RemoveAllHotelUsers(Guid hotelId)
    {
        var res =  CreateQuery().Where(e => e.HotelId == hotelId)
            .ToList();
            
        RepoDbSet.RemoveRange(res);
        return res.Select(e => RepoMapper.Map(e));
    }
}