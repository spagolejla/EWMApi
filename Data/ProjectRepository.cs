using EWMApi.Interfaces;
using EWMApi.Model;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace EWMApi.Data
{
    public class ProjectRepository: IProjectRepository
    {
        private readonly MongoContext _context;

        public ProjectRepository(IOptions<Settings> settings)
        {
            _context = new MongoContext(settings);
        }

        public async Task<IEnumerable<Project>> GetAll()
        {
            try
            {
                return await _context.Projects.Find(_ => true).ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Project> Get(string id)
        {
            try
            {
                return await _context.Projects
                                .Find(project => project.Id.ToString() == id)
                                .FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async System.Threading.Tasks.Task Post(Project project)
        {
            try
            {
                project.Id = (Guid.NewGuid()).ToString();
                await _context.Projects.InsertOneAsync(project);
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
                DeleteResult actionResult = await _context.Projects.DeleteOneAsync(
                     Builders<Project>.Filter.Eq("Id", id));

                return actionResult.IsAcknowledged
                    && actionResult.DeletedCount > 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> Update(string id, Project project)
        {
            var filter = Builders<Project>.Filter.Eq(s => s.Id, id);
            var update = Builders<Project>.Update
                            .Set(s => s.Name, project.Name)
                            .Set(s => s.Description, project.Description)
                            .Set(s => s.StartDate, project.StartDate)
                            .Set(s => s.EndDate, project.EndDate)
                            .Set(s => s.BudgetAmount, project.BudgetAmount)
                            .Set(s => s.HasTicketsAssigned, project.HasTicketsAssigned)
                            .Set(s => s.Status, project.Status)
                            .Set(s => s.Location, project.Location);

            try
            {
                UpdateResult actionResult = await _context.Projects.UpdateOneAsync(filter, update);

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
                DeleteResult actionResult = await _context.Projects.DeleteManyAsync(new BsonDocument());

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
