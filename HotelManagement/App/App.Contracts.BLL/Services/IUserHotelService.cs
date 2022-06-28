using App.BLL.DTO;
using Base.Contracts.BLL;

namespace App.Contracts.BLL.Services;

public interface IUserHotelService : IEntityService<UserHotel>
{
    UserHotel? GetUserHotel(Guid hotelId, Guid userId, bool noTracking = true);
    Task<UserHotel?> GetUserHotelAsync(Guid hotelId, Guid userId, bool noTracking = true);
    Task<IEnumerable<UserHotel>> GetAllUserHotelsAsync(Guid userId, bool noTracking = true);
    UserHotel? Remove(Guid userId, Guid hotelId);
    IEnumerable<UserHotel?> RemoveAllHotelUsers(Guid hotelId);
    bool IsHotelUser(Guid hotelId, Guid userId);
}