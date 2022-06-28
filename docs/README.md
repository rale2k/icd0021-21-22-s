## Scaffolding

### Database
~~~sh
dotnet ef migrations add --project App.DAL --startup-project WebApp --context AppDbContext Initial
dotnet ef migrations remove --project App.DAL --startup-project WebApp --context AppDbContext 
dotnet ef database update --project App.DAL --startup-project WebApp
dotnet ef database drop --project App.DAL --startup-project WebApp
~~~
Override OnModelCreating with this to scaffold. (.UseMicrosoftJson needs to be specified)
~~~c#
protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
{
    var connectionString = "";
    optionsBuilder
        .UseMySql(
            connectionString, 
            ServerVersion.AutoDetect(connectionString),
            b => b
                .UseMicrosoftJson(MySqlCommonJsonChangeTrackingOptions.FullHierarchyOptimizedFast));
}
~~~
### Controllers
###### Razor MVC
~~~sh
dotnet aspnet-codegenerator controller -name AmenityController -actions -m  App.Domain.Amenity -dc AppDbContext -outDir Areas/Admin/Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name ClientController -actions -m  App.Domain.Client -dc AppDbContext -outDir Areas/Admin/Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name HotelController -actions -m  App.Domain.Hotel -dc AppDbContext -outDir Areas/Admin/Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name ReservationController -actions -m  App.Domain.Reservation -dc AppDbContext -outDir Areas/Admin/Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name ReservationAmenityController -actions -m  App.Domain.ReservationAmenity -dc AppDbContext -outDir Areas/Admin/Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name RoomController -actions -m  App.Domain.Room -dc AppDbContext -outDir Areas/Admin/Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name RoomTicketController -actions -m  App.Domain.RoomTicket -dc AppDbContext -outDir Areas/Admin/Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name RoomTypeController -actions -m  App.Domain.RoomType -dc AppDbContext -outDir Areas/Admin/Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name SectionController -actions -m  App.Domain.Section -dc AppDbContext -outDir Areas/Admin/Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name StayController -actions -m  App.Domain.Stay -dc AppDbContext -outDir Areas/Admin/Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name TicketController -actions -m  App.Domain.Ticket -dc AppDbContext -outDir Areas/Admin/Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
~~~
###### API
~~~sh
dotnet aspnet-codegenerator controller -name AmenityController -actions -m  App.Domain.Amenity -dc AppDbContext -outDir Controllers/Api -api --useAsyncActions  -f
dotnet aspnet-codegenerator controller -name ClientController -actions -m  App.Domain.Client -dc AppDbContext -outDir Controllers/Api -api --useAsyncActions  -f
dotnet aspnet-codegenerator controller -name HotelController -actions -m  App.Domain.Hotel -dc AppDbContext -outDir Controllers/Api -api --useAsyncActions  -f
dotnet aspnet-codegenerator controller -name ReservationController -actions -m  App.Domain.Reservation -dc AppDbContext -outDir Controllers/Api -api --useAsyncActions  -f
dotnet aspnet-codegenerator controller -name ReservationAmenityController -actions -m  App.Domain.ReservationAmenity -dc AppDbContext -outDir Controllers/Api -api --useAsyncActions  -f
dotnet aspnet-codegenerator controller -name RoomController -actions -m  App.Domain.Room -dc AppDbContext -outDir Controllers/Api -api --useAsyncActions  -f
dotnet aspnet-codegenerator controller -name RoomTicketController -actions -m  App.Domain.RoomTicket -dc AppDbContext -outDir Controllers/Api -api --useAsyncActions  -f
dotnet aspnet-codegenerator controller -name RoomTypeController -actions -m  App.Domain.RoomType -dc AppDbContext -outDir Controllers/Api -api --useAsyncActions  -f
dotnet aspnet-codegenerator controller -name SectionController -actions -m  App.Domain.Section -dc AppDbContext -outDir Controllers/Api -api --useAsyncActions  -f
dotnet aspnet-codegenerator controller -name StayController -actions -m  App.Domain.Stay -dc AppDbContext -outDir Controllers/Api -api --useAsyncActions  -f
dotnet aspnet-codegenerator controller -name TicketController -actions -m  App.Domain.Ticket -dc AppDbContext -outDir Controllers/Api -api --useAsyncActions  -f
~~~
###### Identity pages
~~~sh
dotnet aspnet-codegenerator identity -dc App.DAL.EF.AppDbContext -f
~~~
