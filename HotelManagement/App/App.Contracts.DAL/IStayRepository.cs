using App.DAL.DTO;
using Base.Contracts.DAL;

namespace App.Contracts.DAL;

public interface IStayRepository : IEntityRepository<Stay>
{
    bool IsHotelUserStay(Guid stayId, Guid userId);
    IEnumerable<Stay?> GetAllRoomStays(Guid roomId, bool noTracking = true);
    Task<IEnumerable<Stay?>> GetAllRoomStaysAsync(Guid roomId, bool noTracking = true);
}