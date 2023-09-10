using EWMApi.Model;

namespace EWMApi.Interfaces
{
    public interface IInvoiceRepository
    {
        Task<IEnumerable<Invoice>> GetAll();

        Task<Invoice> Get(string id);

        Task<IEnumerable<Invoice>> GetByUser(string userId);

        System.Threading.Tasks.Task Post(Invoice item);

        Task<bool> Update(string id, Invoice item);

        Task<bool> Remove(string id);

        Task<bool> RemoveAll();
    }
}
