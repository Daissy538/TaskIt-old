using TaskIt.Domain.RepositoryInterfaces;
using TaskItApi.Models.Interfaces;

namespace TaskItApi.Models
{
    /// <summary>
    /// Used the repository and Unit of Work patterns to centralise database actions.
    /// So that multiple repositories can be used in one service.
    /// After writing all the request call the Complete function. Then all the request in the dbContext instance memory will be executed.
    /// To remove all the allocated resources of the dbContext call the dispose function.
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {
        private readonly TaskItDbContext _taskItDbContext;

        public IUserRepository UserRepository { get; private set; }
        public IGroupRepository GroupRepository { get; private set; }
        public ISubscriptionRepository SubscriptionRepository { get; private set; }
        public IColorRepository ColorRepository { get; private set;  }
        public IIconRepository IconRepository { get; private set; }

        public UnitOfWork(TaskItDbContext taskItDbContext, IUserRepository userRepository, IGroupRepository groupRepository, ISubscriptionRepository subscriptionRepository, IColorRepository colorRepository, IIconRepository iconRepository)
        {
            _taskItDbContext = taskItDbContext;

            UserRepository = userRepository;
            GroupRepository = groupRepository;
            SubscriptionRepository = subscriptionRepository;
            ColorRepository = colorRepository;
            IconRepository = iconRepository;
        }

        /// <summary>
        /// Save changes that where made in the dbContext memory to the database.
        /// </summary>
        /// <returns></returns>
        public int SaveChanges()
        {
            return _taskItDbContext.SaveChanges();
        }

        /// <summary>
        /// Save changes that where made in the dbContext memory to the database.
        /// </summary>
        /// <returns></returns>
        public async Task<int> SaveChangesAsync()
        {
            return await _taskItDbContext.SaveChangesAsync();
        }

        /// <summary>
        /// Release all the allocated resources of the dbContext.
        /// </summary>
        public void Dispose()
        {
            _taskItDbContext.Dispose();
        }
    }
}
