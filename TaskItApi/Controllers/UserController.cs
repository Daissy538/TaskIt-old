
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using TaskItApi.Dtos;
using TaskItApi.Exceptions;
using TaskItApi.Services.Interfaces;

namespace TaskItApi.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IAuthenticationService _authenicationService;
        private readonly ILogger<UserController> _logger;

        public UserController(IUserService userService
                              ,IAuthenticationService authenticationService, ILogger<UserController> logger)
        {
            _userService = userService;
            _authenicationService = authenticationService;
            _logger = logger;
        }

        /// <summary>
        /// Register a new account to the system.
        /// </summary>
        [HttpPost]
        [AllowAnonymous]
        [Route("Register")]
        public async Task<ActionResult<UserOutGoingDto>> Register([FromBody]UserInComingDto userInComingData)
        {
            try
            {
                _authenicationService.RegisterUser(userInComingData);
                return Ok();
            }catch(InvalidInputException invalidInputException)
            {
                _logger.LogInformation($"Could not register user", userInComingData, invalidInputException);
                return BadRequest("Could not register user");
            }
            catch(Exception exception)
            {
                _logger.LogError($"Could not register user", userInComingData, exception);
                return StatusCode(500, "Internal server error");
            }          
        }

        /// <summary>
        /// Authenicate user with a JWT token
        /// </summary>
        [HttpPost]
        [AllowAnonymous]
        [Route("Auth")]
        public async Task<ActionResult<string>> Authenticate ([FromBody]UserInComingDto userInComingData)
        {
            try
            {
                TokenDto token =  _authenicationService.AuthenicateUser(userInComingData);
                
                return Ok(token);
            }
            catch (InvalidInputException invalidInputException)
            {
                _logger.LogInformation($"Could not authenitcate user", userInComingData, invalidInputException);
                return BadRequest("Invalid email and/or password");
            }
            catch (Exception exception)
            {
                _logger.LogError($"Could not authenitcate user", userInComingData, exception);
                return StatusCode(500, "Internal server error");
            }
        }

        /// <summary>
        /// Updat the given user account
        /// </summary>
        [HttpPost]
        [Route("{id:int}/Update")]
        public async Task<ActionResult<UserOutGoingDto>> Update(int Id)
        {
            return null;
        }

        /// <summary>
        /// Delete the given user
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("{id:int}/Delete")]
        public async Task<ActionResult> Delete(int Id)
        {
            return null;
        }
    }
}