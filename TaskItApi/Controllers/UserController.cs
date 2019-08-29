
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using TaskItApi.Dtos;
using TaskItApi.Services.NewFolder;

namespace TaskItApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IAuthenticationService _authenicationService;

        public UserController(IUserService userService
                              ,IAuthenticationService authenticationService)
        {
            _userService = userService;
            _authenicationService = authenticationService;
        }

        /// <summary>
        /// Register a new account to the system.
        /// </summary>
        [HttpPost]
        [AllowAnonymous]
        [Route("Register")]
        public ActionResult<UserOutGoingDto> Register([FromBody]UserInComingDto userInComingData)
        {
            try
            {
                _authenicationService.RegisterUser(userInComingData);
                return null;
            }catch(ArgumentException argumentException)
            {
                return BadRequest(argumentException.Message);
            }
            catch
            {
                return StatusCode(500, "Internal server error");
            }          
        }

        /// <summary>
        /// Authenicate user with a JWT token
        /// </summary>
        [HttpPost]
        [AllowAnonymous]
        [Route("Auth")]
        public ActionResult<string> Authenticate ([FromBody]UserInComingDto userInComingData)
        {
            try
            {
                string token = _authenicationService.AuthenicateUser(userInComingData);
                var response = new
                {
                    token = token
                };
                return OkResult(response);
            }catch
            {
               return BadRequest("Invalid email and/or password");
            }
        }

        /// <summary>
        /// Updat the given user account
        /// </summary>
        [HttpPost]
        [Route("{id:int}/Update")]
        public ActionResult<UserOutGoingDto> Update(int Id)
        {
            return null;
        }

        /// <summary>
        /// Delete the given user
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("{id:int}/Delete")]
        public ActionResult Delete(int Id)
        {
            return null;
        }
    }
}