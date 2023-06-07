﻿using EWMApi.Interfaces;
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