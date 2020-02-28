using System.Collections.Generic;
using Billing.App;
using Billing.App.Features.Invoices;
using Billing.Domain.Invoices;
using Billing.Infrastructure;
using Billing.Infrastructure.Db;
using Billing.Infrastructure.EventStore;
using Billing.Infrastructure.Projections;
using Billing.Infrastructure.Queries;
using EventStore.ClientAPI;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MongoDB.Driver;

namespace Billing.Web
{
    public class Startup
    {
        private readonly string _corsPolicyName = "localDevelopmentCors";
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
                options.AddPolicy(_corsPolicyName, builder => builder.WithOrigins("http://localhost:3000")));
            services.AddMediatR(typeof(IRepository<,>));
            services.AddControllers();

            services.AddScoped(typeof(IRepository<,>), typeof(AggregateRootRepository<,>));
            services.AddScoped(typeof(IDomainEventsDispatcher<>), typeof(MediatrEventDispatcher<>));
            services.AddScoped(typeof(IEventStore<>), typeof(EventStore<>));

            services.AddSingleton(_ => new MongoClient("mongodb://root:example@localhost:27018"));
            services.AddScoped<DbContext>();

            services.AddScoped(
                typeof(IViewModelQuery<Empty, IEnumerable<GetInvoices.Result>>),
                typeof(GetInvoicesQuery));

            services.AddSingleton( _ =>
            {
                var connection = EventStoreConnection
                    .Create("ConnectTo=tcp://admin:changeit@localhost:11113; HeartBeatTimeout=500");
                connection.ConnectAsync().Wait();
                return connection;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseCors(_corsPolicyName);

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
