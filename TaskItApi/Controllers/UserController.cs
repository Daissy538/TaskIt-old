
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
        public ActionResult<UserDto> Register([FromBody]UserInComingDto userInComingData)
        {
            _userService.CreateUser(userInComingData);

            return null;
        }

        /// <summary>
        /// Authenicate user with a JWT token
        /// </summary>
        [HttpPost]
        [AllowAnonymous]
        [Route("Auth")]
        public ActionResult<string> Authenticate ()
        {
            return null;
        }

        /// <summary>
        /// Updat the given user account
        /// </summary>
        [HttpPost]
        [Route("{id:int}/Update")]
        public ActionResult<UserDto> Update(int Id)
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