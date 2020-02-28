using System.Collections.Generic;
using MediatR;

namespace Billing.App.Features.Invoices
{
    public partial class GetInvoices
    {
        public class Command : IRequest<IEnumerable<Result>> 
        {

        }
    }
}