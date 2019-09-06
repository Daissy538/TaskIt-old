using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskItApi.Repositories.Interfaces;

namespace TaskItApi.Models.Interfaces
{
    public interface IUnitOfWork
    {
        IUserRepository UserRepository { get; }

        IGroupRepository GroupRepository { get; }
        ISubscriptionRepository SubscriptionRepository { get; }

        int SaveChanges();

        void Dispose();
    }
}
