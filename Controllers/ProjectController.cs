using EWMApi.Interfaces;
using EWMApi.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EWMApi.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : Controller
    {
        private readonly IProjectRepository _projectRepository;

        public ProjectController(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<Project>> Get()
        {
            return await _projectRepository.GetAll();
        }

        [HttpGet("{id}")]
        public async Task<Project> Get(string id)
        {
            return await _projectRepository.Get(id);
        }

        [HttpPost]
        public void Post([FromBody] Project newItem)
        {
            _projectRepository.Post(newItem);
        }

        [HttpPut()]
        public void Put([FromBody] Project item)
        {
            _projectRepository.Update(item.Id.ToString(), item);
        }

        [HttpDelete("{id}")]
        public void Delete(string id)
        {
            _projectRepository.Remove(id);
        }
    }
}
