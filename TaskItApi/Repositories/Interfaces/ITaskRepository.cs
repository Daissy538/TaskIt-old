using System.Collections.Generic;
using TaskItApi.Entities;

namespace TaskItApi.Repositories.Interfaces
{
    public interface ITaskRepository : IRepositoryBase<Task>
    {
        /// <summary>
        /// Update the task status
        /// </summary>
        /// <param name="taskID">The task</param>
        /// <param name="taskStatus">the new task status</param>
        /// <returns>true if status is updated</returns>
        bool UpdateStatus(int taskID, TaskStatus taskStatus);

        /// <summary>
        /// Find task by taskID
        /// </summary>
        /// <param name="taskID">the task id</param>
        /// <returns>The task that has been found</returns>
        Task FindTaskByID(int taskID);

        /// <summary>
        /// Check if the user is subscribed to the group where the task is part of
        /// </summary>
        /// <param name="taskID">The task</param>
        /// <param name="userID">The active user</param>
        /// <returns>True if subscribed, false otherwise</returns>
        bool IsSbuscribed(int taskID, int userID);

        /// <summary>
        /// Find all tasks of given group
        /// </summary>
        /// <param name="groupID">the group</param>
        /// <returns>the list of tasks</returns>
        IEnumerable<Task> FindTasksOfGroup(int groupID);
    }
}
