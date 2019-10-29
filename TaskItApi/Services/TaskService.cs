using AutoMapper;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using TaskItApi.Dtos.Api.Incoming;
using TaskItApi.Entities;
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

        public IEnumerable<Task> CreateTask(TaskIncomingDTO taskIncoming)
        {
            Task token = new Task() { }

            _unitOfWork.TaskRepository.Create();
        }

        public bool FinishTask(int taskID, int userID)
        {
            throw new System.NotImplementedException();
        }
    }
}
