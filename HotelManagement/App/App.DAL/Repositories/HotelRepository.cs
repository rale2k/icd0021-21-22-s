using App.Contracts.DAL;
using Base.Contracts;
using Base.DAL.EF;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.Repositories;

public class HotelRepository : BaseEntityRepository<App.DAL.DTO.Hotel, Domain.Hotel, AppDbContext>, IHotelRepository
{
    public HotelRepository(AppDbContext dbContext, IMapper<DTO.Hotel, Domain.Hotel> mapper) : base(dbContext, mapper)
    {
    }
    
    public override DTO.Hotel Update(DTO.Hotel entity)
    {
        var domainEntity = RepoDbSet.AsNoTracking()
            .First(x => x.Id == entity.Id);

        var newEntity = RepoMapper.Map(entity)!;
        newEntity.Name = domainEntity.Name;
        newEntity.Name.SetTranslation(entity.Name);
        newEntity.Description = domainEntity.Description;
        newEntity.Description.SetTranslation(entity.Description);

        var updatedEntity = RepoDbSet.Update(newEntity!).Entity;
        return RepoMapper.Map(updatedEntity)!;
    }
}