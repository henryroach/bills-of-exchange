using BillsOfExchange.DataProvider;
using Microsoft.Extensions.DependencyInjection;

namespace BillsOfExchange.DataProvider.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static void AddDataProviders(this IServiceCollection services)
        {
            services.AddTransient<IPartyRepository, PartyRepository>();
            services.AddTransient<IEndorsementRepository, EndorsementRepository>();
            services.AddTransient<IBillOfExchangeRepository, BillOfExchangeRepository>();
        }
    }
}
