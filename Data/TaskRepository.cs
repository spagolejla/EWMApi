using EWMApi.Interfaces;
using EWMApi.Model;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using Task = EWMApi.Model.Task;

namespace EWMApi.Data
{
    public class TaskRepository : ITaskRepository
    {
        private readonly MongoContext _context;

        public TaskRepository(IOptions<Settings> settings)
        {
            _context = new MongoContext(settings);
        }

        public async Task<IEnumerable<Task>> GetAll()
        {
            try
            {
                return await _context.Tasks.Find(_ => true).ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Task> Get(string id)
        {
            try
            {
                return await _context.Tasks
                                .Find(task => task.Id.ToString() == id)
                                .FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async System.Threading.Tasks.Task Post(Task task)
        {
            try
            {
                var latestNo = _context.Tasks.Find(_ => true).ToList().OrderByDescending(task=>task.TaskNo).Select(task => task.TaskNo).Take(1).First();
                task.Id = (Guid.NewGuid()).ToString();
                task.TaskNo = latestNo + 1;
                await _context.Tasks.InsertOneAsync(task);
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

        public async Task<bool> Remove(string id)
        {
            try
            {
                DeleteResult actionResult = await _context.Tasks.DeleteOneAsync(
                     Builders<Task>.Filter.Eq("Id", id));

                return actionResult.IsAcknowledged
                    && actionResult.DeletedCount > 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> Update(string id, Task task)
        {
            var filter = Builders<Task>.Filter.Eq(s => s.Id, id);
            var update = Builders<Task>.Update
                            .Set(s => s.Title, task.Title)
                            .Set(s => s.Description, task.Description)
                            .Set(s => s.StartDate, task.StartDate)
                            .Set(s => s.EndDate, task.EndDate)
                            .Set(s => s.Assigner, task.Assigner)
                            .Set(s => s.Project, task.Project)
                            .Set(s => s.Status, task.Status)
                            .Set(s => s.Priority, task.Priority);

            try
            {
                UpdateResult actionResult = await _context.Tasks.UpdateOneAsync(filter, update);

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
                DeleteResult actionResult = await _context.Tasks.DeleteManyAsync(new BsonDocument());

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
    }
}
