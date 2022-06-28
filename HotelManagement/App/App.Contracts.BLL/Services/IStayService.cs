using App.BLL.DTO;
using Base.Contracts.BLL;

namespace App.Contracts.BLL.Services;

public interface IStayService : IEntityService<Stay>
{
    bool IsHotelUserStay(Guid stayId, Guid userId);
    IEnumerable<Stay?> GetAllRoomStays(Guid roomId, bool noTracking = true);
    Task<IEnumerable<Stay?>> GetAllRoomStaysAsync(Guid roomId, bool noTracking = true);
}