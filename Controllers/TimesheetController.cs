using EWMApi.Interfaces;
using EWMApi.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EWMApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TimesheetController : Controller
    {
        private readonly ITimesheetRepository _timesheetRepository;

        public TimesheetController(ITimesheetRepository timesheetRepository)
        {
            _timesheetRepository = timesheetRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<Timesheet>> Get()
        {
            return await _timesheetRepository.GetAll();
        }

        [HttpGet("{id}")]
        public async Task<Timesheet> Get(string id)
        {
            return await _timesheetRepository.Get(id);
        }

        [HttpGet("getByDate")]
        public async Task<IEnumerable<Timesheet>> GetByDate([FromQuery(Name = "date")] DateTime date)
        {
            return await _timesheetRepository.GetByDate(date);
        }

        [HttpGet("getUserTimesheetByDate/{date}/{userId}")]
        public async Task<Timesheet> GetUserTimesheetByDate(DateTime date, string userId)
        {
            return await _timesheetRepository.GetUserTimesheetByDate(date, userId);
        }

        [HttpGet("getTimesheetByDate")]
        public async Task<Timesheet> GetTimesheetByDate([FromQuery(Name = "date")] DateTime date)
        {
            var userId = "00ee6e49-dd17-41a2-be30-14a2d8d5c7dd"; // TODO: Change to logged in user
            return await _timesheetRepository.GetUserTimesheetByDate(date, userId);
        }

        [HttpPost]
        public void Post([FromBody] Timesheet newItem)
        {
            _timesheetRepository.Post(newItem);
        }

        [HttpPut()]
        public void Put([FromBody] Timesheet item)
        {
            _timesheetRepository.Update(item.Id.ToString(), item);
        }

        [HttpDelete("{id}")]
        public void Delete(string id)
        {
            _timesheetRepository.Remove(id);
        }
    }
}
