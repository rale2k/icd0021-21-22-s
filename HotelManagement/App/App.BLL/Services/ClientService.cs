using App.Contracts.BLL.Services;
using App.Contracts.DAL;
using Base.BLL;
using Base.Contracts;

namespace App.BLL.Services;

public class ClientService : BaseEntityService<App.BLL.DTO.Client, App.DAL.DTO.Client, IClientRepository>, IClientService
{
    public ClientService(IClientRepository repo, IMapper<App.BLL.DTO.Client, App.DAL.DTO.Client> mapper) : base(repo, mapper)
    {
    }
}