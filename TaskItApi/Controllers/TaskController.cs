using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TaskItApi.Dtos.Api;
using TaskItApi.Dtos.Api.Incoming;
using TaskItApi.Exceptions;
using TaskItApi.Extentions;
using TaskItApi.Resources;
using TaskItApi.Services.Interfaces;

namespace TaskItApi.Controllers
{
    public class TaskController : ControllerBase
    {
        private readonly ITaskService _taskService;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly IStringLocalizer<ApiResponse> _localizer;

        public TaskController(ITaskService taskService, IMapper mapper, ILogger<TaskController> logger, IStringLocalizer<ApiResponse> localizer)
        {
            _taskService = taskService;
            _mapper = mapper;
            _logger = logger;

            _localizer = localizer;
        }

        /// <summary>
        /// Create task for a group
        /// </summary>
        [HttpPost]
        [Route("Create")]
        public async Task<ActionResult<IEnumerable<TaskOutgoingDTO>>> Create([FromBody]TaskIncomingDTO taskIncomingDTO)
        {
            int userId = HttpContext.User.GetCurrentUserId();

            try
            {
               IEnumerable<Entities.Task> tasks = _taskService.CreateTask(taskIncomingDTO);
               IEnumerable<TaskOutgoingDTO> result = _mapper.Map<IEnumerable<TaskOutgoingDTO>>(tasks); 
                return Ok(result);
            }
            catch (InvalidInputException invalidInputException)
            {
                _logger.LogInformation($"User {userId} could not create task", taskIncomingDTO, invalidInputException);
                return BadRequest(_localizer["CreateGroup_Error"].Value);
            }
            catch (Exception exception)
            {
                _logger.LogError($"User {userId} could not create task", taskIncomingDTO, exception);
                return StatusCode(500, _localizer["InternalError"].Value);
            }
        }
    }
}
 