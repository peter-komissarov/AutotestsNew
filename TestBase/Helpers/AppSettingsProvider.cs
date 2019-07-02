using System;
using Microsoft.Extensions.Configuration;

namespace TestBase.Helpers
{
    public static class AppSettingsProvider
    {
        public static readonly IConfigurationRoot Configuration;

        static AppSettingsProvider()
        {
            Configuration = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile(
                    "AppSettings.json",
                    false,
                    true)
                .Build();
        }
    }
}