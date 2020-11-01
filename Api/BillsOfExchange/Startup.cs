using Autofac;
using Autofac.Extensions.DependencyInjection;
using Autofac.Extras.DynamicProxy;
using BillsOfExchange.DataProvider;
using BillsOfExchange.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace BillsOfExchange
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddOptions();
            services.AddControllers();
            services.AddLogging();
            services.AddAutofac(ConfigureContainer);
            services.AddSwaggerGen();
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterType<LogInterceptor>();

            builder.RegisterType<PartyRepository>().As<IPartyRepository>().EnableInterfaceInterceptors();
            builder.RegisterType<BillOfExchangeRepository>().As<IBillOfExchangeRepository>()
                .EnableInterfaceInterceptors();
            builder.RegisterType<EndorsementRepository>().As<IEndorsementRepository>().EnableInterfaceInterceptors();

            builder.RegisterType<BillsOfExchangeService>().As<IBillsOfExchangeService>();
            builder.RegisterType<EndorsementService>().As<IEndorsementService>();
            builder.RegisterType<PartyService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseMiddleware<RequestLoggingMiddleware>();
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(builder => {
                builder.AllowAnyOrigin();
                builder.AllowAnyMethod();
                builder.AllowAnyHeader();
            });
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1"));
            app.ApplicationServices.GetAutofacRoot();

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}