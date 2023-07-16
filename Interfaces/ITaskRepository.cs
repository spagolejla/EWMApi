using EWMApi.Model;
using Task = EWMApi.Model.Task;

namespace EWMApi.Interfaces
{
    public interface ITaskRepository
    {
        Task<IEnumerable<Task>> GetAll();

        Task<Task> Get(string id);

        Task<Task> GetActiveTask(string id);

        Task<IEnumerable<Task>> GetByProjectId(string projectId);

        Task<IEnumerable<Task>> GetByUserId(string userId);

        System.Threading.Tasks.Task Post(Task taks);

        Task<bool> Update(string id, Task task);

        Task<bool> Remove(string id);

        Task<bool> RemoveAll();

    }
}
