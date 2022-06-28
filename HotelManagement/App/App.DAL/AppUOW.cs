using App.Contracts.DAL;
using App.DAL.Mappers;
using App.DAL.Repositories;
using Base.DAL.EF;

namespace App.DAL;

public class AppUOW : BaseUnitOfWork<AppDbContext>, IAppUnitOfWork
{
    private readonly AutoMapper.IMapper _mapper;

    public AppUOW(AppDbContext dbContext, AutoMapper.IMapper mapper) : base(dbContext)
    {
        _mapper = mapper;
    }
    
    private IAmenityRepository? _amenities;
    private IClientRepository? _clients;
    private IHotelRepository? _hotels;
    private IUserHotelRepository? _userHotels;
    private IReservationRepository? _reservations;
    private IRoomRepository? _rooms;
    private IRoomTypeRepository? _roomTypes;
    private ISectionRepository? _sections;
    private IStayRepository? _stays;
    private ITicketRepository? _tickets;

    public virtual IAmenityRepository Amenities =>
        _amenities ??= new AmenityRepository(DbContext, new AmenityMapper(_mapper));
    
    public virtual IClientRepository Clients =>
        _clients ??= new ClientRepository(DbContext, new ClientMapper(_mapper));
    
    public virtual IHotelRepository Hotels =>
        _hotels ??= new HotelRepository(DbContext, new HotelMapper(_mapper));
    
    public virtual IUserHotelRepository UserHotels =>
        _userHotels ??= new UserHotelRepository(DbContext, new UserHotelMapper(_mapper));
    
    public virtual IReservationRepository Reservations =>
        _reservations ??= new ReservationRepository(DbContext, new ReservationMapper(_mapper));
    
    public virtual IRoomRepository Rooms =>
        _rooms ??= new RoomRepository(DbContext, new RoomMapper(_mapper));
    
    public virtual IRoomTypeRepository RoomTypes =>
        _roomTypes ??= new RoomTypeRepository(DbContext, new RoomTypeMapper(_mapper));

    public virtual ISectionRepository Sections =>
        _sections ??= new SectionRepository(DbContext, new SectionMapper(_mapper));
    
    public virtual IStayRepository Stays =>
        _stays ??= new StayRepository(DbContext, new StayMapper(_mapper));
    
    public virtual ITicketRepository Tickets =>
        _tickets ??= new TicketRepository(DbContext, new TicketMapper(_mapper));

}
