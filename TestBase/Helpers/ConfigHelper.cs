using System;
using Microsoft.Extensions.Configuration;

namespace TestBase.Helpers
{
    public static class ConfigHelper
    {
        public static readonly IConfigurationRoot Config;

        static ConfigHelper()
        {
            Config = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile(
                    "Configuration.json",
                    false,
                    true)
                .Build();
        }
    }
}