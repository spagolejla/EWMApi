using EWMApi.Interfaces;
using EWMApi.Model;
using Microsoft.AspNetCore.Authorization;
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

        [AllowAnonymous]
        [Route("authenticate")]
        [HttpPost]
        public async Task<ActionResult> Login([FromBody] UserLogin user)
        {
            var token = await _employeeRepository.Authenticate(user.Email, user.Password);

            if (token == null)
            {
                return Unauthorized();
            }

            var loggedUser = await _employeeRepository.GetByEmail(user.Email);
            return Ok(new { token, loggedUser });
        }

        [Authorize]
        [HttpGet]
        public async Task<IEnumerable<Employee>> Get()
        {
            try
            {
                return await _employeeRepository.GetAll();

            }
            catch (Exception e)
            {

                throw e;
            }
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<Employee> Get(string id)
        {
            return await _employeeRepository.Get(id);
        }

        [Authorize]
        [HttpPost]
        public void Post([FromBody] Employee newItem)
        {
            _employeeRepository.Post(newItem);
        }

        [Authorize]
        [HttpPut()]
        public void Put([FromBody] Employee item)
        {
            _employeeRepository.Update(item.Id.ToString(), item);
        }

        [Authorize]
        [HttpDelete("{id}")]
        public void Delete(string id)
        {
            _employeeRepository.Remove(id);
        }
    }
}
