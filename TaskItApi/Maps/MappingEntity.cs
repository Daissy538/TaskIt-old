

using AutoMapper;
using TaskItApi.Dtos;
using TaskItApi.Entities;

namespace TaskItApi.Maps
{
    public class MappingEntity: Profile
    {
       public MappingEntity()
        {
            CreateMap<UserInComingDto, User>();
        }
    }
}
