﻿using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using TaskItApi.Entities;
using TaskItApi.Models;
using TaskItApi.Repositories.Interfaces;

namespace TaskItApi.Repositories
{
    public class IconRepository : RepositoryBase<Icon>, IIconRepository
    {
        private readonly ILogger _logger;

        public IconRepository(TaskItDbContext taskItDbContext, ILogger<IIconRepository> logger)
            : base(taskItDbContext)
        {
            _logger = logger;
        }

        public IEnumerable<Icon> GetAllIcons()
        {
            IEnumerable<Icon> defaultIcons = FindAll();
            return defaultIcons;
        }

        public Icon GetIconByValue(int iconID)
        {
            Icon icon = FindByCondition(c => c.ID == iconID).FirstOrDefault();
            return icon;
        }
    }
}
