using TaskItApi.Entities;

namespace TaskItApi.Repositories.Interfaces
{
    public interface ITaskRepository : IRepositoryBase<Task>
    {
        bool FinishTask(int TaskID);

    }
}
