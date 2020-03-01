using System.Threading.Tasks;
using Billing.App;
using Billing.Infrastructure.Db.Models;
using MongoDB.Driver;

namespace Billing.Infrastructure.Db
{
    public class DbContext : IStorage
    {
        private const string InvoicesCollectionName = "invoices";

        private readonly IMongoDatabase _database;

        public DbContext(MongoClient mongoClient)
        {
            _database = mongoClient.GetDatabase("Billing");
        }

        public IMongoCollection<InvoiceViewModel> GetInvoicesCollection() => _database.GetCollection<InvoiceViewModel>(InvoicesCollectionName);
        
        public async Task ClearStorage()
        {
            await _database.DropCollectionAsync(InvoicesCollectionName);
        }
    }
}