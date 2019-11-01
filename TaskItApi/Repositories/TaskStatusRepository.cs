using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TaskItApi.Attributes;
using TaskItApi.Extentions;
using TaskItApi.Models;
using TaskItApi.Repositories.Interfaces;

namespace TaskItApi.Repositories
{
    public class TaskStatusRepository : RepositoryBase<Entities.TaskStatus>, ITaskStatusRepository
    {
        public TaskStatusRepository(TaskItDbContext taskItDbContext)
        : base(taskItDbContext)
        {
        }

        public Entities.TaskStatus FindStatusByEnum(Enums.TaskStatuses taskStatuses)
        {
            StringValueAttribute stringValueAttribute = taskStatuses.GetStringValueAttribute();
            Entities.TaskStatus taskStatus = FindByCondition(s => s.Status.Equals(stringValueAttribute.Value)).FirstOrDefault();

            return taskStatus;
        }
    }
}
