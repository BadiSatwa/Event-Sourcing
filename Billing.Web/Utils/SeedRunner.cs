using System.Threading.Tasks;
using Billing.App.Features.Seed;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Billing.Web.Utils
{
    public static class SeedRunner
    {
        public static async Task<IHost> Seed(this IHost host)
        {
            var env = host.Services.GetRequiredService<IHostEnvironment>();
            if (!env.IsDevelopment()) return host;

            using var scope = host.Services.CreateScope();
            var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();

            //Seeding Invoices
            await mediator.Send(new InvoiceSeed.Command());

            return host;
        }
    }
}