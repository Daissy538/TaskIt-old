using System.Collections.Generic;
using TaskItApi.Entities;

namespace TaskItApi.Repositories.Interfaces
{
    public interface IColorRepository : IRepositoryBase<Color>
    {
        /// <summary>
        /// Get all default colors
        /// </summary>
        /// <returns>List of colors</returns>
        IEnumerable<Color> GetAllColors();
        /// <summary>
        /// Get 
        /// </summary>
        /// <param name="colorID"></param>
        /// <returns></returns>
        Color GetColorByValue(int colorID);
    }
}
