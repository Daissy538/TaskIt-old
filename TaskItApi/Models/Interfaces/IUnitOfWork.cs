using TaskItApi.Repositories.Interfaces;

namespace TaskItApi.Models.Interfaces
{
    public interface IUnitOfWork
    {
        IUserRepository UserRepository { get; }
        IGroupRepository GroupRepository { get; }
        ISubscriptionRepository SubscriptionRepository { get; }
        IColorRepository ColorRepository { get;  }
        IIconRepository IconRepository { get;  }

        int SaveChanges();

        void Dispose();
    }
}
