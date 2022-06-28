using AutoMapper;

namespace App.Public.DTO.Mappers;

public class AutoMapperConfig : Profile
{
    public AutoMapperConfig()
    {
        CreateMap<App.Public.DTO.Amenity, App.Domain.Amenity>().ReverseMap();
        CreateMap<App.Public.DTO.Apartment, App.Domain.Apartment>().ReverseMap();
        CreateMap<App.Public.DTO.Bill, App.Domain.Bill>().ReverseMap();
        CreateMap<App.Public.DTO.Building, App.Domain.Building>().ReverseMap();
        CreateMap<App.Public.DTO.Contract, App.Domain.Contract>().ReverseMap();
        CreateMap<App.Public.DTO.Reading, App.Domain.Reading>().ReverseMap();
        CreateMap<App.Public.DTO.Service, App.Domain.Service>().ReverseMap();

    }
}