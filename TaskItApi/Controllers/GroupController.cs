using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using TaskItApi.Dtos;
using TaskItApi.Entities;
using TaskItApi.Exceptions;
using TaskItApi.Extentions;
using TaskItApi.Resources;
using TaskItApi.Services.Interfaces;

namespace TaskItApi.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class GroupController : ControllerBase
    {
        private readonly IGroupService _groupService ;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly IStringLocalizer<ApiResponse> _localizer;

        public GroupController(IGroupService groupService, IMapper mapper, ILogger<GroupController> logger, IStringLocalizer<ApiResponse> localizer)
        {
            _groupService = groupService;
            _mapper = mapper;
            _logger = logger;

            _localizer = localizer;
        }

        /// <summary>
        /// Create a group and returns all subscribed groups of user.
        /// </summary>
        [HttpPost]
        [Route("Create")]
        public async Task<ActionResult<IEnumerable<GroupDto>>> Create([FromBody]GroupDto groupDto)
        {
            int userId = HttpContext.User.GetCurrentUserId();
            try
            { 
                IEnumerable<Group>groups =_groupService.Create(groupDto, userId);
                IEnumerable<GroupDto> groupDtos = _mapper.Map<IEnumerable<GroupDto>>(groups);

                return Ok(groupDtos);
            }
            catch (InvalidInputException invalidInputException)
            {
                _logger.LogInformation($"User {userId} could not create group", groupDto, invalidInputException);
                return BadRequest(_localizer["CreateGroup_Error"].Value);
            }
            catch (Exception exception)
            {
                _logger.LogError($"User {userId} could not create group", groupDto, exception);
                return StatusCode(500, _localizer["InternalError"].Value);
            }
        }

        /// <summary>
        /// Delete a group and returns all subscribed groups of user.
        /// </summary>
        [HttpDelete]
        [Route("Delete/{id:int}")]
        public async Task<ActionResult<IEnumerable<GroupDto>>> Delete(int id)
        {
            int userId = HttpContext.User.GetCurrentUserId();

            try
            {  
                IEnumerable<Group> groups = _groupService.Delete(id, userId);
                IEnumerable<GroupDto> groupDtos = _mapper.Map<IEnumerable<GroupDto>>(groups);

                return Ok(groupDtos);
            }
            catch (InvalidInputException invalidInputException)
            {
                _logger.LogInformation($"User {userId} could not delete groupd with id: {id}", invalidInputException);
                return BadRequest(_localizer["DeleteGroup_Error"].Value);
            }
            catch(Exception exception)
            {
                _logger.LogError($"User {userId} could not delete groupd with id: {id}", exception);
                return StatusCode(500, _localizer["InternalError"].Value);
            }
        }

        /// <summary>
        /// Returns all subscribed groups of user.
        /// </summary>
        [HttpGet]
        [Route("All")]
        public async Task<ActionResult<IEnumerable<GroupDto>>> GetGroups()
        {
            int userId = HttpContext.User.GetCurrentUserId();
            try
            {   
                IEnumerable<Group> groups = _groupService.GetGroups(userId);
                IEnumerable<GroupDto> groupDtos = _mapper.Map<IEnumerable<GroupDto>>(groups);

                return Ok(groupDtos);
            }
            catch (Exception exception)
            {
                _logger.LogError($"User {userId} could not retrieve subscribed groups", exception);
                return StatusCode(500, _localizer["InternalError"].Value);
            }
        }

    }
}