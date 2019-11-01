
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public Task FindTaskByID(int taskID)
        {
           return FindByCondition(t => t.ID == taskID).SingleOrDefault();
        }

        public bool UpdateStatus(int taskID, TaskStatus taskStatus)
        {
            try
            {
                Task task = FindByCondition(t => t.ID == taskID).SingleOrDefault();

                task.Status = taskStatus;
                Update(task);
            }catch(Exception exception)
            {
                throw exception;
            }

            return true;
        }

        public bool IsSbuscribed(int taskID, int userID)
        {
           return FindByCondition(t => t.ID == taskID)
                                       .Where(t => t.Group.Members.Where(m => m.UserID == userID).Count() > 0)
                                       .Count() > 0;
        }

        public IEnumerable<Task> FindTasksOfGroup(int groupID)
        {
            return FindByCondition(s => s.GroupID == groupID)
                                .Include(s => s.Group)
                                .ThenInclude(g => g.Color);
        }
    }
}
