using Microsoft.EntityFrameworkCore;
using TaskItApi.Entities;
using TaskItApi.Exceptions;
using TaskItApi.Models;
using TaskItApi.Repositories;
using TaskItApiTest.dbTest;
using Xunit;

namespace TaskItApiTest.RepositoryTests
{
    public class SubscriptionRepositoryTest
    {

        [Fact]
        public async System.Threading.Tasks.Task Test_Subscribe()
        {
            var dbContext = DbContextMocker.GetTaskItDbContext(nameof(Test_Subscribe));
            var subscriptionRepository = new SubscriptionRepository(dbContext);

            bool result = subscriptionRepository.SubscribeUser(1, 2);
            Assert.True(result);
        }

        [Fact]
        public async System.Threading.Tasks.Task Test_Subscribe_AlreadySubscribed()
        {
            var dbContext = DbContextMocker.GetTaskItDbContext(nameof(Test_Subscribe_AlreadySubscribed));
            var subscriptionRepository = new SubscriptionRepository(dbContext);

            Assert.Throws<InvalidInputException>(() => subscriptionRepository.SubscribeUser(1, 1));
        }

        [Fact]
        public async System.Threading.Tasks.Task Test_UnSubscribe_AlreadySubscribed()
        {
            var dbContext = DbContextMocker.GetTaskItDbContext(nameof(Test_UnSubscribe_AlreadySubscribed));
            var subscriptionRepository = new SubscriptionRepository(dbContext);

            Assert.Throws<InvalidInputException>(() => subscriptionRepository.UnSubscribeUser(1, 4));
        }
    }
}
