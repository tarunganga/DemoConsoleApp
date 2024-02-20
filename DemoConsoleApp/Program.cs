using Microsoft.Extensions.Configuration;

namespace DemoConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IConfiguration config = BuildConfig();
            var poClient = new PurchaseOrderClient(config);
            var po = poClient.GetPurchaseOrder(Guid.Parse("c578292b-ac37-4c74-85a6-e0ecd780438f")).Result;
        }


        private static IConfiguration BuildConfig()
        {

            var configBuilder = new ConfigurationBuilder()
                            .SetBasePath(Directory.GetCurrentDirectory())
                            .AddJsonFile("appsettings.json", false)
                            .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", true);

            return configBuilder.Build();
        }
    }
}
