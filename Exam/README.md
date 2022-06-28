### Database
~~~sh
dotnet ef migrations add --project App.DAL --startup-project WebApp --context AppDbContext Initial
dotnet ef migrations remove --project App.DAL --startup-project WebApp --context AppDbContext 
dotnet ef database update --project App.DAL --startup-project WebApp
dotnet ef database drop --project App.DAL --startup-project WebApp
~~~
### Controllers
###### Razor MVC
~~~sh
dotnet aspnet-codegenerator controller -name AmenityController -actions -m  App.Domain.Amenity -dc AppDbContext -outDir Areas/Admin/Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name ApartmentController -actions -m  App.Domain.Apartment -dc AppDbContext -outDir Areas/Admin/Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name BillController -actions -m  App.Domain.Bill -dc AppDbContext -outDir Areas/Admin/Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name BuildingController -actions -m  App.Domain.Building -dc AppDbContext -outDir Areas/Admin/Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name ContractController -actions -m  App.Domain.Contract -dc AppDbContext -outDir Areas/Admin/Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name ReadingController -actions -m  App.Domain.Reading -dc AppDbContext -outDir Areas/Admin/Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name ServiceController -actions -m  App.Domain.Service -dc AppDbContext -outDir Areas/Admin/Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
~~~
###### API
~~~sh
dotnet aspnet-codegenerator controller -name AmenityController -actions -m  App.Domain.Amenity -dc AppDbContext -outDir ApiControllers -api --useAsyncActions  -f
dotnet aspnet-codegenerator controller -name ApartmentController -actions -m  App.Domain.Apartment -dc AppDbContext -outDir ApiControllers -api --useAsyncActions  -f
dotnet aspnet-codegenerator controller -name BillController -actions -m  App.Domain.Bill -dc AppDbContext -outDir ApiControllers -api --useAsyncActions  -f
dotnet aspnet-codegenerator controller -name BuildingController -actions -m  App.Domain.Building -dc AppDbContext -outDir ApiControllers -api --useAsyncActions  -f
dotnet aspnet-codegenerator controller -name ContractController -actions -m  App.Domain.Contract -dc AppDbContext -outDir ApiControllers -api --useAsyncActions  -f
dotnet aspnet-codegenerator controller -name ReadingController -actions -m  App.Domain.Reading -dc AppDbContext -outDir ApiControllers -api --useAsyncActions  -f
dotnet aspnet-codegenerator controller -name ServiceController -actions -m  App.Domain.Service -dc AppDbContext -outDir ApiControllers -api --useAsyncActions  -f
~~~
###### Identity pages
~~~sh
dotnet aspnet-codegenerator identity -dc App.DAL.EF.AppDbContext -f
~~~
