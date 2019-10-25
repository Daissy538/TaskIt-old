using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using System.Collections.Generic;
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
    public class GroupServiceTest
    {
        private readonly ILoggerFactory _loggerFactory;
        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly Mock<IGroupRepository> _groupRepositoryMock;
        private readonly Mock<ISubscriptionRepository> _subscriptionRepositoryMock;
        private readonly Mock<IColorRepository> _colorRepositoryMock;
        private readonly Mock<IIconRepository> _iconRepositoryMock;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Mock<IConfiguration> _configMock;
        private readonly Mock<IEmailHandler> _emailHandler;
        private readonly Mock<ITokenHandler> _tokenHandler;
        private readonly IMapper _mapper;

        //Mock user
        private readonly string _mockName = "Test";
        private readonly string _mockEmail = "test@test.nl";
        private readonly string _mockPassword = "Test123@";
        private readonly int _mockID = 1;

        public GroupServiceTest()
        {
            _loggerFactory = LoggerFactory.Create(builder => {
                builder
                .AddFilter("Microsoft", LogLevel.Warning)
                .AddFilter("System", LogLevel.Warning)
                .AddFilter("LoggingConsoleApp.Program", LogLevel.Debug)
                .AddConsole()
                .AddEventLog();
            });

            User user = new User
            {
                ID = 1,
                Email = this._mockEmail,
                Name = this._mockName
            };

            _userRepositoryMock = new Mock<IUserRepository>();
            _userRepositoryMock.Setup(u => u.AddUser(It.Is<User>(u => u.Email.ToLower() == this._mockEmail))).Throws(new InvalidInputException("User already exist"));
            _userRepositoryMock.Setup(u => u.GetUser(It.Is<int>(i => i == this._mockID))).Returns(user);
            _userRepositoryMock.Setup(u => u.ContainceUser(It.Is<int>(i => i == this._mockID))).Returns(true);
            _userRepositoryMock.Setup(u => u.ContainceUser(It.Is<int>(i => i != this._mockID))).Throws(new InvalidInputException("User doesn't exist"));
            _userRepositoryMock.Setup(u => u.GetUser(It.Is<string>(s => s.ToLower() == this._mockEmail))).Returns(user);
            _userRepositoryMock.Setup(u => u.GetUser(It.Is<string>(s => s.ToLower() != this._mockEmail))).Throws(new InvalidInputException("User doesn't exist"));
            _userRepositoryMock.Setup(u => u.ContainceUser(It.Is<string>(s => s.ToLower() == this._mockEmail))).Returns(true);
            _userRepositoryMock.Setup(u => u.ContainceUser(It.Is<string>(s => s.ToLower() != this._mockEmail))).Returns(false);
            _userRepositoryMock.Setup(u => u.ContainceUser(It.Is<int>(i => i == _mockID))).Returns(true);
            _userRepositoryMock.Setup(u => u.ContainceUser(It.Is<int>(i => i != _mockID))).Returns(false);

            _groupRepositoryMock = new Mock<IGroupRepository>();
            _subscriptionRepositoryMock = new Mock<ISubscriptionRepository>();
            _colorRepositoryMock = new Mock<IColorRepository>();
            _iconRepositoryMock = new Mock<IIconRepository>();

            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _unitOfWorkMock.Setup(u => u.UserRepository).Returns(_userRepositoryMock.Object);
            _unitOfWorkMock.Setup(u => u.GroupRepository).Returns(_groupRepositoryMock.Object);
            _unitOfWorkMock.Setup(u => u.SubscriptionRepository).Returns(_subscriptionRepositoryMock.Object);
            _unitOfWorkMock.Setup(u => u.ColorRepository).Returns(_colorRepositoryMock.Object);
            _unitOfWorkMock.Setup(u => u.IconRepository).Returns(_iconRepositoryMock.Object);

            _configMock = new Mock<IConfiguration>();
            _configMock.Setup(c => c[It.Is<string>(s => s.Equals("AppSettings:AppSecret"))]).Returns("TESTSTRINGSECRET");

            _emailHandler = new Mock<IEmailHandler>();
            _tokenHandler = new Mock<ITokenHandler>();

            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingEntity());
            });

            _mapper = mappingConfig.CreateMapper();
        }

        [Fact]
        public async System.Threading.Tasks.Task Test_CreateGroup()
        {
            GroupService groupService = new GroupService(_mapper, _unitOfWorkMock.Object, _loggerFactory.CreateLogger<IGroupService>(), _emailHandler.Object, _tokenHandler.Object);

            GroupIncomingDTO newGroup = new GroupIncomingDTO()
            {
                ColorID = 1,
                Description = "Test description",
                IconID = 1,
                Name = "House"
            };

            _groupRepositoryMock.Setup(u => u.FindAllGroupOfUser(1)).Returns(mockGroupResponse(newGroup));
            IEnumerable<Group> result = groupService.Create(newGroup, this._mockID);

            Assert.NotNull(result);
            Assert.NotEmpty(result);
        }

        
        public async System.Threading.Tasks.Task Test_CreateGroup_NonExistingUser()
        {
            GroupService groupService = new GroupService(_mapper, _unitOfWorkMock.Object, _loggerFactory.CreateLogger<IGroupService>(), _emailHandler.Object, _tokenHandler.Object);

            GroupIncomingDTO newGroup = new GroupIncomingDTO()
            {
                ColorID = 1,
                Description = "Test description",
                IconID = 1,
                Name = "House"
            };

            _groupRepositoryMock.Setup(u => u.FindAllGroupOfUser(1)).Returns(mockGroupResponse(newGroup));
            Assert.Throws<InvalidInputException>(() => groupService.Create(newGroup, 2));
        }

        [Fact]
        public async System.Threading.Tasks.Task Test_DeleteGroup()
        {
            GroupService groupService = new GroupService(_mapper, _unitOfWorkMock.Object, _loggerFactory.CreateLogger<IGroupService>(), _emailHandler.Object, _tokenHandler.Object);

            Color color = new Color()
            {
                ID = 1,
                Name = "Pink",
                Value = "#5c6bc0"
            };

            Icon icon = new Icon()
            {
                ID = 1,
                Name = "Natuur",
                Value = "nature_people"
            };

            Group existingGroup = new Group()
            {
                ID = 1,
                Color = color,
                Description = "Test description",
                Icon = icon,
                Name = "House",
                Members = new List<Subscription>()
            };

            _groupRepositoryMock.Setup(u => u.FindGroupOfUser(1, 1)).Returns(existingGroup);
            _groupRepositoryMock.Setup(u => u.FindAllGroupOfUser(1)).Returns(new List<Group>());
            IEnumerable<Group> result = groupService.Delete(1, this._mockID);

            Assert.NotNull(result);
            Assert.Empty(result);
        }

        [Fact]
        public async System.Threading.Tasks.Task Test_DeleteGroup_NotExisting()
        {
            GroupService groupService = new GroupService(_mapper, _unitOfWorkMock.Object, _loggerFactory.CreateLogger<IGroupService>(), _emailHandler.Object, _tokenHandler.Object);

            Assert.Throws<InvalidInputException>(() => groupService.Delete(2, this._mockID));
        }

        [Fact]
        public async System.Threading.Tasks.Task Test_UpdateGroup()
        {
            GroupService groupService = new GroupService(_mapper, _unitOfWorkMock.Object, _loggerFactory.CreateLogger<IGroupService>(), _emailHandler.Object, _tokenHandler.Object);

            Color color = new Color()
            {
                ID = 1,
                Name = "Pink",
                Value = "#5c6bc0"
            };

            Icon icon = new Icon()
            {
                ID = 1,
                Name = "Natuur",
                Value = "nature_people"
            };

            Group existingGroup = new Group()
            {
                ID = 1,
                Color = color,
                Description = "Test description",
                Icon = icon,
                Name = "House",
                Members = new List<Subscription>()
            };

            GroupIncomingDTO updateGroup = new GroupIncomingDTO()
            {
                ColorID = 1,
                Description = "hallo",
                IconID = 1,
                Name = "Test"
            };

            _colorRepositoryMock.Setup(u => u.GetColorByValue(updateGroup.ColorID)).Returns(color);
            _iconRepositoryMock.Setup(u => u.GetIconByValue(updateGroup.IconID)).Returns(icon);

            _groupRepositoryMock.Setup(u => u.FindGroupOfUser(1, 1)).Returns(existingGroup);
            Group result = groupService.Update(existingGroup.ID, updateGroup, _mockID);

            Assert.NotNull(result);
            Assert.Equal(updateGroup.Name, result.Name);
            Assert.Equal(updateGroup.Description, result.Description);
        }

        [Fact]
        public async System.Threading.Tasks.Task Test_UpdateGroup_InvalidUser()
        {
            GroupService groupService = new GroupService(_mapper, _unitOfWorkMock.Object, _loggerFactory.CreateLogger<IGroupService>(), _emailHandler.Object, _tokenHandler.Object);

            Color color = new Color()
            {
                ID = 1,
                Name = "Pink",
                Value = "#5c6bc0"
            };

            Icon icon = new Icon()
            {
                ID = 1,
                Name = "Natuur",
                Value = "nature_people"
            };

            Group existingGroup = new Group()
            {
                ID = 1,
                Color = color,
                Description = "Test description",
                Icon = icon,
                Name = "House",
                Members = new List<Subscription>()
            };

            GroupIncomingDTO updateGroup = new GroupIncomingDTO()
            {
                ColorID = 1,
                Description = "hallo",
                IconID = 1,
                Name = "Test"
            };

            _colorRepositoryMock.Setup(u => u.GetColorByValue(updateGroup.ColorID)).Returns(color);
            _iconRepositoryMock.Setup(u => u.GetIconByValue(updateGroup.IconID)).Returns(icon);

            _groupRepositoryMock.Setup(u => u.FindGroupOfUser(1, 1)).Returns(existingGroup);
            int incorrectUserId = 3;

            Assert.Throws<InvalidInputException>(() => groupService.Update(existingGroup.ID, updateGroup, incorrectUserId));
        }

        //Mock group response
        //Without including subscribtion data
        private IEnumerable<Group> mockGroupResponse(GroupIncomingDTO groupDto)
        {
            Group group = _mapper.Map<Group>(groupDto);

            List<Group> result = new List<Group>();
            result.Add(group);

            return result;
        }
    }
}
