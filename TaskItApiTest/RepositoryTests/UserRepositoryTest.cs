using Microsoft.Extensions.Logging;
using System;
using System.Text;
using TaskItApi.Entities;
using TaskItApi.Exceptions;
using TaskItApi.Repositories;
using TaskItApi.Repositories.Interfaces;
using TaskItApiTest.dbTest;
using Xunit;

namespace TaskItApiTest.RepositoryTests
{
    public class UserRepositoryTest
    {
        private readonly int userId = 1;
        private readonly string userEmail = "test@test.nl";
        private readonly byte[] testHash = Encoding.UTF8.GetBytes("0xFCC898A159F91F054C5263BE46197436C89BA4FCBFE9FF4425ADD562982D8B9FF79E88D3C84BB758596F1F64E8280D146FFB160CA1B84788CFB67404A44D9C48");
        private readonly byte[] testSalt = Encoding.UTF8.GetBytes("0xCB333AC3600125C47304C66369B85D4918F919DB1D52C9C44577B20A8412D57836EDF7D1B7A38492D33908AD91B12B562EA73FF59DCBB009DC57DDD3699CA488E0ACEE68D55C7704EFA2823D301A6B5D9921CA78FA521018B8EFD7786434634C0F300D5DCD7CEAED7EF1490A20A4EDC681BDB00B0E918391B53FA67838E3AFB5");


        [Fact]
        public async System.Threading.Tasks.Task Test_AddUser()
        {
            var dbContext = DbContextMocker.GetTaskItDbContext(nameof(Test_AddUser));
            var loggerFactory = LoggerFactory.Create(builder => {
                builder
                .AddFilter("Microsoft", LogLevel.Warning)
                .AddFilter("System", LogLevel.Warning)
                .AddFilter("LoggingConsoleApp.Program", LogLevel.Debug)
                .AddConsole()
                .AddEventLog();
            });
            var userRepository = new UserRepository(dbContext, loggerFactory.CreateLogger<IUserRepository>());

            User newUser = new User()
            {
                ID = 2,
                Email = "test1@test.nl",
                Name = "Test",
                PasswordHash = testHash,
                PasswordSalt = testSalt
            };

            userRepository.AddUser(newUser);
            dbContext.SaveChanges();

            User user = userRepository.GetUser("test1@test.nl");

            Assert.NotNull(user);
            Assert.Equal(newUser.Email, user.Email);
            Assert.Equal(newUser.ID, user.ID);
            Assert.Equal(newUser.Name, user.Name);
        }

        [Fact]
        public async System.Threading.Tasks.Task Test_AddUser_NoContent()
        {
            var dbContext = DbContextMocker.GetTaskItDbContext(nameof(Test_AddUser_NoContent));
            var loggerFactory = LoggerFactory.Create(builder => {
                builder
                .AddFilter("Microsoft", LogLevel.Warning)
                .AddFilter("System", LogLevel.Warning)
                .AddFilter("LoggingConsoleApp.Program", LogLevel.Debug)
                .AddConsole()
                .AddEventLog();
            });
            var userRepository = new UserRepository(dbContext, loggerFactory.CreateLogger<IUserRepository>());

            User newUser = new User();

            Assert.Throws<NullReferenceException>(() => userRepository.AddUser(newUser));
        }

        [Fact]
        public async System.Threading.Tasks.Task Test_AddUser_EmailAlreadyExist()
        {
            var dbContext = DbContextMocker.GetTaskItDbContext(nameof(Test_AddUser_EmailAlreadyExist));
            var loggerFactory = LoggerFactory.Create(builder => {
                builder
                .AddFilter("Microsoft", LogLevel.Warning)
                .AddFilter("System", LogLevel.Warning)
                .AddFilter("LoggingConsoleApp.Program", LogLevel.Debug)
                .AddConsole()
                .AddEventLog();
            });
            var userRepository = new UserRepository(dbContext, loggerFactory.CreateLogger<IUserRepository>());

            User newUser = new User()
            {
                Email = "test@test.nl",
                Name = "Test",
                PasswordHash = testHash,
                PasswordSalt = testSalt
            };

            Assert.Throws<InvalidInputException>(() => userRepository.AddUser(newUser));
        }

        [Fact]
        public async System.Threading.Tasks.Task Test_GetUser_Email()
        {
            var dbContext = DbContextMocker.GetTaskItDbContext(nameof(Test_GetUser_Email));
            var loggerFactory = LoggerFactory.Create(builder => {
                builder
                .AddFilter("Microsoft", LogLevel.Warning)
                .AddFilter("System", LogLevel.Warning)
                .AddFilter("LoggingConsoleApp.Program", LogLevel.Debug)
                .AddConsole()
                .AddEventLog();
            });
            var userRepository = new UserRepository(dbContext, loggerFactory.CreateLogger<IUserRepository>());

            User user = userRepository.GetUser(userEmail);

            Assert.NotNull(user);
            Assert.Equal(userEmail, user.Email);
        }

        [Fact]
        public async System.Threading.Tasks.Task Test_GetUser_ID()
        {
            var dbContext = DbContextMocker.GetTaskItDbContext(nameof(Test_GetUser_ID));
            var loggerFactory = LoggerFactory.Create(builder => {
                builder
                .AddFilter("Microsoft", LogLevel.Warning)
                .AddFilter("System", LogLevel.Warning)
                .AddFilter("LoggingConsoleApp.Program", LogLevel.Debug)
                .AddConsole()
                .AddEventLog();
            });
            var userRepository = new UserRepository(dbContext, loggerFactory.CreateLogger<IUserRepository>());

            User user = userRepository.GetUser(userId);

            Assert.NotNull(user);
            Assert.Equal(userId, user.ID);
        }
    }
}
