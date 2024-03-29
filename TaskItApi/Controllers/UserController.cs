﻿
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using TaskIt.Core.Dtos;
using TaskItApi.Dtos;
using TaskItApi.Exceptions;
using TaskItApi.Resources;
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
        private readonly IStringLocalizer<ApiResponse> _localizer;
        private readonly IMapper _mapper;

        public UserController(IUserService userService
                              , IAuthenticationService authenticationService, ILogger<UserController> logger, IStringLocalizer<ApiResponse> localizer, IMapper mapper)
        {
            _userService = userService;
            _authenicationService = authenticationService;
            _logger = logger;
            _localizer = localizer;
            _mapper = mapper;
        }

        /// <summary>
        /// Register a new account to the system.
        /// </summary>
        [HttpPost]
        [AllowAnonymous]
        [Route("Register")]
        public async Task<ActionResult<UserOutGoingDto>> Register(UserInComingDto userInComingData)
        {
            try
            {
                await _authenicationService.RegisterUserAsync(userInComingData.Name, userInComingData.Email, userInComingData.Password);
                return Ok();
            }catch(InvalidInputException invalidInputException)
            {
                _logger.LogInformation($"Could not register user", userInComingData, invalidInputException);
                return BadRequest(_localizer["RegisterUser_Error"].Value);
            }
            catch(Exception exception)
            {
                _logger.LogError($"Could not register user", userInComingData, exception);
                return StatusCode(500, _localizer["InternalError"].Value);
            }          
        }

        /// <summary>
        /// Authenicate user with a JWT token
        /// </summary>
        [HttpPost]
        [AllowAnonymous]
        [Route("Auth")]
        public async Task<ActionResult<string>> Authenticate (UserInComingDto userInComingData)
        {
            try
            {
                var request = this._mapper.Map<AuthenticateDto>(userInComingData);
                string token =  await _authenicationService.AuthenticateUser(request);
                
                return Ok(token);
            }
            catch (InvalidInputException invalidInputException)
            {
                _logger.LogInformation($"Could not authenitcate user", userInComingData, invalidInputException);
                return BadRequest(_localizer["AuthenticateUser_Error"].Value);
            }
            catch (Exception exception)
            {
                _logger.LogError($"Could not authenitcate user", userInComingData, exception);
                return StatusCode(500, _localizer["InternalError"].Value);
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