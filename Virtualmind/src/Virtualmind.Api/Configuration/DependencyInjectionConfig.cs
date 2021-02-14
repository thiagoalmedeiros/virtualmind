using Microsoft.Extensions.DependencyInjection;
using Virtualmind.Api.Data;
using Virtualmind.Api.Model;
using Virtualmind.Api.Services;
using Virtualmind.Api.Services.Interfaces;

namespace Virtualmind.Api.Configuration
{
    public static  class DependencyInjectionConfig
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services) {

            services.AddScoped<DataContext>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IQuotationService, QuotationService>();
            services.AddScoped<ITransactionService, TransactionService>();
            return services;
        }
    }
}
