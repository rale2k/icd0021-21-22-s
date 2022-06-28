using App.BLL.DTO;
using App.Contracts.BLL.Services;
using App.Contracts.DAL;
using Base.BLL;
using Base.Contracts;

namespace App.BLL.Services;

public class UserHotelService : BaseEntityService<App.BLL.DTO.UserHotel, App.DAL.DTO.UserHotel, IUserHotelRepository>, IUserHotelService
{
    public UserHotelService(IUserHotelRepository repo, IMapper<App.BLL.DTO.UserHotel, App.DAL.DTO.UserHotel> mapper) : base(repo, mapper)
    {
    }
    
    public UserHotel? GetUserHotel(Guid hotelId, Guid userId, bool noTracking = true)
    {
        return Mapper.Map(Repository.GetUserHotel(hotelId, userId, noTracking));
    }
    public async Task<UserHotel?> GetUserHotelAsync(Guid hotelId, Guid userId, bool noTracking = true)
    {
        return Mapper.Map(await Repository.GetUserHotelAsync(hotelId, userId, noTracking));
    }
    public IEnumerable<UserHotel> GetAllUserHotels(Guid userId, bool noTracking = true)
    {
        return Repository.GetAllUserHotels(userId, noTracking)
            .Select(e => Mapper.Map(e)!);
    }
    public async Task<IEnumerable<UserHotel>> GetAllUserHotelsAsync(Guid userId, bool noTracking = true)
    {
        return (await Repository.GetAllUserHotelsAsync(userId, noTracking))
            .Select(e => Mapper.Map(e)!);
    }
    public UserHotel? Remove(Guid userId, Guid hotelId)
    {
        return Remove(GetUserHotel(hotelId, userId)!);
    }
    public IEnumerable<UserHotel?> RemoveAllHotelUsers(Guid hotelId)
    {
        return Repository.RemoveAllHotelUsers(hotelId).Select(e => Mapper.Map(e));
    }

    public bool IsHotelUser(Guid hotelId, Guid userId)
    {
        var res = GetUserHotel(hotelId, userId);

        return res != null;
    }
}