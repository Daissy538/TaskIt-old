
using System.Collections.Generic;
using TaskItApi.Dtos.Api.Incoming;
using TaskItApi.Entities;

namespace TaskItApi.Services.Interfaces
{
    public interface ITaskService
    {
        /// <summary>
        /// Finish task 
        /// </summary>
        /// <param name="taskID">The task id</param>
        /// <param name="userID">the active user id</param>
        /// <returns></returns>
        bool FinishTask(int taskID, int userID);

        /// <summary>
        /// Create task for group. Can only create a task for a group if the user is subscribed on it.
        /// </summary>
        /// <param name="taskIncoming">the task data</param>
        /// <param name="UserID">The active user</param>
        /// <returns></returns>
        IEnumerable<Task> CreateTask(TaskIncomingDTO taskIncoming, int UserID);

        /// <summary>
        /// Get all tasks that is part of a group.
        /// Only retrieves tasks if the user is subscribed to the group
        /// </summary>
        /// <param name="groupID">group id </param>
        /// <param name="userID">the active user id</param>
        /// <returns>The list of tasks</returns>
        IEnumerable<Task> GetTasksOfGroup(int groupID, int userID);

        /// <summary>
        /// Get all tasks of groups where the user is subscribed on
        /// </summary>
        /// <param name="userID">Active user id</param>
        /// <returns>The list of tasks</returns>
        IEnumerable<Task> GetTasksOfUser(int userID);

        /// <summary>
        /// Get all tasks where the user is holder of
        /// </summary>
        /// <param name="userID">Active user id</param>
        /// <returns>The list of tasks</returns>
        IEnumerable<Task> GetTaskOfHolder(int userID);

        Task AddTaskHolder(int taskID,int userID);
    }
}
