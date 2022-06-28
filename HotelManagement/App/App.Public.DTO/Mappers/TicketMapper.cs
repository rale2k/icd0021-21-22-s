using AutoMapper;
using Base.DAL;

namespace App.Public.DTO.Mappers;

public class TicketMapper : BaseMapper<App.Public.DTO.Ticket, App.BLL.DTO.Ticket>
{
    public TicketMapper(IMapper mapper) : base(mapper)
    {
    }
}