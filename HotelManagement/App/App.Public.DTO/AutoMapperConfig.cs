using AutoMapper;

namespace App.Public.DTO;

public class AutoMapperConfig : Profile
{
    public AutoMapperConfig()
    {
        CreateMap<App.Public.DTO.Amenity, App.BLL.DTO.Amenity>().ReverseMap();
        CreateMap<App.Public.DTO.Client, App.BLL.DTO.Client>().ReverseMap();
        CreateMap<App.Public.DTO.Hotel, App.BLL.DTO.Hotel>().ReverseMap();
        CreateMap<App.Public.DTO.Reservation, App.BLL.DTO.Reservation>().ReverseMap();
        CreateMap<App.Public.DTO.Room, App.BLL.DTO.Room>().ReverseMap();
        CreateMap<App.Public.DTO.RoomType, App.BLL.DTO.RoomType>().ReverseMap();
        CreateMap<App.Public.DTO.Section, App.BLL.DTO.Section>().ReverseMap();
        CreateMap<App.Public.DTO.Stay, App.BLL.DTO.Stay>().ReverseMap();
        CreateMap<App.Public.DTO.Ticket, App.BLL.DTO.Ticket>().ReverseMap();
        CreateMap<App.Public.DTO.UserHotel, App.BLL.DTO.UserHotel>().ReverseMap();
    }
}