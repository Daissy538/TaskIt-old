using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskItApi.Dtos;
using TaskItApi.Entities;
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
        /// Create a group and returns all subscribte groups of user.
        /// </summary>
        [HttpPost]
        [Route("Create")]
        public ActionResult<IEnumerable<GroupDto>> Create([FromBody]GroupDto groupDto)
        {
            try
            {
                ClaimsIdentity claims = HttpContext.User.Identity as ClaimsIdentity;
                string userId = claims.Claims.Where(c => c.Type == ClaimTypes.NameIdentifier).FirstOrDefault().Value;
                IEnumerable<Group>groups =_groupService.Create(groupDto, Convert.ToInt32(userId));

                IEnumerable<GroupDto> groupDtos = this._mapper.Map<IEnumerable<GroupDto>>(groups);
                return Ok(groupDtos);
            }
            catch
            {
                return StatusCode(500, "Internal server error");
            }
        }

        /// <summary>
        /// Delete a group and returns all subscribte groups of user.
        /// </summary>
        [HttpPost]
        [Route("Delete/{id:int}")]
        public ActionResult<IEnumerable<GroupDto>> Delete(int id)
        {
            try
            {
                string userName = User.Identity.Name;
                IEnumerable<Group> groups = _groupService.Delete(id, userName);

                IEnumerable<GroupDto> groupDtos = this._mapper.Map<IEnumerable<GroupDto>>(groups);
                return Ok(groupDtos);
            }
            catch
            {
                return StatusCode(500, "Internal server error");
            }
        }

    }
}