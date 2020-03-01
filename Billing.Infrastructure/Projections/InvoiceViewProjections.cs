using System.Threading;
using System.Threading.Tasks;
using Billing.Domain.Invoices;
using Billing.Infrastructure.Db;
using Billing.Infrastructure.Db.Models;
using MediatR;
using MongoDB.Driver;

namespace Billing.Infrastructure.Projections
{
    public class InvoiceViewProjections : INotificationHandler<InvoiceCreated>,
        INotificationHandler<ExpenseAdded>
    {
        private readonly IMongoCollection<InvoiceViewModel> _invoiceCollection;

        public InvoiceViewProjections(DbContext context)
        {
            _invoiceCollection = context.GetInvoicesCollection();
        }

        public Task Handle(InvoiceCreated notification, CancellationToken cancellationToken) =>
            _invoiceCollection.InsertOneAsync(new InvoiceViewModel
            {
                InvoiceId = notification.AggregateRootId.Id
            }, cancellationToken: cancellationToken);

        public Task Handle(ExpenseAdded notification, CancellationToken cancellationToken) =>
            _invoiceCollection.UpdateOneAsync(
                i => i.InvoiceId == notification.AggregateRootId.Id,
                Builders<InvoiceViewModel>.Update.Push(i => i.Expenses, new ExpenseViewModel()), 
                cancellationToken: cancellationToken);
    }
}