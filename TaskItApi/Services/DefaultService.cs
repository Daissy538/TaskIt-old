using AutoMapper;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using TaskItApi.Entities;
using TaskItApi.Models.Interfaces;
using TaskItApi.Services.Interfaces;

namespace TaskItApi.Services
{
    public class DefaultService : IDefaultService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger _logger;

        public DefaultService(IMapper mapper, IUnitOfWork unitOfWork, ILogger<IDefaultService> logger)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        /// <summary>
        /// Get all default colors
        /// </summary>
        /// <returns>List of colors</returns>
        public IEnumerable<Color> GetAllColors()
        {
            IEnumerable<Color> colors = _unitOfWork.ColorRepository.GetAllColors();
            return colors;
        }
        /// <summary>
        /// Get all the default icons
        /// </summary>
        /// <returns>List of icons</returns>
        public IEnumerable<Icon> GetAllIcons()
        {
            IEnumerable<Icon> icons = _unitOfWork.IconRepository.GetAllIcons();
            return icons;
        }
    }
}
