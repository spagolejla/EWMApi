using EWMApi.Interfaces;
using EWMApi.Model;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace EWMApi.Data
{
    public class InvoiceRepository : IInvoiceRepository
    {
        private readonly MongoContext _context;

        public InvoiceRepository(IOptions<Settings> settings)
        {
            _context = new MongoContext(settings);
        }

        public async Task<IEnumerable<Invoice>> GetAll()
        {
            try
            {
                return await _context.Invoices.Find(_ => true).ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Invoice> Get(string id)
        {
            try
            {
                return await _context.Invoices
                                .Find(invoice => invoice.Id.ToString() == id)
                                .FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async System.Threading.Tasks.Task Post(Invoice invoice)
        {
            try
            {
                invoice.Id = (Guid.NewGuid()).ToString();
                await _context.Invoices.InsertOneAsync(invoice);
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
                DeleteResult actionResult = await _context.Invoices.DeleteOneAsync(
                     Builders<Invoice>.Filter.Eq("Id", id));

                return actionResult.IsAcknowledged
                    && actionResult.DeletedCount > 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> Update(string id, Invoice invoice)
        {
            var filter = Builders<Invoice>.Filter.Eq(s => s.Id, id);
            var update = Builders<Invoice>.Update
                            .Set(s => s.Date, invoice.Date)
                            .Set(s => s.Employee, invoice.Employee)
                            .Set(s => s.Month, invoice.Month)
                            .Set(s => s.TotalHours, invoice.TotalHours)
                            .Set(s => s.TotalCost, invoice.TotalCost)
                            .Set(s => s.Timesheets, invoice.Timesheets);

            try
            {
                UpdateResult actionResult = await _context.Invoices.UpdateOneAsync(filter, update);

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
                DeleteResult actionResult = await _context.Invoices.DeleteManyAsync(new BsonDocument());

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


        public async Task<IEnumerable<Invoice>> GetByUser(string userId)
        {
            try
            {
                return await _context.Invoices
                                .Find(invoice => (invoice.Employee.Id == userId))
                                .ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
