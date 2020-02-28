using MediatR;

namespace Billing.App.Features.Invoices
{
    public partial class CreateInvoice
    {
        public class Command : IRequest<Result>
        {

        }
    }
}