using System.Collections.Generic;
using TaskItApi.Entities;

namespace TaskItApi.Services.Interfaces
{
    /// <summary>
    /// Service for default values that can be  used by the api clients
    /// </summary>
    public interface IDefaultService
    {
        /// <summary>
        /// Get all default colors
        /// </summary>
        /// <returns>List of colors</returns>
        public IEnumerable<Color> GetAllColors();
        /// <summary>
        /// Get all the default icons
        /// </summary>
        /// <returns>List of icons</returns>
        public IEnumerable<Icon> GetAllIcons();
    }
}
