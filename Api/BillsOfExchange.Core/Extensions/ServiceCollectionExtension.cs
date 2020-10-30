using AutoMapper;
using BillsOfExchange.Core.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace BillsOfExchange.Core.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static void AddCore(this IServiceCollection services)
        {
            // Auto Mapper Configurations
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);

            // Services dependencies
            services.AddTransient<IPartyService, PartyService>();
            services.AddTransient<IBillOfExchangeService, BillOfExchangeService>();
        }
    }
}
