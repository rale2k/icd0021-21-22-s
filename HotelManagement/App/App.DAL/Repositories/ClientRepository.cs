using App.Contracts.DAL;
using Base.Contracts;
using Base.DAL.EF;

namespace App.DAL.Repositories;

public class ClientRepository : BaseEntityRepository<App.DAL.DTO.Client, Domain.Client, AppDbContext>, IClientRepository
{
    public ClientRepository(AppDbContext dbContext, IMapper<DTO.Client, Domain.Client> mapper) : base(dbContext, mapper)
    {
    }
}