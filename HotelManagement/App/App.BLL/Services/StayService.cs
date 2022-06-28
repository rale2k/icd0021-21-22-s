using App.BLL.DTO;
using App.Contracts.BLL.Services;
using App.Contracts.DAL;
using Base.BLL;
using Base.Contracts;

namespace App.BLL.Services;

public class StayService : BaseEntityService<App.BLL.DTO.Stay, App.DAL.DTO.Stay, IStayRepository>, IStayService
{
    public StayService(IStayRepository repo, IMapper<App.BLL.DTO.Stay, App.DAL.DTO.Stay> mapper) : base(repo, mapper)
    {
    }

    public bool IsHotelUserStay(Guid stayId, Guid userId)
    {
        return Repository.IsHotelUserStay(stayId, userId);
    }

    public IEnumerable<Stay?> GetAllRoomStays(Guid roomId, bool noTracking = true)
    {
        return Repository.GetAllRoomStays(roomId, noTracking)
            .Select(e => Mapper.Map(e))
            .ToList();
    }

    public async Task<IEnumerable<Stay?>> GetAllRoomStaysAsync(Guid roomId, bool noTracking = true)
    {
        return (await Repository.GetAllRoomStaysAsync(roomId, noTracking))
            .Select(e => Mapper.Map(e))
            .ToList();
    }
}