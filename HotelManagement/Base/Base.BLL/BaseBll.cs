using Base.Contracts.BLL;
using Base.Contracts.DAL;

namespace Base.BLL;

public abstract class BaseBll<TUnitOfWork> : IBll
    where TUnitOfWork : IUnitOfWork
{
    protected readonly TUnitOfWork Uow;
    protected BaseBll(TUnitOfWork uow)
    {
        Uow = uow;
    }

    public abstract Task<int> SaveChangesAsync();

    public abstract int SaveChanges();
}
