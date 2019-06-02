using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TestBase.Helpers;
using TestBase.Services;

namespace TestBase.IoC
{
    /// <summary>
    /// Provides an IoC Container with registered services in it.
    /// </summary>
    public static class IocContainer
    {
        private static readonly AsyncLazy<ServiceProvider> LazyContainer = new AsyncLazy<ServiceProvider>(ConfigureContainer);

        private static ServiceProvider ConfigureContainer()
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile(
                    "appsettings.json",
                    false,
                    true)
                //.AddJsonFile(
                //    $"appsettings.{}.json",
                //    true)
                .Build();

            var serviceCollection = new ServiceCollection();
            RegisterHttpClients(serviceCollection, configuration);
            RegisterServices(serviceCollection);
            return serviceCollection.BuildServiceProvider();
        }

        private static void RegisterHttpClients(IServiceCollection serviceCollection, IConfiguration configuration)
        {
            serviceCollection.AddHttpClient(
                "github",
                c =>
                {
                    c.BaseAddress = new Uri(configuration["URL:GitHub"]);
                    c.DefaultRequestHeaders.Add("Accept", "application/vnd.github.v3+json");
                    c.DefaultRequestHeaders.Add("User-Agent", "HttpClientFactory-Sample");
                });
        }

        private static void RegisterServices(IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<GitHubService>();
        }

        /// <summary>
        /// Asynchronously gets configured container instance.
        /// </summary>
        /// <returns>The type of <see cref="T:Microsoft.Extensions.DependencyInjection.ServiceProvider" /> that is being asynchronously lazily initialized.</returns>
        public static async Task<ServiceProvider> GetContainerAsync()
        {
            // It's enough. You don't need to use '.ConfigureAwait(false)'
            return await LazyContainer.Value.ConfigureAwait(false);
        }
    }
}