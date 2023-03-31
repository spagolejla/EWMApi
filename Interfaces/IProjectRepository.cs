using EWMApi.Model;

namespace EWMApi.Interfaces
{
    public interface IProjectRepository
    {
        Task<IEnumerable<Project>> GetAll();

        Task<Project> Get(string id);

        System.Threading.Tasks.Task Post(Project item);

        Task<bool> Update(string id, Project item);

        Task<bool> Remove(string id);

        Task<bool> RemoveAll();
    }
}
