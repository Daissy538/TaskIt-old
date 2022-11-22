using System.Collections.Generic;
using TaskItApi.Entities;

namespace TaskIt.Domain.RepositoryInterfaces
{
    public interface IIconRepository : IRepositoryBase<Icon>
    {
        /// <summary>
        /// Get all default icon
        /// </summary>
        /// <returns>List of icons</returns>
        IEnumerable<Icon> GetAllIcons();
        /// <summary>
        /// Get icon by value
        /// </summary>
        /// <param name="iconID"></param>
        /// <returns>the icon based on the given value</returns>
        Icon GetIconByValue(int iconID);
    }
}
