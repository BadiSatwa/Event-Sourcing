using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Billing.App.Features.Invoices;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Billing.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceController : ControllerBase
    {
        private readonly IMediator _mediator;

        public InvoiceController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IEnumerable<GetInvoices.Result>> Get()
        {
            //await _mediator.Send(new AddExpense.Command() { Id = new InvoiceId(Guid.Parse("f2ad3cb4-fe5e-4ae9-aab3-3e4c297af949")) });
            return await _mediator.Send(new GetInvoices.Command());
        }

        [HttpGet]
        [Route("{invoiceId}")]
        public async Task<GetInvoice.Result> GetInvoice(Guid invoiceId) =>
            await _mediator.Send(new GetInvoice.Command {InvoiceId = invoiceId});

        [HttpPost]
        public async Task<CreateInvoice.Result> Create() => await _mediator.Send(new CreateInvoice.Command());

        [HttpPost]
        [Route("{invoiceId}/expense")]
        public async Task AddExpense(Guid invoiceId) =>
            await _mediator.Send(new AddExpense.Command { InvoiceId = invoiceId });
    }
}