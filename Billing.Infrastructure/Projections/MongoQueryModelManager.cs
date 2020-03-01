using System.Threading.Tasks;
using Billing.App;

namespace Billing.Infrastructure.Projections
{
    public class MongoQueryModelManager : IQueryModelManager
    {
        private readonly IStorage _storage;

        public MongoQueryModelManager(IStorage storage)
        {
            _storage = storage;
        }

        public async Task RecreateModel()
        {
            await _storage.ClearStorage();
        }
    }
}