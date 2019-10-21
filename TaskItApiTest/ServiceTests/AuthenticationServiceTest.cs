using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using System.Text;
using TaskItApi.Dtos;
using TaskItApi.Entities;
using TaskItApi.Exceptions;
using TaskItApi.Handlers.Interfaces;
using TaskItApi.Maps;
using TaskItApi.Models.Interfaces;
using TaskItApi.Repositories.Interfaces;
using TaskItApi.Services;
using TaskItApi.Services.Interfaces;
using Xunit;

namespace TaskItApiTest.ServiceTests
{
    public class AuthenticationServiceTest
    {
        private readonly ILoggerFactory _loggerFactory;
        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Mock<IConfiguration> _configMock;
        private readonly Mock<ITokenHandler> _tokenHandler;
        private readonly IMapper _mapper;

        //Mock user
        private readonly string mockName = "Test";
        private readonly string mockEmail = "test@test.nl";
        private readonly string mockPassword = "Test123@";

        public AuthenticationServiceTest()
        {
            _loggerFactory = LoggerFactory.Create(builder => {
                builder
                .AddFilter("Microsoft", LogLevel.Warning)
                .AddFilter("System", LogLevel.Warning)
                .AddFilter("LoggingConsoleApp.Program", LogLevel.Debug)
                .AddConsole()
                .AddEventLog();
            });

            CreatePasswordHash(this.mockPassword, out byte[] passwordHash, out byte[] passwordSalt);

            User user = new User
            {
                ID = 1,
                Email = this.mockEmail,
                Name = this.mockName,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt
            };

            _userRepositoryMock = new Mock<IUserRepository>();
            _userRepositoryMock.Setup(u => u.AddUser(It.Is<User>(u => u.Email.ToLower() == this.mockEmail))).Throws(new InvalidInputException("User already exist"));
            _userRepositoryMock.Setup(u => u.GetUser(It.Is<string>(s => s.ToLower() == this.mockEmail))).Returns(user);
            _userRepositoryMock.Setup(u => u.GetUser(It.Is<string>(s => s.ToLower() != this.mockEmail))).Throws(new InvalidInputException("User doesn't exist"));

            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _unitOfWorkMock.Setup(u => u.UserRepository).Returns(_userRepositoryMock.Object);

            _configMock = new Mock<IConfiguration>();
            _configMock.Setup(c => c[It.Is<string>(s => s.Equals("AppSettings:AppSecret"))]).Returns("TESTSTRINGSECRET");
            
            _tokenHandler = new Mock<ITokenHandler>();

            var mappingConfig = new MapperConfiguration(mc => 
            {
                mc.AddProfile(new MappingEntity());
            });

            _mapper = mappingConfig.CreateMapper();

        }

        [Fact]
        public async System.Threading.Tasks.Task Test_RegisterUser()
        {
            UserInComingDto newUser = new UserInComingDto()
            {
                Email = "test1@test.nl",
                Name = this.mockName,
                Password = this.mockName
            };

            AuthenticationService authenticationService = new AuthenticationService(_mapper , _unitOfWorkMock.Object, _loggerFactory.CreateLogger<IAuthenticationService>(), _tokenHandler.Object);

            Assert.NotNull(authenticationService.RegisterUser(newUser));
        }

        [Theory]
        [InlineData("test@test.nl")]
        [InlineData("Test@test.nl")]
        public async System.Threading.Tasks.Task Test_RegisterUser_EmailExist(string email)
        {
            UserInComingDto newUser = new UserInComingDto()
            {
                Email = email,
                Name = this.mockName,
                Password = this.mockPassword
            };           

            AuthenticationService authenticationService = new AuthenticationService(_mapper, _unitOfWorkMock.Object, _loggerFactory.CreateLogger<IAuthenticationService>(), _tokenHandler.Object);

            Assert.Throws<InvalidInputException>(() => authenticationService.RegisterUser(newUser));
        }

        [Theory]
        [InlineData("test@test.nl")]
        [InlineData("Test@test.nl")]
        public async System.Threading.Tasks.Task Test_LoginUser(string email)
        {
            UserInComingDto newUser = new UserInComingDto()
            {
                Email = email,
                Name = this.mockName,
                Password = this.mockPassword
            };

            AuthenticationService authenticationService = new AuthenticationService(_mapper, _unitOfWorkMock.Object, _loggerFactory.CreateLogger<IAuthenticationService>(), _tokenHandler.Object);
            TokenDto result = authenticationService.AuthenicateUser(newUser);

            Assert.NotNull(result);
            Assert.NotNull(result.Token);
        }

        [Fact]
        public async System.Threading.Tasks.Task Test_LoginUser_InvalidPassword()
        {
            UserInComingDto newUser = new UserInComingDto()
            {
                Email = this.mockEmail,
                Name = this.mockName,
                Password = "Test123!"
            };

            AuthenticationService authenticationService = new AuthenticationService(_mapper, _unitOfWorkMock.Object, _loggerFactory.CreateLogger<IAuthenticationService>(), _tokenHandler.Object);

            Assert.Throws<InvalidInputException>(() => authenticationService.AuthenicateUser(newUser));
        }

        [Fact]
        public async System.Threading.Tasks.Task Test_LoginUser_InvalidEmail()
        {
            UserInComingDto newUser = new UserInComingDto()
            {
                Email = "test123@test.nl",
                Name = this.mockName,
                Password = this.mockPassword
            };

            AuthenticationService authenticationService = new AuthenticationService(_mapper, _unitOfWorkMock.Object, _loggerFactory.CreateLogger<IAuthenticationService>(), _tokenHandler.Object);

            Assert.Throws<InvalidInputException>(() => authenticationService.AuthenicateUser(newUser));
        }

        /// <summary>
        /// Used for create a hash and salt for a mock user
        /// </summary>
        /// <param name="password"></param>
        /// <param name="passwordHash"></param>
        /// <param name="passwordSalt"></param>
        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }

    }
}
