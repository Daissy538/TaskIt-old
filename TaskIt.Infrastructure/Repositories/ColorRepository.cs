using Microsoft.Extensions.Logging;
using TaskIt.Domain.RepositoryInterfaces;
using TaskItApi.Entities;
using TaskItApi.Models;

namespace TaskItApi.Repositories
{
    public class ColorRepository : RepositoryBase<Color>, IColorRepository
    {
        private readonly ILogger _logger;

        public ColorRepository(TaskItDbContext taskItDbContext, ILogger<IColorRepository> logger)
            : base(taskItDbContext)
        {
            _logger = logger;
        }

        public IEnumerable<Color> GetAllColors()
        {
            IEnumerable<Color> defaultColors = FindAll();
            return defaultColors;
        }

        public Color GetColorByValue(int colorID)
        {
            Color color = FindByCondition(c => c.ID == colorID).FirstOrDefault();
            return color;
        }
    }
}
