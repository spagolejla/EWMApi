using EWMApi.Interfaces;
using EWMApi.Model;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace EWMApi.Data
{
    public class TimesheetRepository : ITimesheetRepository
    {
        private readonly MongoContext _context;

        public TimesheetRepository(IOptions<Settings> settings)
        {
            _context = new MongoContext(settings);
        }

        public async Task<IEnumerable<Timesheet>> GetAll()
        {
            try
            {
                return await _context.Timesheets.Find(_ => true).ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Timesheet> Get(string id)
        {
            try
            {
                return await _context.Timesheets
                                .Find(timesheet => timesheet.Id.ToString() == id)
                                .FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async System.Threading.Tasks.Task Post(Timesheet timesheet)
        {
            try
            {
                timesheet.Id = (Guid.NewGuid()).ToString();
                await _context.Timesheets.InsertOneAsync(timesheet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> Remove(string id)
        {
            try
            {
                DeleteResult actionResult = await _context.Timesheets.DeleteOneAsync(
                     Builders<Timesheet>.Filter.Eq("Id", id));

                return actionResult.IsAcknowledged
                    && actionResult.DeletedCount > 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> Update(string id, Timesheet timesheet)
        {
            var filter = Builders<Timesheet>.Filter.Eq(s => s.Id, id);
            var update = Builders<Timesheet>.Update
                            .Set(s => s.Date, timesheet.Date)
                            .Set(s => s.User, timesheet.User)
                            .Set(s => s.Status, timesheet.Status)
                            .Set(s => s.TotalHours, timesheet.TotalHours)
                            .Set(s => s.WorkPeriods, timesheet.WorkPeriods)
                            .Set(s => s.Actions, timesheet.Actions);

            try
            {
                UpdateResult actionResult = await _context.Timesheets.UpdateOneAsync(filter, update);

                return actionResult.IsAcknowledged
                    && actionResult.ModifiedCount > 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> RemoveAll()
        {
            try
            {
                DeleteResult actionResult = await _context.Timesheets.DeleteManyAsync(new BsonDocument());

                return actionResult.IsAcknowledged
                    && actionResult.DeletedCount > 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private ObjectId GetInternalId(string id)
        {
            if (!ObjectId.TryParse(id, out ObjectId internalId))
                internalId = ObjectId.Empty;

            return internalId;
        }

        public async Task<IEnumerable<Timesheet>> GetByDate(DateTime date)
        {
            try
            {
                return await _context.Timesheets
                                .Find(timesheet => (timesheet.Date.Day == date.Date.Day)
                                && (timesheet.Date.Month == date.Date.Month)
                                && (timesheet.Date.Year == date.Date.Year))
                                .ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Timesheet> GetUserTimesheetByDate(DateTime date, string userId)
        {
            try
            {
                return await _context.Timesheets
                                .Find(timesheet => (timesheet.Date.Day == date.Date.Day)
                                && (timesheet.Date.Month == date.Date.Month)
                                && (timesheet.Date.Year == date.Date.Year)
                                && (timesheet.User.Id == userId))
                                .FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
