using System.IO;
using Microsoft.Extensions.Configuration;

namespace SendPush
{
    public static class Startup
    {
        public static IConfiguration Configure()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");
            return builder.Build();
        }
    }
}