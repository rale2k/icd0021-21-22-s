using App.BLL;
using App.Contracts.BLL;
using App.DAL;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Xunit.Abstractions;

namespace Tests.WebApp.Tests.Unit;

public class HotelServiceTests
{
    private readonly IAppBll _appBll;
    private readonly ITestOutputHelper _testOutputHelper;
    private readonly AppDbContext _ctx;

    // ARRANGE - common
    public HotelServiceTests(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;

        // set up db context for testing - using InMemory db engine
        var optionBuilder = new DbContextOptionsBuilder<AppDbContext>();
        optionBuilder.EnableSensitiveDataLogging(true);
        // provide new random database name here
        // or parallel test methods will conflict each other
        optionBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
        _ctx = new AppDbContext(optionBuilder.Options);
        _ctx.Database.EnsureDeleted();
        _ctx.Database.EnsureCreated();
        _ctx.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        _ctx.ChangeTracker.AutoDetectChangesEnabled = false;
        var dalMapperCfg = GetDalMapperConfiguration();
        var bllMapperCfg = GetBllMapperConfiguration();

        using var loggerFactory = LoggerFactory.Create(builder => builder.AddConsole());

        _appBll = new AppBll(new AppUOW(_ctx, new Mapper(dalMapperCfg)), new Mapper(bllMapperCfg));
    }

        [Fact]
        public async Task Action_Test__Add()
        {
            // ARRANGE
            var hotel = new App.BLL.DTO.Hotel()
            {
                Name = "Testing_Hotel",
                Description = "Testing_Hotel_Description",
                Id = default!
            };
            
            // ACT
            var newHotel = _appBll.Hotels.Add(hotel);
            await _appBll.SaveChangesAsync();
            
            // Assert
            var res = _ctx.Hotels.FirstOrDefault(e => e.Id == newHotel.Id);
            
            Assert.NotNull(res);
            Assert.Equal(res!.Name, hotel.Name);
            Assert.Equal(res.Description, hotel.Description);
            Assert.Equal(res.Id, newHotel.Id);
        }
        
        [Fact]
        public async Task Action_Test__Update()
        {
            // ARRANGE
            var hotel = _ctx.Hotels.Add(new App.Domain.Hotel()
            {
                Name = "Testing_Hotel",
                Description = "Testing_Hotel_Description",
                Id = default!
            });

            var updatedHotel = new App.BLL.DTO.Hotel()
            {
                Name = "Updated_Testing_Hotel",
                Description = "Updated_Testing_Hotel_Description",
                Id = hotel.Entity.Id
            };
            
            await _ctx.SaveChangesAsync();
            hotel.State = EntityState.Detached;

            // ACT
            var newHotel = _appBll.Hotels.Update(updatedHotel);
            await _appBll.SaveChangesAsync();
            
            // Assert
            var h = _ctx.Hotels.FirstOrDefault(e => e.Id == hotel.Entity.Id);
            
            Assert.Equal(h!.Name, updatedHotel.Name);
            Assert.Equal(h.Description, updatedHotel.Description);
            Assert.Equal(h.Id, updatedHotel.Id);
            
            Assert.Equal(h.Name, newHotel.Name);
            Assert.Equal(h.Description, newHotel.Description);
            Assert.Equal(h.Id, newHotel.Id);
        }
        
        
        [Fact]
        public async Task Action_Test__Remove()
        {
            // ARRANGE
            var hotel = _ctx.Hotels.Add(new App.Domain.Hotel()
            {
                Name = "Testing_Hotel",
                Description = "Testing_Hotel_Description",
                Id = default!
            });
            
            await _ctx.SaveChangesAsync();
            hotel.State = EntityState.Detached;

            // ACT
            await _appBll.Hotels.RemoveAsync(hotel.Entity.Id);
            await _appBll.SaveChangesAsync();
            
            // Assert
            Assert.Null(_ctx.Hotels.FirstOrDefault(e => e.Id == hotel.Entity.Id));
        }

        [Fact]
        public async Task Action_Test__FirstOrDefault()
        {
            // ARRANGE
            var hotel = _ctx.Hotels.Add(new App.Domain.Hotel()
            {
                Name = "Testing_Hotel",
                Description = "Testing_Hotel_Description",
                Id = default!
            });
            
            await _ctx.SaveChangesAsync();
            
            // ACT
            var res = await _appBll.Hotels.FirstOrDefaultAsync(hotel.Entity.Id);
            
            // Assert
            Assert.NotNull(res);
            Assert.Equal(hotel.Entity.Id, res!.Id);
        }

        [Fact]
        public async Task Action_Test__GetAll()
        {
            var hotel = _ctx.Hotels.Add(new App.Domain.Hotel()
            {
                Name = "Testing_Hotel",
                Description = "Testing_Hotel_Description",
                Id = default!
            });

            var hotel2 = _ctx.Hotels.Add(new App.Domain.Hotel()
            {
                Name = "Testing_Hotel2",
                Description = "Testing_Hotel_Description2",
                Id = default!
            });

            await _ctx.SaveChangesAsync();
            
            // ACT
            var res = await _appBll.Hotels.GetAllAsync();
            
            // Assert
            var hotels = res.ToList();
            Assert.NotNull(hotels.FirstOrDefault(e => e.Id == hotel.Entity.Id));
            Assert.NotNull(hotels.FirstOrDefault(e => e.Id == hotel2.Entity.Id));
        }
        
        [Fact]
        public async Task Action_Test__Exists()
        {
            // ARRANGE
            var hotel = _ctx.Hotels.Add(new App.Domain.Hotel()
            {
                Name = "Testing_Hotel",
                Description = "Testing_Hotel_Description",
                Id = default!
            });
            
            await _ctx.SaveChangesAsync();

            // ACT
            var res = await _appBll.Hotels.ExistsAsync(hotel.Entity.Id);
            // Assert
            Assert.True(res);
        }
        

        private static MapperConfiguration GetBllMapperConfiguration()
        {
            return new(cfg =>
            {
                cfg.CreateMap<App.BLL.DTO.Amenity, App.DAL.DTO.Amenity>().ReverseMap();
                cfg.CreateMap<App.BLL.DTO.Client, App.DAL.DTO.Client>().ReverseMap();
                cfg.CreateMap<App.BLL.DTO.Hotel, App.DAL.DTO.Hotel>().ReverseMap();
                cfg.CreateMap<App.BLL.DTO.Reservation, App.DAL.DTO.Reservation>().ReverseMap();
                cfg.CreateMap<App.BLL.DTO.Room, App.DAL.DTO.Room>().ReverseMap();
                cfg.CreateMap<App.BLL.DTO.RoomType, App.DAL.DTO.RoomType>().ReverseMap();
                cfg.CreateMap<App.BLL.DTO.Section, App.DAL.DTO.Section>().ReverseMap();
                cfg.CreateMap<App.BLL.DTO.Stay, App.DAL.DTO.Stay>().ReverseMap();
                cfg.CreateMap<App.BLL.DTO.Ticket, App.DAL.DTO.Ticket>().ReverseMap();
                cfg.CreateMap<App.BLL.DTO.UserHotel, App.DAL.DTO.UserHotel>().ReverseMap();
            });
        }

        private static MapperConfiguration GetDalMapperConfiguration()
        {
            return new(cfg =>
            {
                cfg.CreateMap<App.DAL.DTO.Amenity, App.Domain.Amenity>().ReverseMap();
                cfg.CreateMap<App.DAL.DTO.Client, App.Domain.Client>().ReverseMap();
                cfg.CreateMap<App.DAL.DTO.Hotel, App.Domain.Hotel>().ReverseMap();
                cfg.CreateMap<App.DAL.DTO.Reservation, App.Domain.Reservation>().ReverseMap();
                cfg.CreateMap<App.DAL.DTO.Room, App.Domain.Room>().ReverseMap();
                cfg.CreateMap<App.DAL.DTO.RoomType, App.Domain.RoomType>().ReverseMap();
                cfg.CreateMap<App.DAL.DTO.Section, App.Domain.Section>().ReverseMap();
                cfg.CreateMap<App.DAL.DTO.Stay, App.Domain.Stay>().ReverseMap();
                cfg.CreateMap<App.DAL.DTO.Ticket, App.Domain.Ticket>().ReverseMap();
                cfg.CreateMap<App.DAL.DTO.UserHotel, App.Domain.UserHotel>().ReverseMap();
            });
        }

}