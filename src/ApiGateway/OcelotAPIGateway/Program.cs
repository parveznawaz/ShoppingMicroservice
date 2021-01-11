using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;

namespace OcelotAPIGateway
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        //public static IHostBuilder CreateHostBuilder(string[] args)
        //{
        //    var builder = Host.CreateDefaultBuilder(args)
        //        .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
        //    builder.ConfigureAppConfiguration((config) => { config.AddJsonFile("ocelot.json"); });
        //    return builder;
        //}

        public static IWebHostBuilder CreateHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((host, config) =>
                {
                    config.AddJsonFile("ocelot.json");
                })
                .UseStartup<Startup>();
    }

}
