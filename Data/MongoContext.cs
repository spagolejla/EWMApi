using EWMApi.Model;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Task = EWMApi.Model.Task;

namespace EWMApi.Data
{
    public class MongoContext
    {
        private readonly IMongoDatabase _database = null;

        public MongoContext(IOptions<Settings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            if (client != null)
                _database = client.GetDatabase(settings.Value.Database);
        }

        public IMongoCollection<Task> Tasks
        {
            get
            {
                return _database.GetCollection<Task>("task");
            }
        }
    }
}
