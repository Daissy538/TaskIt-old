using Moq;
using System;
using System.Collections.Generic;
using TaskIt.Domain.RepositoryInterfaces;
using TaskItApi.Entities;
using TaskItApi.Handlers.Interfaces;
using TaskItApi.Models;
using TaskItApi.Models.Interfaces;
using TaskItApi.Services;
using TaskItApiTest.Builders;
using Xunit;
using Task = System.Threading.Tasks.Task;

namespace TaskItApiTest.Groups
{
    public class GroupServiceTest
    {
 
        private readonly Mock<IUserRepository> userRepositoryMock;
        private readonly Mock<IGroupRepository> groupRepositoryMock;
        private readonly Mock<ISubscriptionRepository> subscriptionRepositoryMock;
        private readonly Mock<IColorRepository> colorRepositoryMock;
        private readonly Mock<IIconRepository> iconRepositoryMock;


        private IUnitOfWork _unitOfWork;

        public GroupServiceTest()
        {
            userRepositoryMock = new Mock<IUserRepository>();
            groupRepositoryMock = new Mock<IGroupRepository>();
            subscriptionRepositoryMock = new Mock<ISubscriptionRepository>();
            colorRepositoryMock = new Mock<IColorRepository>();
            iconRepositoryMock = new Mock<IIconRepository>();

            var dbContextMock = new Mock<TaskItDbContext>();
            this._unitOfWork = new UnitOfWork(dbContextMock.Object, userRepositoryMock.Object, groupRepositoryMock.Object, subscriptionRepositoryMock.Object, colorRepositoryMock.Object, iconRepositoryMock.Object);

        }

        [Fact]
        public async Task Create_A_Group()
        {
            var group = new GroupBuilder()
                .WithId(1)
                .Build();

            var user = new UserBuilder()
                .WithID(1)
                .Build();

            var groupList = new List<Group>();
            groupList.Add(group);

            groupRepositoryMock.Setup(u => u.FindAllGroupOfUserAsync(user.ID)).ReturnsAsync(groupList);
            userRepositoryMock.Setup(u => u.GetUserAsync(user.ID)).ReturnsAsync(user);

            GroupService groupService = new GroupService(this._unitOfWork);

            Group newGroup = new GroupBuilder()
                .Build();

            List<Group> result = await groupService.CreateAsync(newGroup, user.ID);

            Assert.NotEmpty(result);
            Assert.Contains(group, result);
        }

        [Fact]
        public async Task Can_Not_Create_Group_For_Non_Existing_User()
        {
            var userId = 1;

            GroupService groupService = new GroupService(this._unitOfWork);

            Group newGroup = new GroupBuilder()
                                 .Build();

            userRepositoryMock.Setup(u => u.GetUserAsync(userId)).Returns<User?>(null);
            await Assert.ThrowsAsync<NullReferenceException>(async () => await groupService.CreateAsync(newGroup, userId));
        }

        [Fact]
        public async Task Delete_Group()
        {
            var userId = 1;
            GroupService groupService = new GroupService(this._unitOfWork);

            Color color = new ColorBuilder()
                                .WithID(1)
                                .Build();

            Icon icon = new IconBuilder()
                        .WithID(1)
                        .Build();

            Group existingGroup = new GroupBuilder()
                                        .WithColorId(color.ID)
                                        .WithIconId(icon.ID)
                                        .WithId(1)
                                        .Build();

            groupRepositoryMock.Setup(u => u.FindGroupOfUserAsync(existingGroup.ID, userId)).ReturnsAsync(existingGroup);
            groupRepositoryMock.Setup(u => u.FindAllGroupOfUserAsync(userId)).ReturnsAsync(new List<Group>());
            IEnumerable<Group> result = await groupService.DeleteAsync(1, userId);

            Assert.NotNull(result);
            Assert.DoesNotContain(existingGroup, result);
        }

        [Fact]
        public async Task Can_Not_Delete_A_Group_When_It_Does_Not_Exist()
        {
            var userId = 1;
            var groupId = 1;

            GroupService groupService = new GroupService(this._unitOfWork);
            await Assert.ThrowsAsync<NullReferenceException>(async() => await groupService.DeleteAsync(groupId, userId));
        }

        [Fact]
        public async Task Update_Group()
        {
            var userId = 1;

            GroupService groupService = new GroupService(this._unitOfWork);
            Color color = new ColorBuilder()
                                .WithID(1)
                                .Build();

            Icon icon = new IconBuilder()
                        .WithID(1)
                        .Build();

            Group existingGroup = new GroupBuilder()
                                        .WithColorId(color.ID)
                                        .WithIconId(icon.ID)
                                        .WithId(1)
                                        .Build();

            groupRepositoryMock.Setup(u => u.FindGroupOfUserAsync(existingGroup.ID, userId)).ReturnsAsync(existingGroup);

            Group updateGroup = new GroupBuilder()
                                        .WithColorId(color.ID)
                                        .WithIconId(icon.ID)
                                        .WithId(existingGroup.ID)
                                        .WithDescription("New")
                                        .Build();

            Group result = await groupService.UpdateAsync(updateGroup, userId);

            Assert.NotNull(result);
            Assert.Equal(updateGroup.Name, result.Name);
            Assert.Equal(updateGroup.Description, result.Description);
        }

        [Fact]
        public async Task Not_Update_A_Group_When_User_Is_Not_Linked()
        {
            var userId = 1;

            GroupService groupService = new GroupService(this._unitOfWork);

            Color color = new ColorBuilder()
                                .WithID(1)
                                .Build();

            Icon icon = new IconBuilder()
                        .WithID(1)
                        .Build();

            Group updateGroup = new GroupBuilder()
                                        .WithColorId(color.ID)
                                        .WithIconId(icon.ID)
                                        .WithId(1)
                                        .Build();

            groupRepositoryMock.Setup(u => u.FindGroupOfUserAsync(updateGroup.ID, userId)).Returns<Group?>(null);

            await Assert.ThrowsAsync<NullReferenceException>(async () => await groupService.UpdateAsync(updateGroup, userId));
        }
    }
}
