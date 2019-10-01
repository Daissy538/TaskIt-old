using System;
using System.Text;
using TaskItApi.Entities;
using TaskItApi.Models;

namespace TaskItApiTest.dbTest
{
    public static class DbContextExtensions
    {
        public static void Seed(this TaskItDbContext dbContext)
        {
            byte[] hash = Encoding.UTF8.GetBytes("0xFCC898A159F91F054C5263BE46197436C89BA4FCBFE9FF4425ADD562982D8B9FF79E88D3C84BB758596F1F64E8280D146FFB160CA1B84788CFB67404A44D9C48");
            byte[] salt = Encoding.UTF8.GetBytes("0xCB333AC3600125C47304C66369B85D4918F919DB1D52C9C44577B20A8412D57836EDF7D1B7A38492D33908AD91B12B562EA73FF59DCBB009DC57DDD3699CA488E0ACEE68D55C7704EFA2823D301A6B5D9921CA78FA521018B8EFD7786434634C0F300D5DCD7CEAED7EF1490A20A4EDC681BDB00B0E918391B53FA67838E3AFB5");

            dbContext.Users.Add(new User
            {
                ID = 1,
                Email = "test@test.nl",
                Name = "Test",
                PasswordHash = hash,
                PasswordSalt = salt
            }) ;

            dbContext.Groups.Add(new Group
            {
                ID = 1,
                Name = "Thuis",
                Description = "Test Omschrijving",
                Color = "#5c6bc0",
                Icon = "house"
            });

            dbContext.Groups.Add(new Group
            {
                ID = 2,
                Name = "Sport",
                Description = "Test Omschrijving",
                Color = "#5c6bc0",
                Icon = "nature_people"
            });

            dbContext.Subscriptions.Add(new Subscription
            {
                ID = 1,
                DateOfSubscription = DateTime.UtcNow,
                GroupID = 1,
                UserID = 1
            });

            dbContext.SaveChanges();
        }
    }
}
