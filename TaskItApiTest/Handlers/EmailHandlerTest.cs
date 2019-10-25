using Microsoft.Extensions.Configuration;
using Moq;
using System.IO;
using TaskItApi.Dtos;
using TaskItApi.Entities;
using TaskItApi.Handlers;
using TaskItApi.Handlers.Interfaces;
using TaskItApi.Helper;
using Xunit;

namespace TaskItApiTest.Handlers
{
    public class EmailHandlerTest
    {
        private readonly Mock<IConfiguration> _configMock;
        private readonly Mock<IResourcesHelper> _resourcesHelper;
        private readonly Mock<ITokenHandler> _tokenHandler;

        public EmailHandlerTest()
        {
            _configMock = new Mock<IConfiguration>();
            _configMock.Setup(c => c[It.Is<string>(s => s.Equals("AppSettings:AppSecret"))]).Returns("TESTSTRINGSECRET");
            _configMock.Setup(c => c.GetSection(It.Is<string>(s => s.Equals("InviteEmailUrl"))).Value).Returns("https://localhost:44384");

            _resourcesHelper = new Mock<IResourcesHelper>();
            _resourcesHelper.Setup(r => r.GetInviteEmailTemplatePath()).Returns(GetInviteEmailPath());

            _tokenHandler = new Mock<ITokenHandler>();
            _tokenHandler.Setup(c => c.CreateInviteToken(It.Is<User>(u => u != null), It.Is<Group>(g => g != null))).Returns("TESTToken12321");
        }

        [Fact]
        public async System.Threading.Tasks.Task Test_CreateInviteEmail()
        {            
            IEmailHandler emailHandler = new EmailHandler(_configMock.Object, _resourcesHelper.Object, _tokenHandler.Object);

            User recievingUser = new User()
            {
                ID = 1,
                Email = "test@test.nl",
                Name = "test1"
            };

            User sendingUser = new User() 
            {
                ID = 2,
                Email = "test2@test.nl",
                Name = "test2"
            };

            Group group = new Group()
            {
                ID = 1,
                Name = "Group1" 
            };

            
            EmailDTO result = emailHandler.CreateInviteEmail(recievingUser, sendingUser, group);

            Assert.Equal(result.RecievingAdrress, recievingUser.Email);
            Assert.Contains(group.Name, result.Message);
            Assert.Contains(recievingUser.Name, result.Message);
        }

        private string GetInviteEmailPath()
        {            
            string invationEmailPath = Path.Combine(Directory.GetCurrentDirectory(), "Resources\\EmailTemplates\\GroupInvitation.html");
            return invationEmailPath;
        }
    }
}
