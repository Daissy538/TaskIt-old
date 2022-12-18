using TaskItApi.Entities;
using TaskItApi.Services.Interfaces;

namespace TaskItApi.Services
{
    public class DefaultService : IDefaultService
    {
        public DefaultService()
        {
           
        }

        public IEnumerable<Color> GetAllColors()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Icon> GetAllIcons()
        {
            throw new NotImplementedException();
        }
    }
}
