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
        ITaskHolderRepository TaskHolderRepository { get;  }
        ITaskRepository TaskRepository { get; }
        int SaveChanges();

        void Dispose();
    }
}
