using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Serilog.Events;
using Serilog.Exceptions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Serilog.Sinks.Kafka;
using Serilog.Formatting.Compact;
using Serilog.Exceptions.Core;
using System.Linq;
using Infrastructure.Core.CoreOptions;

namespace Infrastructure.Core.Logging
{
    /// <summary>
    /// Serilog
    /// </summary>
    public static class SerilogExtentions
    {
        public static void CreateLoggerConfiguration(this IServiceProvider serviceProvider, IWebHostEnvironment env)
        {
            var config = serviceProvider.GetService<IConfiguration>();
            string exprExclude = string.Join(" or ", ProjectConfig.Options.ExcludeEndpoint.Select(x => $"Contains(RequestPath, '{x}')"));
            string exprInclude = "SourceType = 'USER-LOG'";

            var loggerConfig = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
            .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning)
            .Enrich.WithExceptionDetails(new DestructuringOptionsBuilder().WithDefaultDestructurers().WithRootName("Exception"))
            .Enrich.FromLogContext()
            .Filter.ByExcluding(exprExclude)
            .Filter.ByIncludingOnly(exprInclude);

            if (!env.IsProduction())
            {
                loggerConfig.WriteTo.Async(a => a.Console());
            }

            if (config.GetValue<int>("CoreAppSettings:Logging:EnableEs", 0).Equals(1))
            {
                loggerConfig.WriteTo.Async(a =>
                a.Kafka(
                    bootstrapServers: config.GetValue<string>("CoreAppSettings:Logging:EsUrl", string.Empty),
                    topic: config.GetValue<string>("CoreAppSettings:Logging:EsTopic", string.Empty),
                    formatter: new CompactJsonFormatter()));
            }

            Log.Logger = loggerConfig.CreateLogger();
        }
    }
}
