using System;
using Microsoft.Extensions.Configuration;

namespace TestBase.Helpers
{
    public static class ConfigHelper
    {
        public static readonly IConfigurationRoot Configuration;

        static ConfigHelper()
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