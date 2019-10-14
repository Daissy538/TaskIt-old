using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using TaskItApi.Entities;
using TaskItApi.Models;
using TaskItApi.Repositories.Interfaces;

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
