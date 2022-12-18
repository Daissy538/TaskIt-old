using Microsoft.Extensions.Configuration;
using Moq;
using System;
using System.ComponentModel.DataAnnotations;
using System.Security.Authentication;
using System.Text;
using TaskIt.Core.Dtos;
using TaskIt.Domain.RepositoryInterfaces;
using TaskItApi.Entities;
using TaskItApi.Handlers;
using TaskItApi.Handlers.Interfaces;
using TaskItApi.Models;
using TaskItApi.Models.Interfaces;
using TaskItApi.Services;
using TaskItApiTest.Builders;
using Xunit;

namespace TaskItApiTest.Authenitication
{
    public class AuthenticationServiceTest
    {
        private readonly Mock<IUserRepository> userRepositoryMock;
        private readonly Mock<IGroupRepository> groupRepositoryMock;
        private readonly Mock<ISubscriptionRepository> subscriptionRepositoryMock;
        private readonly Mock<IColorRepository> colorRepositoryMock;
        private readonly Mock<IIconRepository> iconRepositoryMock;

        private IUnitOfWork _unitOfWorkMock;
        private ITokenHandler _tokenHandler;

        //Mock user
        private readonly string mockName = "Test";
        private readonly string mockEmail = "test@test.nl";
        private readonly string mockPassword = "Test123@";

        public AuthenticationServiceTest()
        {
            userRepositoryMock = new Mock<IUserRepository>();
            groupRepositoryMock = new Mock<IGroupRepository>();
            subscriptionRepositoryMock = new Mock<ISubscriptionRepository>();
            colorRepositoryMock = new Mock<IColorRepository>();
            iconRepositoryMock = new Mock<IIconRepository>();

            var dbContextMock = new Mock<TaskItDbContext>();
            this._unitOfWorkMock = new UnitOfWork(dbContextMock.Object, userRepositoryMock.Object, groupRepositoryMock.Object, subscriptionRepositoryMock.Object, colorRepositoryMock.Object, iconRepositoryMock.Object);


            var configuration = new Mock<IConfiguration>();
            configuration.Setup(c => c[It.Is<string>(s => s.Equals("AppSettings:AppSecret"))]).Returns("TESTSTRINGSECRET");
            _tokenHandler = new TokenHandler(configuration.Object);

        }

        [Fact]
        public async System.Threading.Tasks.Task Register_An_User()
        {
            AuthenticationService authenticationService = new AuthenticationService(this._unitOfWorkMock, this._tokenHandler);
            var result = await authenticationService.RegisterUserAsync(mockName, mockEmail, mockPassword);

            Assert.Equal(mockName, result.Name);
            Assert.Equal(mockEmail, result.Email);
            Assert.NotEmpty(result.PasswordHash);
            Assert.NotEmpty(result.PasswordSalt);
            Assert.Null(result.Subscriptions);
        }

        [Theory]
        [InlineData("testtest.nl")]
        [InlineData("@test.com")]
        [InlineData("test@.com")]
        public async System.Threading.Tasks.Task Not_Register_An_User_With_Invalid_Email(string email)
        {
            AuthenticationService authenticationService = new AuthenticationService(this._unitOfWorkMock, this._tokenHandler);
            await Assert.ThrowsAsync<ValidationException>(async () => await authenticationService.RegisterUserAsync(mockName, email, mockPassword));

        }

        [Fact]
        public async System.Threading.Tasks.Task Not_Register_An_User_When_Email_Exist()
        {
            userRepositoryMock.Setup(r => r.ContainceUser(mockEmail)).Returns(true);

            AuthenticationService authenticationService = new AuthenticationService(this._unitOfWorkMock, this._tokenHandler);

            await Assert.ThrowsAsync<ValidationException>(async () => await authenticationService.RegisterUserAsync(mockName, mockEmail, mockPassword));
        }

        [Theory]
        [InlineData("test@test.nl")]
        [InlineData("Test@test.nl")]
        public async System.Threading.Tasks.Task Login_User(string email)
        {
            AuthenticationService authenticationService = new AuthenticationService(this._unitOfWorkMock, this._tokenHandler);

            var user = new AuthenticateDto()
            {
                Email = email,
                Password = mockPassword
            };

            var token = authenticationService.AuthenticateUser(user);

            Assert.NotNull(token);
        }

        [Fact]
        public async System.Threading.Tasks.Task Not_Login_User_With_Invalid_Password()
        {
            User? dbUser = null;
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                dbUser = new UserBuilder()
                    .WithID(1)
                    .WithPasswordSalt(hmac.Key)
                    .WithEmail(mockEmail)
                    .WithPasswordHash(hmac.ComputeHash(Encoding.UTF8.GetBytes("12345")))
                    .WithName(mockName)
                    .Build();
            }

            userRepositoryMock.Setup(r => r.GetUserAsync(mockEmail)).Returns(System.Threading.Tasks.Task.FromResult<User?>(dbUser));

            AuthenticationService authenticationService = new AuthenticationService(this._unitOfWorkMock, this._tokenHandler);

            var user = new AuthenticateDto()
            {
                Email = mockEmail,
                Password = mockPassword
            };

            await Assert.ThrowsAsync<AuthenticationException>(async () => await authenticationService.AuthenticateUser(user));
        }

        [Fact]
        public async System.Threading.Tasks.Task Not_Login_User_With_Invalid_Email()
        {
            User? dbUser = null;
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                dbUser = new UserBuilder()
                    .WithID(1)
                    .WithPasswordSalt(hmac.Key)
                    .WithEmail(mockEmail)
                    .WithPasswordHash(hmac.ComputeHash(Encoding.UTF8.GetBytes(mockPassword)))
                    .WithName(mockName)
                    .Build();
            }

            userRepositoryMock.Setup(r => r.GetUserAsync(mockEmail)).Returns(System.Threading.Tasks.Task.FromResult<User?>(dbUser));

            AuthenticationService authenticationService = new AuthenticationService(this._unitOfWorkMock, this._tokenHandler);
            
            var user = new AuthenticateDto()
            {
                Email = "test@mail.nl",
                Password = mockPassword
            };

            await Assert.ThrowsAsync<NullReferenceException>(async () => await authenticationService.AuthenticateUser(user));
        }
    }
}
