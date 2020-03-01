using System;
using System.Linq;
using Billing.Domain.Invoices;
using Shouldly;
using Xunit;

namespace Billing.Domain.Tests
{
    public class InvoiceTests
    {
        [Fact]
        public void Create_EmitInvoiceCreatedEvent()
        {
            //Arrange
            var invoiceId = Guid.NewGuid();

            //Act
            var invoice = new Invoice(new InvoiceId(invoiceId));
            
            //Assert
            invoice.DomainEvents.Count.ShouldBe(1);
            var @event = invoice.DomainEvents.Single();
            @event.AggregateRootId.Id.ShouldBe(invoiceId);
        }
    }
}