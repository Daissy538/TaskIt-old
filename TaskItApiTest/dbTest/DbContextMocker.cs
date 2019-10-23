using Microsoft.EntityFrameworkCore;
using TaskItApi.Models;

namespace TaskItApiTest.dbTest
{
    public static class DbContextMocker
    {
        public static TaskItDbContext GetTaskItDbContext(string dbName)
        {
            // Create options for DbContext instance
            var options = new DbContextOptionsBuilder<TaskItDbContext>()
                .UseInMemoryDatabase(databaseName: dbName)
                .Options;

            // Create instance of DbContext
            var dbContext = new TaskItDbContext(options);

            // Add entities in memory
            dbContext.Seed();

            return dbContext;
        }
    }
}
