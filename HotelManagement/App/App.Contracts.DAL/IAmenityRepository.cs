using App.DAL.DTO;
using Base.Contracts.DAL;

namespace App.Contracts.DAL;

public interface IAmenityRepository : IEntityRepository<Amenity>
{
    IEnumerable<Amenity?> GetHotelAmenities(Guid hotelId, bool noTracking = true);
    Task<IEnumerable<Amenity?>> GetHotelAmenitiesAsync(Guid hotelId, bool noTracking = true);
}