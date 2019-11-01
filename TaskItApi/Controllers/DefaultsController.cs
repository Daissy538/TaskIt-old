using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using TaskItApi.Dtos;
using TaskItApi.Entities;
using TaskItApi.Extentions;
using TaskItApi.Resources;
using TaskItApi.Services.Interfaces;

namespace TaskItApi.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class DefaultsController : ControllerBase
    {
        private readonly IDefaultService _defaultService;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly IStringLocalizer<ApiResponse> _localizer;

        public DefaultsController(IDefaultService defaultService, IMapper mapper, ILogger<DefaultsController> logger, IStringLocalizer<ApiResponse> localizer, IStringLocalizer<Colors> localizerColors)
        {
            _defaultService = defaultService;
            _mapper = mapper;
            _logger = logger;

            _localizer = localizer;
        }

        /// <summary>
        /// Returns all default colors
        /// </summary>
        [HttpGet]
        [Route("Colors")]
        public async Task<ActionResult<IEnumerable<ColorDTO>>> GetColors()
        {    
            try
            {
                IEnumerable<Color> colors = _defaultService.GetAllColors();
                IEnumerable<ColorDTO> response = _mapper.Map<IEnumerable<ColorDTO>>(colors);
                return Ok(response);
            }
            catch (Exception exception)
            {
                int userId = HttpContext.User.GetCurrentUserId();

                _logger.LogError($"User {userId} could not retrieve default colors", exception);
                return StatusCode(500, _localizer["InternalError"].Value);
            }

        }

        /// <summary>
        /// Returns all default colors
        /// </summary>
        [HttpGet]
        [Route("Icons")]
        public async Task<ActionResult<IEnumerable<IconDTO>>> GetIcons()
        {          
            try
            {
                IEnumerable<Icon> icons = _defaultService.GetAllIcons();
                IEnumerable<IconDTO> response = _mapper.Map<IEnumerable<IconDTO>>(icons);
                return Ok(response);
            }
            catch (Exception exception)
            {
                int userId = HttpContext.User.GetCurrentUserId();

                _logger.LogError($"User {userId} could not retrieve default icons", exception);
                return StatusCode(500, _localizer["InternalError"].Value);
            }
        }

    }
}