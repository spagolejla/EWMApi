using EWMApi.Model;

namespace EWMApi.Interfaces
{
    public interface ITimesheetRepository
    {
        Task<IEnumerable<Timesheet>> GetAll();

        Task<Timesheet> Get(string id);

        Task<IEnumerable<Timesheet>> GetByDate(DateTime date);

        Task<Timesheet> GetUserTimesheetByDate(DateTime date, string userId);

        System.Threading.Tasks.Task Post(Timesheet item);

        Task<bool> Update(string id, Timesheet item);

        Task<bool> Remove(string id);

        Task<bool> RemoveAll();
    }
}
