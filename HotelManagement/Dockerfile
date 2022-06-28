FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /app
EXPOSE 80

# copy csproj and restore as distinct layers
COPY *.props .
COPY *.sln .

COPY App/App.Contracts.BLL/*.csproj /app/App/App.Contracts.BLL/
COPY App/App.Contracts.DAL/*.csproj /app/App/App.Contracts.DAL/
COPY App/App.BLL/*.csproj /app/App/App.BLL/
COPY App/App.BLL.DTO/*.csproj /app/App/App.BLL.DTO/
COPY App/App.DAL/*.csproj /app/App/App.DAL/
COPY App/App.DAL.DTO/*.csproj /app/App/App.DAL.DTO/
COPY App/App.Domain/*.csproj /app/App/App.Domain/
COPY App/App.Public.DTO/*.csproj /app/App/App.Public.DTO/
COPY App/App.Resources/*.csproj /app/App/App.Resources/
COPY App/WebApp/*.csproj /app/App/WebApp/
COPY App/Webapp.Tests/*.csproj /app/App/Webapp.Tests/

COPY App/WebApp/appsettings.json /app/App/WebApp/

COPY Base/Base.Contracts.Domain/*.csproj /app/Base/Base.Contracts.Domain/
COPY Base/Base.Contracts.DAL/*.csproj /app/Base/Base.Contracts.DAL/
COPY Base/Base.Contracts/*.csproj /app/Base/Base.Contracts/
COPY Base/Base.Contracts.BLL/*.csproj /app/Base/Base.Contracts.BLL/
COPY Base/Base.BLL/*.csproj /app/Base/Base.BLL/
COPY Base/Base.DAL/*.csproj /app/Base/Base.DAL/
COPY Base/Base.DAL.EF/*.csproj /app/Base/Base.DAL.EF/
COPY Base/Base.Domain/*.csproj /app/Base/Base.Domain/
COPY Base/Base.Extensions/*.csproj /app/Base/Base.Extensions/
COPY Base/Base.Resources/*.csproj /app/Base/Base.Resources/
COPY Base/Base.Webapp.Helpers/*.csproj /app/Base/Base.Webapp.Helpers/

RUN dotnet restore

# copy everything else and build app
COPY App/App.Contracts.BLL/. /app/App/App.Contracts.BLL/
COPY App/App.Contracts.DAL/. /app/App/App.Contracts.DAL/
COPY App/App.BLL/. /app/App/App.BLL/
COPY App/App.BLL.DTO/. /app/App/App.BLL.DTO/
COPY App/App.DAL/. /app/App/App.DAL/
COPY App/App.DAL.DTO/. /app/App/App.DAL.DTO/
COPY App/App.Domain/. /app/App/App.Domain/
COPY App/App.Public.DTO/. /app/App/App.Public.DTO/
COPY App/App.Resources/. /app/App/App.Resources/
COPY App/WebApp/. /app/App/WebApp/
COPY App/Webapp.Tests/. /app/App/WebApp.Tests/


COPY Base/Base.Contracts.Domain/. /app/Base/Base.Contracts.Domain/
COPY Base/Base.Contracts.DAL/. /app/Base/Base.Contracts.DAL/
COPY Base/Base.Contracts/. /app/Base/Base.Contracts/
COPY Base/Base.Contracts.BLL/. /app/Base/Base.Contracts.BLL/
COPY Base/Base.BLL/. /app/Base/Base.BLL/
COPY Base/Base.DAL/. /app/Base/Base.DAL/
COPY Base/Base.DAL.EF/. /app/Base/Base.DAL.EF/
COPY Base/Base.Domain/. /app/Base/Base.Domain/
COPY Base/Base.Extensions/. /app/Base/Base.Extensions/
COPY Base/Base.Resources/. /app/Base/Base.Resources/
COPY Base/Base.Webapp.Helpers/. /app/Base/Base.Webapp.Helpers/

WORKDIR /app/App/WebApp
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS runtime
WORKDIR /app
EXPOSE 80

COPY --from=build /app/App/WebApp/out ./
ENTRYPOINT ["dotnet", "WebApp.dll"]
