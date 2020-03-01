using System.Collections.Generic;
using Billing.App;
using Billing.App.Features.Invoices;
using Billing.Infrastructure;
using Billing.Infrastructure.Db;
using Billing.Infrastructure.EventStore;
using Billing.Infrastructure.Queries;
using EventStore.ClientAPI;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
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
            services.AddMediatR(typeof(IRepository<,>), typeof(AggregateRootRepository<,>));
            services.AddControllers();

            services.AddScoped(typeof(IRepository<,>), typeof(AggregateRootRepository<,>));
            services.AddScoped(typeof(IDomainEventsDispatcher<>), typeof(MediatrEventDispatcher<>));
            services.AddScoped(typeof(IEventStore<>), typeof(EventStore<>));

            //Db Configuration
            services.Configure<DbOptions>(Configuration.GetSection("Db"));
            services.AddSingleton(s =>
            {
                var dbOptions = s.GetRequiredService<IOptions<DbOptions>>().Value;
                var builder = new MongoUrlBuilder
                {
                    Server = new MongoServerAddress(dbOptions.Address, dbOptions.Port),
                    Username = dbOptions.UserName,
                    Password = dbOptions.Password
                };
                return new MongoClient(builder.ToMongoUrl());
            });
            services.AddScoped<DbContext>();

            //Event Store Configuration
            services.Configure<EventStoreOptions>(Configuration.GetSection("EventStore"));
            services.AddSingleton(s =>
            {
                var options = s.GetRequiredService<IOptions<EventStoreOptions>>().Value;
                var connection = EventStoreConnection
                    .Create($"ConnectTo=tcp://{options.UserName}:{options.Password}@{options.Address}:{options.Port};");
                connection.ConnectAsync().Wait();
                return connection;
            });

            //Queries
            services.AddScoped(
                typeof(IViewModelQuery<Empty, IEnumerable<GetInvoices.Result>>),
                typeof(GetInvoicesQuery));
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
