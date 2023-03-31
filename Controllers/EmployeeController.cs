using EWMApi.Interfaces;
using EWMApi.Model;
using Microsoft.AspNetCore.Mvc;

namespace EWMApi.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : Controller
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeController(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<Employee>> Get()
        {
            return await _employeeRepository.GetAll();
        }

        [HttpGet("{id}")]
        public async Task<Employee> Get(string id)
        {
            return await _employeeRepository.Get(id);
        }

        [HttpPost]
        public void Post([FromBody] Employee newItem)
        {
            _employeeRepository.Post(newItem);
        }

        [HttpPut()]
        public void Put([FromBody] Employee item)
        {
            _employeeRepository.Update(item.Id.ToString(), item);
        }

        [HttpDelete("{id}")]
        public void Delete(string id)
        {
            _employeeRepository.Remove(id);
        }
    }
}
