using Microsoft.AspNetCore.Identity;

namespace Base.Domain.Identity;

public class BaseUser : BaseUser<Guid>

{
    public BaseUser() : base()
    {
    }

    public BaseUser(string userName) : base(userName)
    {
    }
}

public class BaseUser<TKey> : IdentityUser<TKey>
    where TKey : IEquatable<TKey>
{
    public BaseUser() : base()
    {
    }

    public BaseUser(string userName) : base(userName)
    {
    }
}