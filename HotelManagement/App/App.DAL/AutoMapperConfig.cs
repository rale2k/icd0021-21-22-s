using AutoMapper;

namespace App.DAL;

public class AutoMapperConfig : Profile
{
    public AutoMapperConfig()
    {
        CreateMap<DTO.Amenity, Domain.Amenity>().ReverseMap();
        CreateMap<DTO.Client, Domain.Client>().ReverseMap();
        CreateMap<DTO.Hotel, Domain.Hotel>().ReverseMap();
        CreateMap<DTO.Reservation, Domain.Reservation>().ReverseMap();
        CreateMap<DTO.Room, Domain.Room>().ReverseMap();
        CreateMap<DTO.RoomType, Domain.RoomType>().ReverseMap();
        CreateMap<DTO.Section, Domain.Section>().ReverseMap();
        CreateMap<DTO.Stay, Domain.Stay>().ReverseMap();
        CreateMap<DTO.Ticket, Domain.Ticket>().ReverseMap();
        CreateMap<DTO.UserHotel, Domain.UserHotel>().ReverseMap();
    }
}