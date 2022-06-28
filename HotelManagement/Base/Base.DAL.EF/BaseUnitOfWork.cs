using Base.Contracts.DAL;
using Microsoft.EntityFrameworkCore;

namespace Base.DAL.EF;

public class BaseUnitOfWork <TDbContext> : IUnitOfWork
    where TDbContext : DbContext
{
    protected readonly TDbContext DbContext;

    public BaseUnitOfWork(TDbContext dbContext)
    {
        DbContext = dbContext;
    }

    public virtual async Task<int> SaveChangesAsync()
    {
        return await DbContext.SaveChangesAsync();
    }

    public virtual int SaveChanges()
    {
        return DbContext.SaveChanges();
    }
}
