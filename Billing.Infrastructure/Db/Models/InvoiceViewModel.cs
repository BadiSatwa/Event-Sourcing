using System;
using System.Reflection.Metadata;
using MongoDB.Bson;

namespace Billing.Infrastructure.Db.Models
{
    public class InvoiceViewModel
    {
        public ObjectId Id { get; set; }
        public Guid InvoiceId { get; set; }
        public ExpenseViewModel[] Expenses { get; set; }
    }

    public class ExpenseViewModel
    {

    }
}