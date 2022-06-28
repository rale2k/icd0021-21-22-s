using AutoMapper;

namespace App.BLL;

public class AutoMapperConfig : Profile
{
    public AutoMapperConfig()
    {
        CreateMap<DTO.Amenity, DAL.DTO.Amenity>().ReverseMap();
        CreateMap<DTO.Client, DAL.DTO.Client>().ReverseMap();
        CreateMap<DTO.Hotel, DAL.DTO.Hotel>().ReverseMap();
        CreateMap<DTO.Reservation, DAL.DTO.Reservation>().ReverseMap();
        CreateMap<DTO.Room, DAL.DTO.Room>().ReverseMap();
        CreateMap<DTO.RoomType, DAL.DTO.RoomType>().ReverseMap();
        CreateMap<DTO.Section, DAL.DTO.Section>().ReverseMap();
        CreateMap<DTO.Stay, DAL.DTO.Stay>().ReverseMap();
        CreateMap<DTO.Ticket, DAL.DTO.Ticket>().ReverseMap();
        CreateMap<DTO.UserHotel, DAL.DTO.UserHotel>().ReverseMap();
    }
}