
namespace TaskItApi.Repositories.Interfaces
{
    public interface ITaskStatusRepository : IRepositoryBase<Entities.TaskStatus>
    {
        Entities.TaskStatus FindStatusByEnum(Enums.TaskStatuses taskStatuses);
    }
}
