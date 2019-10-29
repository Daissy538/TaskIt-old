
using System.Collections.Generic;
using TaskItApi.Dtos.Api.Incoming;
using TaskItApi.Entities;

namespace TaskItApi.Services.Interfaces
{
    public interface ITaskService
    {
        bool FinishTask(int taskID, int userID);

        IEnumerable<Task> CreateTask(TaskIncomingDTO taskIncoming);

        Task AddTaskHolder(int taskID,int userID);
    }
}
