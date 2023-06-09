﻿using System;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.Elasticsearch;
using Microsoft.AspNetCore.Server.Kestrel.Https;

namespace AuthServer.Host
{
    public class Program
    {
        public static int Main(string[] args)
        {
            //TODO: Temporary: it's not good to read appsettings.json here just to configure logging
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables()
                .Build();

            //Log.Logger = new LoggerConfiguration()
            //    .MinimumLevel.Debug()
            //    .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
            //    .MinimumLevel.Override("Microsoft.EntityFrameworkCore", LogEventLevel.Warning)
            //    .Enrich.WithProperty("Application", "AuthServer")
            //    .Enrich.FromLogContext()
            //    .WriteTo.File("Logs/logs.txt")
            //    .WriteTo.Elasticsearch(
            //        new ElasticsearchSinkOptions(new Uri(configuration["ElasticSearch:Url"]))
            //        {
            //            AutoRegisterTemplate = true,
            //            AutoRegisterTemplateVersion = AutoRegisterTemplateVersion.ESv6,
            //            IndexFormat = "msdemo-log-{0:yyyy.MM}"
            //        })
            //    .CreateLogger();

            try
            {
                Log.Information("Starting AuthServer.Host.");
                CreateHostBuilder(args).Build().Run();
                return 0;
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "AuthServer.Host terminated unexpectedly!");
                return 1;
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        internal static IHostBuilder CreateHostBuilder(string[] args) =>
            Microsoft.Extensions.Hosting.Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
                .UseAutofac()
                .UseSerilog();
    }
}
