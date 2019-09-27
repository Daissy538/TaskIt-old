using System;
using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TaskItApi.Dtos;
using TaskItApi.Entities;
using TaskItApi.Exceptions;
using TaskItApi.Extentions;
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

        public GroupController(IGroupService groupService, IMapper mapper, ILogger<GroupController> logger)
        {
            _groupService = groupService;
            _mapper = mapper;
            _logger = logger;
        }

        /// <summary>
        /// Create a group and returns all subscribed groups of user.
        /// </summary>
        [HttpPost]
        [Route("Create")]
        public ActionResult<IEnumerable<GroupDto>> Create([FromBody]GroupDto groupDto)
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
                _logger.LogInformation($"User {userId} could not create groupd", groupDto, invalidInputException);
                return BadRequest("Could not create group");
            }
            catch (Exception exception)
            {
                _logger.LogError($"User {userId} could not create group", groupDto, exception);
                return StatusCode(500, "Internal server error");
            }
        }

        /// <summary>
        /// Delete a group and returns all subscribed groups of user.
        /// </summary>
        [HttpDelete]
        [Route("Delete/{id:int}")]
        public ActionResult<IEnumerable<GroupDto>> Delete(int id)
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
                return BadRequest("Could not delete group");
            }
            catch(Exception exception)
            {
                _logger.LogError($"User {userId} could not delete groupd with id: {id}", exception);
                return StatusCode(500, "Internal server error");
            }
        }

        /// <summary>
        /// Returns all subscribed groups of user.
        /// </summary>
        [HttpGet]
        [Route("All")]
        public ActionResult<IEnumerable<GroupDto>> GetGroups()
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
                return StatusCode(500, "Internal server error");
            }
        }

    }
}