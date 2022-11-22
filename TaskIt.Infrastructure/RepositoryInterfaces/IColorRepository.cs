using TaskItApi.Entities;

namespace TaskIt.Domain.RepositoryInterfaces
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
