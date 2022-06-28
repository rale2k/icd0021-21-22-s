using App.BLL.DTO;
using Base.Contracts.BLL;

namespace App.Contracts.BLL.Services;

public interface IAmenityService : IEntityService<Amenity>
{
    IEnumerable<Amenity?> GetHotelAmenities(Guid hotelId, bool noTracking = true);
    Task<IEnumerable<Amenity?>> GetHotelAmenitiesAsync(Guid hotelId, bool noTracking = true);

}