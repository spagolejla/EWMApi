using EWMApi.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Task = EWMApi.Model.Task;

namespace EWMApi.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : Controller
    {
        private readonly ITaskRepository _taskRepository;

        public TaskController(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<Task>> Get()
        {
            return await _taskRepository.GetAll();
        }

        [HttpGet("{id}")]
        public async Task<Task> Get(string id)
        {
            return await _taskRepository.Get(id);
        }

        [HttpPost]
        public void Post([FromBody] Task newItem)
        {
            _taskRepository.Post(newItem);
        }

        [HttpPut("{id}")]
        public void Put(string id, [FromBody] Task task)
        {
            _taskRepository.Update(id, task);
        }

        [HttpDelete("{id}")]
        public void Delete(string id)
        {
            _taskRepository.Remove(id);
        }
    }
}
