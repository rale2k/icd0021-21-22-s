using App.DAL.DTO;
using Base.Contracts.DAL;

namespace App.Contracts.DAL;

public interface IUserHotelRepository : IEntityRepository<UserHotel>
{
    UserHotel? GetUserHotel(Guid hotelId, Guid userId, bool noTracking = true);
    Task<UserHotel?> GetUserHotelAsync(Guid hotelId, Guid userId, bool noTracking = true);
    IEnumerable<UserHotel> GetAllUserHotels(Guid userId, bool noTracking = true);
    Task<IEnumerable<UserHotel>> GetAllUserHotelsAsync(Guid userId, bool noTracking = true);
    IEnumerable<UserHotel?> GetAllHotelUsers(Guid hotelId, bool noTracking = true);
    IEnumerable<UserHotel?> RemoveAllHotelUsers(Guid hotelId);
}