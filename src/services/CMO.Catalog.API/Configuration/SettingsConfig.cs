using CMO.Product.API.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace CMO.Product.API.Configuration
{
    public static class SettingsConfig
    {
        public static IServiceCollection AddSettingsConfiguration(this IServiceCollection services, IConfiguration configuration)
        {

            var connectionSettings = new ConnectionSettings();
            new ConfigureFromConfigurationOptions<ConnectionSettings>(configuration.GetSection("ConnectionStrings")).Configure(connectionSettings);
            services.AddSingleton(connectionSettings);

            return services;
        }

    }
}