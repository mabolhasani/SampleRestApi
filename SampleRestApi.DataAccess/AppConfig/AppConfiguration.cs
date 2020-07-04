using System.IO;
using Microsoft.Extensions.Configuration;

namespace SampleRestApi.DataAccess.AppConfig
{
    internal class AppConfiguration
    {
        public static string GetConnectionString()
        {
            ConfigurationBuilder configurationBuilder = new ConfigurationBuilder();
            string path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
            configurationBuilder.AddJsonFile(path, false);

            IConfigurationRoot root = configurationBuilder.Build();

            var dsds= root.GetSection("connectionStrings").GetSection("SampleDb").Value;
            return root.GetSection("connectionStrings").GetSection("SampleDb").Value;
        }
    }
}
