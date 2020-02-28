using Billing.Infrastructure.Db.Models;
using MongoDB.Driver;

namespace Billing.Infrastructure.Db
{
    public class DbContext
    {
        private readonly IMongoDatabase _database;

        public DbContext(MongoClient mongoClient)
        {
            _database = mongoClient.GetDatabase("Billing");
        }

        public IMongoCollection<InvoiceViewModel> GetInvoicesCollection() => _database.GetCollection<InvoiceViewModel>("invoices");
    }
}