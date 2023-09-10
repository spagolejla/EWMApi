using EWMApi.Interfaces;
using EWMApi.Model;
using Microsoft.AspNetCore.Mvc;

namespace EWMApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceController : Controller
    {
        private readonly IInvoiceRepository _invoiceRepository;

        public InvoiceController(IInvoiceRepository invoiceRepository)
        {
            _invoiceRepository = invoiceRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<Invoice>> Get()
        {
            return await _invoiceRepository.GetAll();
        }

        [HttpGet("getById/{id}")]
        public async Task<Invoice> Get(string id)
        {
            return await _invoiceRepository.Get(id);
        }


        [HttpGet("getByUser")]
        public async Task<IEnumerable<Invoice>> GetByUser([FromQuery(Name = "userId")] string userId)
        {
            return await _invoiceRepository.GetByUser(userId);
        }

        [HttpPost]
        public void Post([FromBody] Invoice newItem)
        {
            _invoiceRepository.Post(newItem);
        }

        [HttpPut()]
        public void Put([FromBody] Invoice item)
        {
            _invoiceRepository.Update(item.Id.ToString(), item);
        }

        [HttpDelete("{id}")]
        public void Delete(string id)
        {
            _invoiceRepository.Remove(id);
        }
    }
}
