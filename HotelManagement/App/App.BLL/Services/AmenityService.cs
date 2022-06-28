using App.BLL.DTO;
using App.Contracts.BLL.Services;
using App.Contracts.DAL;
using Base.BLL;
using Base.Contracts;

namespace App.BLL.Services;

public class AmenityService : BaseEntityService<App.BLL.DTO.Amenity, App.DAL.DTO.Amenity, IAmenityRepository>, IAmenityService
{
    public AmenityService(IAmenityRepository repo, IMapper<App.BLL.DTO.Amenity, App.DAL.DTO.Amenity> mapper) : base(repo, mapper)
    {
    }

    public IEnumerable<Amenity?> GetHotelAmenities(Guid hotelId, bool noTracking = true)
    {
        return Repository.GetHotelAmenities(hotelId)
            .Select(e => Mapper.Map(e))
            .ToList();
    }

    public async Task<IEnumerable<Amenity?>> GetHotelAmenitiesAsync(Guid hotelId, bool noTracking = true)
    {
        return (await Repository.GetHotelAmenitiesAsync(hotelId))
            .Select(e => Mapper.Map(e))
            .ToList();
    }
}