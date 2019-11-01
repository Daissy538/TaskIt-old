using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using TaskItApi.Dtos.Api.Incoming;
using TaskItApi.Entities;
using TaskItApi.Enums;
using TaskItApi.Exceptions;
using TaskItApi.Models.Interfaces;
using TaskItApi.Services.Interfaces;

namespace TaskItApi.Services
{
    public class TaskService : ITaskService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger _logger;

        public TaskService(IMapper mapper, IUnitOfWork unitOfWork, ILogger<IGroupService> logger)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public Task AddTaskHolder(int taskID, int userID)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Task> CreateTask(TaskIncomingDTO taskIncoming, int userID)
        {
            bool userSubscribed = _unitOfWork.GroupRepository.IsSubscribed(taskIncoming.GroupID, userID);

            if (!userSubscribed)
            {
                _logger.LogError($"Could not create task {taskIncoming}: User {userID} is not subscribed on group");
                throw new InvalidInputException("User is not subscribe on group");
            }

            Task task = _mapper.Map<Task>(taskIncoming);

            TaskStatus taskStatus = _unitOfWork.TaskStatusRepository.FindStatusByEnum(TaskStatuses.Open);
            task.StatusID = taskStatus.ID;

            _unitOfWork.TaskRepository.Create(task);
            _unitOfWork.SaveChanges();

            IEnumerable<Task> currentTasks = _unitOfWork.TaskRepository.FindTasksOfGroup(taskIncoming.GroupID);
            return currentTasks;
        }

        public bool FinishTask(int taskID, int userID)
        {
            bool userSubscribed = _unitOfWork.TaskRepository.IsSbuscribed(taskID, userID);

            if (!userSubscribed)
            {
                _logger.LogError($"Could not finish task {taskID}: User {userID} is not subscribed on group");
                throw new InvalidInputException("User is not subscribe on group");
            }

            TaskStatus taskStatus = _unitOfWork.TaskStatusRepository.FindStatusByEnum(TaskStatuses.Finished);

            _unitOfWork.TaskRepository.UpdateStatus(taskID, taskStatus);
            _unitOfWork.SaveChanges();

            return true;
        }

        /// <summary>
        /// Get all tasks where the user is holder of
        /// </summary>
        /// <param name="userID">Active user id</param>
        /// <returns>The list of tasks</returns>
        public IEnumerable<Task> GetTaskOfHolder(int userID)
        {
            IEnumerable<Task> tasks = _unitOfWork.TaskRepository.FindAll().Where(t => t.TaskHolders.Where(h => h.UserID == userID).Count() > 0).ToList();

            return tasks;
        }

        /// <summary>
        /// Get all tasks that is part of a group.
        /// Only retrieves tasks if the user is subscribed to the group
        /// </summary>
        /// <param name="groupID">group id </param>
        /// <param name="userID">the active user id</param>
        /// <returns>The list of tasks</returns>
        public IEnumerable<Task> GetTasksOfGroup(int groupID, int userID)
        {
            Group group = _unitOfWork.GroupRepository.FindGroupOfUser(groupID, userID);

            if (group != default(Group))
            {
                _logger.LogError($"Could not retrieve tasks of group {groupID}: User {userID} is not subscribed on group");
                throw new InvalidInputException("User is not subscribe on group");
            }

            return group.Tasks;
        }

        /// <summary>
        /// Get all tasks of groups where the user is subscribed on
        /// </summary>
        /// <param name="userID">Active user id</param>
        /// <returns>The list of tasks</returns>
        public IEnumerable<Task> GetTasksOfUser(int userID)
        {
            IEnumerable<Task> tasks = _unitOfWork.GroupRepository.FindAllGroupOfUser(userID).SelectMany(g => g.Tasks);

            return tasks;
        }
    }
}
