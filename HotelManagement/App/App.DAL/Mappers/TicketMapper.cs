using AutoMapper;
using Base.DAL;

namespace App.DAL.Mappers;

public class TicketMapper : BaseMapper<App.DAL.DTO.Ticket, App.Domain.Ticket>
{
    public TicketMapper(IMapper mapper) : base(mapper)
    {
    }
}