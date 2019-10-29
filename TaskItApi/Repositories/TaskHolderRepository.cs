using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskItApi.Entities;
using TaskItApi.Models;
using TaskItApi.Repositories.Interfaces;

namespace TaskItApi.Repositories
{
    public class TaskHolderRepository : RepositoryBase<TaskHolder>, ITaskHolderRepository
    {
        public TaskHolderRepository(TaskItDbContext taskItDbContext)
    : base(taskItDbContext)
        {
        }

        public bool AddHolder(int groupID, int userID)
        {
            throw new NotImplementedException();
        }

        public bool RemoveHolder(int taskHolderID, int userID)
        {
            throw new NotImplementedException();
        }
    }
}
