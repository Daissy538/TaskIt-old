using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using TaskItApi.Dtos;
using TaskItApi.Dtos.Api;
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
        private readonly IGroupService _groupService;
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
        public async Task<ActionResult<IEnumerable<GroupOutgoingDTO>>> Create(GroupIncomingDTO groupDto)
        {
            int userId = HttpContext.User.GetCurrentUserId();

            try
            {
                Group group = _mapper.Map<Group>(groupDto);

                IEnumerable<Group> groups = await _groupService.CreateAsync(group, userId);
                IEnumerable<GroupOutgoingDTO> groupDtos = _mapper.Map<IEnumerable<GroupOutgoingDTO>>(groups);

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
        public async Task<ActionResult<IEnumerable<GroupOutgoingDTO>>> Delete(int id)
        {
            int userId = HttpContext.User.GetCurrentUserId();

            try
            {
                IEnumerable<Group> groups = await _groupService.DeleteAsync(id, userId);
                IEnumerable<GroupOutgoingDTO> groupDtos = _mapper.Map<IEnumerable<GroupOutgoingDTO>>(groups);

                return Ok(groupDtos);
            }
            catch (InvalidInputException invalidInputException)
            {
                _logger.LogInformation($"User {userId} could not delete groupd with id: {id}", invalidInputException);
                return BadRequest(_localizer["DeleteGroup_Error"].Value);
            }
            catch (Exception exception)
            {
                _logger.LogError($"User {userId} could not delete groupd with id: {id}", exception);
                return StatusCode(500, _localizer["InternalError"].Value);
            }
        }

        /// <summary>
        /// Create a group and returns all subscribed groups of user.
        /// </summary>
        [HttpPost]
        [Route("Update/{id}")]
        public async Task<ActionResult<GroupOutgoingDTO>> Update(int id, [FromBody]GroupIncomingDTO groupDto)
        {
            int userId = HttpContext.User.GetCurrentUserId();

            try
            {
                Group groupRequest = _mapper.Map<Group>(groupDto);
                Group group = await _groupService.UpdateAsync(groupRequest, userId);
                GroupOutgoingDTO response = _mapper.Map<GroupOutgoingDTO>(group);

                return Ok(response);
            }
            catch (InvalidInputException invalidInputException)
            {
                _logger.LogInformation($"User {userId} could not update groupd with id: {id}", invalidInputException);
                return BadRequest(_localizer["DeleteGroup_Error"].Value);
            }
            catch (Exception exception)
            {
                _logger.LogError($"User {userId} could not update groupd with id: {id}", exception);
                return StatusCode(500, _localizer["InternalError"].Value);
            }
        }

        /// <summary>
        /// Returns all subscribed groups of user.
        /// </summary>
        [HttpGet]
        [Route("All")]
        public async Task<ActionResult<IEnumerable<GroupOutgoingDTO>>> GetGroups()
        {
            int userId = HttpContext.User.GetCurrentUserId();

            try
            {
                IEnumerable<Group> groups = await _groupService.GetGroupsAsync(userId);
                IEnumerable<GroupOutgoingDTO> groupDtos = _mapper.Map<IEnumerable<GroupOutgoingDTO>>(groups);

                return Ok(groupDtos);
            }
            catch (Exception exception)
            {
                _logger.LogError($"User {userId} could not retrieve subscribed groups", exception);
                return StatusCode(500, _localizer["InternalError"].Value);
            }
        }

        /// <summary>
        /// Return group details based on id
        /// </summary>
        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<GroupOutgoingDTO>> GetGroup(int id)
        {
            int userId = HttpContext.User.GetCurrentUserId();

            try
            {
                Group group = await _groupService.GetGroup(id, userId);
                GroupOutgoingDTO result = _mapper.Map<GroupOutgoingDTO>(group);

                return Ok(result);
            }
            catch (Exception exception)
            {
                _logger.LogError($"User {userId} could not get group with id: {id}", exception);
                return StatusCode(500, _localizer["InternalError"].Value);
            }

        }

        /// <summary>
        /// Subscribe user to a group
        /// </summary>
        [HttpPost]
        [Route("{ID}/Invite")]
        public async Task<ActionResult<bool>> InviteUserToGroup( int ID, [FromBody]InviteIncomingDTO inviteIncoming)
        {
            int userId = HttpContext.User.GetCurrentUserId();

            try
            {
                _groupService.InviteUserToGroup(userId, inviteIncoming.RecievingMail, ID);
                return Ok();
            }
            catch(InvalidInputException invalidInputException)
            {
                _logger.LogWarning($"Could not send invite email", invalidInputException);
                return Ok(); //to preserve the privacy of the users. There will not be error message send when input is invalid.
            }
            catch(Exception exception)
            {
                _logger.LogError($"User {userId} could not send invite email for group: {ID} to: {inviteIncoming.RecievingMail}", exception);
                return StatusCode(500, _localizer["InternalError"].Value);
            }   
        }

        /// <summary>
        /// Subscribe user to a group
        /// </summary>
        [HttpPost]
        [Route("Subscribe")]
        public async Task<ActionResult<bool>> SubscribeToGroup([FromBody] TokenDto tokenDTO)
        {
            int userID = HttpContext.User.GetCurrentUserId();

            try
            {
                _groupService.SubscribeToGroup(userID, tokenDTO.Token);
                return Ok(true);
            }
            catch(Exception exception)
            {
                return BadRequest(_localizer["SubscribeGroup_Error"].Value);
            }            
        }

        [HttpDelete]
        [Route("{ID}/Unsubscribe")]
        public async Task<ActionResult<Boolean>> Unsubscribe(int ID)
        {
            int userID = HttpContext.User.GetCurrentUserId();

            try
            {
                _groupService.Unsubscribe(userID, ID);
                return Ok(true);
            }
            catch (InvalidOperationException invalidOperationException)
            {
                _logger.LogWarning($"Could unsubscribe user {userID}", invalidOperationException);
                return BadRequest(_localizer["UnsubscribeGroup_Error_UserMim"].Value);
            }catch(Exception exception)
            {
                _logger.LogWarning($"Could unsubscribe user {userID}", exception);
                return StatusCode(500, _localizer["InternalError"].Value);
            }
        }
    }
}