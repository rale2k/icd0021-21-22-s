using App.BLL.Mappers;
using App.BLL.Services;
using App.Contracts.BLL;
using App.Contracts.BLL.Services;
using App.Contracts.DAL;
using Base.BLL;

namespace App.BLL;

public class AppBll : BaseBll<IAppUnitOfWork>, IAppBll
{
    private readonly AutoMapper.IMapper _mapper;

    public AppBll(IAppUnitOfWork uow, AutoMapper.IMapper mapper): base(uow)
    {
        _mapper = mapper;
    }
    
    public override async Task<int> SaveChangesAsync()
    {
        return await Uow.SaveChangesAsync();
    }

    public override int SaveChanges()
    {
        return Uow.SaveChanges();
    }
    
    private IAmenityService? _amenities;
    private IClientService? _clients;
    private IHotelService? _hotels;
    private IUserHotelService? _userHotels;
    private IReservationService? _reservations;
    private IRoomService? _rooms;
    private IRoomTypeService? _roomTypes;
    private ISectionService? _sections;
    private IStayService? _stays;
    private ITicketService? _tickets;

    public virtual IAmenityService Amenities =>
        _amenities ??= new AmenityService(Uow.Amenities, new AmenityMapper(_mapper));
    
    public virtual IClientService Clients =>
        _clients ??= new ClientService(Uow.Clients, new ClientMapper(_mapper));
    
    public virtual IHotelService Hotels =>
        _hotels ??= new HotelService(Uow.Hotels, new HotelMapper(_mapper));
    
    public virtual IUserHotelService UserHotels =>
        _userHotels ??= new UserHotelService(Uow.UserHotels, new UserHotelMapper(_mapper));
    
    public virtual IReservationService Reservations =>
        _reservations ??= new ReservationService(Uow.Reservations, new ReservationMapper(_mapper));
    
    public virtual IRoomService Rooms =>
        _rooms ??= new RoomService(Uow.Rooms, new RoomMapper(_mapper));
    
    public virtual IRoomTypeService RoomTypes =>
        _roomTypes ??= new RoomTypeService(Uow.RoomTypes, new RoomTypeMapper(_mapper));

    public virtual ISectionService Sections =>
        _sections ??= new SectionService(Uow.Sections, new SectionMapper(_mapper));
    
    public virtual IStayService Stays =>
        _stays ??= new StayService(Uow.Stays, new StayMapper(_mapper));
    
    public virtual ITicketService Tickets =>
        _tickets ??= new TicketService(Uow.Tickets, new TicketMapper(_mapper));
    
}
