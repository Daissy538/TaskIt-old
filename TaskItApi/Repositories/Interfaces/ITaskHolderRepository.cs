using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskItApi.Entities;

namespace TaskItApi.Repositories.Interfaces
{
    public interface ITaskHolderRepository : IRepositoryBase<TaskHolder>
    {
        bool AddHolder(int groupID, int userID);

        bool RemoveHolder(int taskHolderID, int userID);


    }
}
