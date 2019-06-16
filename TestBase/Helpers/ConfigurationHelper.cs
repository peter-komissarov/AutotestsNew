using System;
using Microsoft.Extensions.Configuration;

namespace TestBase.Helpers
{
    public static class ConfigurationHelper
    {
        public static IConfigurationRoot TestConfig;

        static ConfigurationHelper()
        {
            TestConfig = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile(
                    "Configuration.json",
                    false,
                    true)
                .Build();
        }
    }
}
