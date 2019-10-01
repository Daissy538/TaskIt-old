using Microsoft.Extensions.Logging;
using TaskItApi.Entities;
using TaskItApi.Models;
using TaskItApi.Repositories.Interfaces;

namespace TaskItApi.Repositories
{
    public class SubscriptionRepository : RepositoryBase<Subscription>, ISubscriptionRepository
    {
        private readonly ILogger _logger;

        public SubscriptionRepository(TaskItDbContext taskItDbContext, ILogger<ISubscriptionRepository> logger)
            : base(taskItDbContext)
        {
            _logger = logger;
        }


    }
}
