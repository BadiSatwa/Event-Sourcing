using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Billing.App;
using Billing.App.Features.Invoices;
using Billing.Infrastructure.Db;
using Billing.Infrastructure.Db.Models;
using MongoDB.Driver;

namespace Billing.Infrastructure.Queries
{
    public class GetInvoicesQuery : IViewModelQuery<Empty, IEnumerable<GetInvoices.Result>>
    {
        private readonly IMongoCollection<InvoiceViewModel> _collection;

        public GetInvoicesQuery(DbContext context)
        {
            _collection = context.GetInvoicesCollection();
        }

        public async Task<IEnumerable<GetInvoices.Result>> Execute(Empty arg)
        {
            var invoices = await _collection.Find(_ => true).ToListAsync();
            return invoices.Select(i => new GetInvoices.Result
            {
                InvoiceId = i.InvoiceId,
                NumberOfExpenses = (i.Expenses?.Length).GetValueOrDefault(0)
            });
        }
    }
}