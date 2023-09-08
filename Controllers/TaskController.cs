using EWMApi.Data;
using EWMApi.Interfaces;
using EWMApi.Model;
using EWMApi.Model.Enums;
using Microsoft.AspNetCore.Authorization;
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
        private readonly ITimesheetRepository _timesheetRepository;
        private readonly IProjectRepository _projectRepository;
        private readonly IEmployeeRepository _employeeRepository;


        public TaskController(ITaskRepository taskRepository, ITimesheetRepository timesheetRepository, IProjectRepository projectRepository, IEmployeeRepository employeeRepository)
        {
            _taskRepository = taskRepository;
            _timesheetRepository = timesheetRepository;
            _projectRepository = projectRepository;
            _employeeRepository = employeeRepository;
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

        [HttpGet("activeTask/{id}")]
        public async Task<Task> GetActiveTask(string id)
        {
            return await _taskRepository.GetActiveTask(id);
        }

        [HttpGet("project/{id}")]
        public async Task<IEnumerable<Task>> GetByProjectId(string id)
        {
            return await _taskRepository.GetByProjectId(id);
        }

        [HttpGet("user/{id}")]
        public async Task<IEnumerable<Task>> GetByUserId(string id)
        {
            return await _taskRepository.GetByUserId(id);
        }

        [HttpGet("dashboard-data")]
        public async Task<DashboardData> GetDashboardData()
        {
            var tasks = await _taskRepository.GetAll();
            var projects = await _projectRepository.GetAll();
            var employees = await _employeeRepository.GetAll();

            var dashboardData = new DashboardData()
            {
                NumberOfTasks = tasks.Count(),
                NumberOfActiveTasks = tasks.Count(x => x.Status == EWMApi.Model.Enums.TaskStatus.InProgress),
                NumberOfProjects = projects.Count(),
                NumberOfOpenProjects = projects.Count(x => x.Status == Model.Enums.ProjectStatus.Open),
                NumberOfEmployees = employees.Count(),
                NumberOfActiveEmployees = employees.Count(x => x.Active == true)
            };

            return dashboardData;
        }

        [HttpPost]
        public void Post([FromBody] Task newItem)
        {
            _taskRepository.Post(newItem);
        }

        [HttpPut()]
        public void Put([FromBody] Task task)
        {
            _taskRepository.Update(task.Id.ToString(), task);
        }

        [HttpPut("startTask")]
        public async void StartTaskFromMobile([FromBody] Task task)
        {
            task.Status = Model.Enums.TaskStatus.InProgress;

            var timesheet = await _timesheetRepository.GetUserTimesheetByDate(DateTime.Now, task.Assigner.Id);

            var newWorkPeriod = new WorkPeriod()
            {
                Id = Guid.NewGuid().ToString(),
                Start = DateTime.Now,
                Task = task
            };

            if (timesheet != null)
            {
                timesheet.WorkPeriods.Add(newWorkPeriod);
                timesheet.Status = TimesheetStatus.Open;
                _timesheetRepository.Update(timesheet.Id, timesheet);
            }
            else
            {
                var newTimesheet = new Timesheet()
                {
                    User = task.Assigner,
                    Status = TimesheetStatus.Open,
                    Date = DateTime.Now,
                    Actions = new List<TimesheetAction>(),
                    WorkPeriods = new List<WorkPeriod>(),

                };

                newTimesheet.WorkPeriods.Add(newWorkPeriod);

                _timesheetRepository.Post(newTimesheet);
            };

            _taskRepository.Update(task.Id.ToString(), task);
        }


        [HttpPut("stopTask")]
        public async void StopTaskFromMobile([FromBody] Task task)
        {
            //task.Status = Model.Enums.TaskStatus.InProgress; // TODO: add status on FE

            var timesheet = await _timesheetRepository.GetUserTimesheetByDate(DateTime.Now, task.Assigner.Id);

            if (timesheet != null)
            {
                var newWorkPeriod = timesheet.WorkPeriods.Where(workPeriod => workPeriod.End == null).FirstOrDefault();

                if (newWorkPeriod != null)
                {
                    newWorkPeriod.End = DateTime.UtcNow;
                    TimeSpan ts = newWorkPeriod.End.Value - newWorkPeriod.Start;
                    newWorkPeriod.TotalHours = ts.TotalHours;
                    timesheet.TotalHours = timesheet.TotalHours != null ? timesheet.TotalHours + ts.TotalHours : ts.TotalHours;
                }

                timesheet.Status = TimesheetStatus.Open;


                _timesheetRepository.Update(timesheet.Id, timesheet);
            }

            _taskRepository.Update(task.Id.ToString(), task);
        }

        [HttpDelete("{id}")]
        public void Delete(string id)
        {
            _taskRepository.Remove(id);
        }
    }
}
