using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskItApi.Dtos;
using TaskItApi.Entities;
using TaskItApi.Extentions;
using TaskItApi.Services.Interfaces;

namespace TaskItApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class GroupController : ControllerBase
    {
        private readonly IGroupService _groupService ;
        private readonly IMapper _mapper;

        public GroupController(IGroupService groupService, IMapper mapper)
        {
            _groupService = groupService;
            _mapper = mapper;
        }

        /// <summary>
        /// Create a group and returns all subscribed groups of user.
        /// </summary>
        [HttpPost]
        [Route("Create")]
        public ActionResult<IEnumerable<GroupDto>> Create([FromBody]GroupDto groupDto)
        {
            try
            {
                int userId = HttpContext.User.GetCurrentUserId();

                IEnumerable<Group>groups =_groupService.Create(groupDto, userId);
                IEnumerable<GroupDto> groupDtos = _mapper.Map<IEnumerable<GroupDto>>(groups);

                return Ok(groupDtos);
            }
            catch
            {
                return BadRequest("Could not create requ");
            }
        }

        /// <summary>
        /// Delete a group and returns all subscribed groups of user.
        /// </summary>
        [HttpDelete]
        [Route("Delete/{id:int}")]
        public ActionResult<IEnumerable<GroupDto>> Delete(int id)
        {
            try
            {
                int userId = HttpContext.User.GetCurrentUserId();

                IEnumerable<Group> groups = _groupService.Delete(id, userId);
                IEnumerable<GroupDto> groupDtos = _mapper.Map<IEnumerable<GroupDto>>(groups);

                return Ok(groupDtos);
            }
            catch
            {
                return BadRequest("Could not delete group");
            }
        }

        /// <summary>
        /// Returns all subscribed groups of user.
        /// </summary>
        [HttpGet]
        [Route("All")]
        public ActionResult<IEnumerable<GroupDto>> GetGroups()
        {
            try
            {
                int userId = HttpContext.User.GetCurrentUserId();

                IEnumerable<Group> groups = _groupService.GetGroups(userId);
                IEnumerable<GroupDto> groupDtos = _mapper.Map<IEnumerable<GroupDto>>(groups);

                return Ok(groupDtos);
            }
            catch
            {
                return StatusCode(500, "Internal server error");
            }
        }

    }
}