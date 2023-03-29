using Task = EWMApi.Model.Task;

namespace EWMApi.Interfaces
{
    public interface ITaskRepository
    {
        Task<IEnumerable<Task>> GetAll();

        Task<Task> Get(string id);

        System.Threading.Tasks.Task Post(Task taks);

        Task<bool> Update(string id, Task task);

        Task<bool> Remove(string id);

        Task<bool> RemoveAll();

    }
}
