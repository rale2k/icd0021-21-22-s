using AutoMapper;
using Base.DAL;

namespace App.Public.DTO.Mappers;

public class ClientMapper : BaseMapper<App.Public.DTO.Client, App.BLL.DTO.Client>
{
    public ClientMapper(IMapper mapper) : base(mapper)
    {
    }
}