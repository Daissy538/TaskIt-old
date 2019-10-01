using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using TaskItApi.Entities;
using TaskItApi.Repositories;
using TaskItApi.Repositories.Interfaces;
using TaskItApiTest.dbTest;
using Xunit;

namespace TaskItApiTest.RepositoryTests
{
    public class GroupRepositoryTest
    {
        private readonly int userId = 1;
        private readonly int nonExistingUserId = 0;
        private readonly int groupId = 1;
        private readonly int nonExistingGroupId = 0;
        private readonly int nonSubscribedGroupId = 2;

        [Fact]
        public async System.Threading.Tasks.Task Test_GetGroups()
        {

            var dbContext = DbContextMocker.GetTaskItDbContext(nameof(Test_GetGroups));
            var loggerFactory = LoggerFactory.Create(builder => {
                builder
                .AddFilter("Microsoft", LogLevel.Warning)
                .AddFilter("System", LogLevel.Warning)
                .AddFilter("LoggingConsoleApp.Program", LogLevel.Debug)
                .AddConsole()
                .AddEventLog();
            });
            var groupRepository = new GroupRepository(dbContext, loggerFactory.CreateLogger<IGroupRepository>());

            IEnumerable<Group> result  = groupRepository.FindAllGroupOfUser(userId);
          
            Assert.NotNull(result);
            Assert.NotEmpty(result);
        }

        [Fact]
        public async System.Threading.Tasks.Task Test_GetGroup()
        {
            var dbContext = DbContextMocker.GetTaskItDbContext(nameof(Test_GetGroup));
            var loggerFactory = LoggerFactory.Create(builder => {
                builder
                .AddFilter("Microsoft", LogLevel.Warning)
                .AddFilter("System", LogLevel.Warning)
                .AddFilter("LoggingConsoleApp.Program", LogLevel.Debug)
                .AddConsole()
                .AddEventLog();
            });
            var groupRepository = new GroupRepository(dbContext, loggerFactory.CreateLogger<IGroupRepository>());

            Group result = groupRepository.FindGroupOfUser(userId, groupId);

            Assert.NotNull(result);
            Assert.Equal(groupId, result.ID);
        }

        [Fact]
        public async System.Threading.Tasks.Task Test_GetGroups_NonExistingUser()
        {
            var dbContext = DbContextMocker.GetTaskItDbContext(nameof(Test_GetGroups_NonExistingUser));
            var loggerFactory = LoggerFactory.Create(builder => {
                builder
                .AddFilter("Microsoft", LogLevel.Warning)
                .AddFilter("System", LogLevel.Warning)
                .AddFilter("LoggingConsoleApp.Program", LogLevel.Debug)
                .AddConsole()
                .AddEventLog();
            });

            var groupRepository = new GroupRepository(dbContext, loggerFactory.CreateLogger<IGroupRepository>());

            IEnumerable<Group> result = groupRepository.FindAllGroupOfUser(nonExistingUserId);
            
            Assert.Empty(result);
        }

        [Fact]
        public async System.Threading.Tasks.Task Test_GetGroup_NonExistingUser()
        {
            var dbContext = DbContextMocker.GetTaskItDbContext(nameof(Test_GetGroup_NonExistingUser));
            var loggerFactory = LoggerFactory.Create(builder => {
                builder
                .AddFilter("Microsoft", LogLevel.Warning)
                .AddFilter("System", LogLevel.Warning)
                .AddFilter("LoggingConsoleApp.Program", LogLevel.Debug)
                .AddConsole()
                .AddEventLog();
            });

            var groupRepository = new GroupRepository(dbContext, loggerFactory.CreateLogger<IGroupRepository>());

            Group result = groupRepository.FindGroupOfUser(nonExistingUserId, groupId);

            Assert.Null(result);
        }

        [Fact]
        public async System.Threading.Tasks.Task Test_GetGroup_NonExistingGroup()
        {
            var dbContext = DbContextMocker.GetTaskItDbContext(nameof(Test_GetGroup_NonExistingGroup));
            var loggerFactory = LoggerFactory.Create(builder => {
                builder
                .AddFilter("Microsoft", LogLevel.Warning)
                .AddFilter("System", LogLevel.Warning)
                .AddFilter("LoggingConsoleApp.Program", LogLevel.Debug)
                .AddConsole()
                .AddEventLog();
            });

            var groupRepository = new GroupRepository(dbContext, loggerFactory.CreateLogger<IGroupRepository>());

            Group result = groupRepository.FindGroupOfUser(userId, nonExistingGroupId); 

            Assert.Null(result);
        }

        [Fact]
        public async System.Threading.Tasks.Task Test_GetGroup_NotSubscribedGroup()
        {
            var dbContext = DbContextMocker.GetTaskItDbContext(nameof(Test_GetGroup_NotSubscribedGroup));
            var loggerFactory = LoggerFactory.Create(builder => {
                builder
                .AddFilter("Microsoft", LogLevel.Warning)
                .AddFilter("System", LogLevel.Warning)
                .AddFilter("LoggingConsoleApp.Program", LogLevel.Debug)
                .AddConsole()
                .AddEventLog();
            });

            var groupRepository = new GroupRepository(dbContext, loggerFactory.CreateLogger<IGroupRepository>());

            Group result = groupRepository.FindGroupOfUser(userId, nonSubscribedGroupId);

            Assert.Null(result);
        }

    }
}
