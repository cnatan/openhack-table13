using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var config = new ConfigurationBuilder()
            .AddEnvironmentVariables(prefix: "MINECRAFT_API_")
            .AddCommandLine(args)
            .Build();

            BuildWebHost(args, config).Run();
        }

        public static IWebHost BuildWebHost(string[] args, IConfiguration config) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .UseConfiguration(config)
                .UseUrls("http://0.0.0.0:8888")
                .Build();
    }
}
