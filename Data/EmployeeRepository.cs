using EWMApi.Interfaces;
using EWMApi.Model;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace EWMApi.Data
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly MongoContext _context;

        public EmployeeRepository(IOptions<Settings> settings)
        {
            _context = new MongoContext(settings);
        }

        public async Task<IEnumerable<Employee>> GetAll()
        {
            try
            {
                return await _context.Employees.Find(_ => true).ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Employee> Get(string id)
        {
            try
            {
                return await _context.Employees
                                .Find(employee => employee.Id.ToString() == id)
                                .FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async System.Threading.Tasks.Task Post(Employee employee)
        {
            try
            {
                employee.Id = (Guid.NewGuid()).ToString();
                await _context.Employees.InsertOneAsync(employee);
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
                DeleteResult actionResult = await _context.Employees.DeleteOneAsync(
                     Builders<Employee>.Filter.Eq("Id", id));

                return actionResult.IsAcknowledged
                    && actionResult.DeletedCount > 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> Update(string id, Employee employee)
        {
            var filter = Builders<Employee>.Filter.Eq(s => s.Id, id);
            var update = Builders<Employee>.Update
                            .Set(s => s.FirstName, employee.FirstName)
                            .Set(s => s.LastName, employee.LastName)
                            .Set(s => s.Email, employee.Email)
                            .Set(s => s.Password, employee.Password)
                            .Set(s => s.Telephone, employee.Telephone)
                            .Set(s => s.Position, employee.Position)
                            .Set(s => s.StartWorkDate, employee.StartWorkDate)
                            .Set(s => s.ShortDescription, employee.ShortDescription)
                            .Set(s => s.SalaryPerHour, employee.SalaryPerHour)
                            .Set(s => s.Active, employee.Active)
                            .Set(s => s.Picture, employee.Picture)
                            .Set(s => s.AvatarUrl, employee.AvatarUrl);


            try
            {
                UpdateResult actionResult = await _context.Employees.UpdateOneAsync(filter, update);

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
                DeleteResult actionResult = await _context.Employees.DeleteManyAsync(new BsonDocument());

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
