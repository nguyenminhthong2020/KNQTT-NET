using Infrastructure.Core.Utilities;
using Infrastructure.Core.Configuration;
using Infrastructure.Core.CoreOptions;
using Infrastructure.Core.Logging;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using System.Net.Http;
using OpenTelemetry;

namespace Infrastructure.Core
{
    public static class DependencyInjectionExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        private static IServiceCollection AddHttpClientServices(this IServiceCollection services)
        {
            services.AddHttpClient();

            return services;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        /// <param name="Configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AddCoreInfrastructureLayer(this IServiceCollection services, IConfiguration Configuration, CoreOption options)
        {
            ProjectConfig.Options = options;
            services.AddSingleton<ICoreHttpClient, CoreHttpClient>();
            services.Configure<CoreAppSettings>(Configuration.GetSection(nameof(CoreAppSettings)));
            services.AddHttpClientServices();
            if (Configuration.GetValue<int>("CoreAppSettings:Observability:Enable").Equals(1))
            {
                services.AddOpenTelemetry()
                .WithTracing(
                    tracerBuilder =>
                    {
                        tracerBuilder.Build(Configuration);
                    });
            }
            return services;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        /// <returns></returns>
        public static IApplicationBuilder AddCoreInfrastructureLayer(this IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.ApplicationServices.CreateLoggerConfiguration(env);
            CoreAppSettingServices.Services = app.ApplicationServices;
            CoreUtility.ConfigureContextAccessor(app.ApplicationServices.GetRequiredService<IHttpContextAccessor>());
            LoggingHelper.Config(app.ApplicationServices.GetRequiredService<IDiagnosticContext>());
            CoreUtility.ConfigureHttpClientFactory(app.ApplicationServices.GetRequiredService<IHttpClientFactory>());

            app.UseSerilogRequestLogging(opts =>
            {
                opts.EnrichDiagnosticContext = DiagnosticContext.EnrichFromRequest;
            });

            return app;
        }
    }
}
