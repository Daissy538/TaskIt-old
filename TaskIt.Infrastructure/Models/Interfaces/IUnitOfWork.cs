using TaskIt.Domain.RepositoryInterfaces;

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

        Task<int> SaveChangesAsync();

        void Dispose();
    }
}
