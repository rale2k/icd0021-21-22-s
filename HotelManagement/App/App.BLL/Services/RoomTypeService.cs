using App.Contracts.BLL.Services;
using App.Contracts.DAL;
using Base.BLL;
using Base.Contracts;

namespace App.BLL.Services;

public class RoomTypeService : BaseEntityService<App.BLL.DTO.RoomType, App.DAL.DTO.RoomType, IRoomTypeRepository>, IRoomTypeService
{
    public RoomTypeService(IRoomTypeRepository repo, IMapper<App.BLL.DTO.RoomType, App.DAL.DTO.RoomType> mapper): base(repo, mapper)
    {
    }

    public IEnumerable<DTO.RoomType?> GetHotelRoomTypes(Guid hotelId, bool noTracking = true)
    {
        return Repository.GetHotelRoomTypes(hotelId)
            .Select(e => Mapper.Map(e))
            .ToList();
    }

    public async Task<IEnumerable<DTO.RoomType?>> GetHotelRoomTypesAsync(Guid hotelId, bool noTracking = true)
    {
        return (await Repository.GetHotelRoomTypesAsync(hotelId))
            .Select(e => Mapper.Map(e))
            .ToList();
    }

}