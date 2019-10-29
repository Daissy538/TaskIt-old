
using System;
using TaskItApi.Entities;
using TaskItApi.Models;
using TaskItApi.Repositories.Interfaces;

namespace TaskItApi.Repositories
{
    public class TaskRepository : RepositoryBase<Task>, ITaskRepository
    {
        public TaskRepository(TaskItDbContext taskItDbContext)
    : base(taskItDbContext)
        {
        }

        public bool FinishTask(int TaskID)
        {
            throw new NotImplementedException();
        }
    }
}
