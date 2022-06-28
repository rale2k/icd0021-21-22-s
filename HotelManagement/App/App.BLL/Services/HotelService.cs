using App.Contracts.BLL.Services;
using App.Contracts.DAL;
using Base.BLL;
using Base.Contracts;

namespace App.BLL.Services;

public class HotelService : BaseEntityService<App.BLL.DTO.Hotel, App.DAL.DTO.Hotel, IHotelRepository>, IHotelService
{
    public HotelService(IHotelRepository repo, IMapper<App.BLL.DTO.Hotel, App.DAL.DTO.Hotel> mapper) : base(repo, mapper)
    {
    }
}