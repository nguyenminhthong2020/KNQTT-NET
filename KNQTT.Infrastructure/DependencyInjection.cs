using Elasticsearch.Net;
using Infrastructure.Core.Utilities;
using KNQTT.Infrastructure.Configuration;
using KNQTT.Infrastructure.Consts;
using KNQTT.Infrastructure.HttpClient;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nest;
using Nest.JsonNetSerializer;
using Polly;
using Polly.Extensions.Http;
using Polly.Timeout;
using System.Net;

namespace KNQTT.Infrastructure
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddHttpClientServices(this IServiceCollection services)
        {
            services.AddTransient<Number1ClientDelegatingHandler>();
            services.AddTransient<Number2ClientDelegatingHandler>();
            services.AddTransient<Number3ClientDelegatingHandler>();
            services.AddTransient<Number4DelegatingHandler>();

            services.AddSingleton<IElasticClient>(sp =>
            {
                var pool = new SingleNodeConnectionPool(new Uri($"{Helper.Settings.ElasticsearchSettings.Url}:{Helper.Settings.ElasticsearchSettings.Port}"));
                if (CoreUtility.IsLocal())
                {
                    pool = new SingleNodeConnectionPool(new Uri($"http://server/elasticsearch"));
                }
                var settings = new ConnectionSettings(pool, JsonNetSerializer.Default);
                settings.BasicAuthentication(Helper.Settings.ElasticsearchSettings.User, Helper.Settings.ElasticsearchSettings.Pass);
                settings.EnableApiVersioningHeader();
                settings.DefaultFieldNameInferrer(p => p);

                return new ElasticClient(settings);
            });

            services.AddHttpClient(HttpClientName.Number1Service, client =>
            {
                client.BaseAddress = new Uri(Helper.Settings.Domain.Number1Service);
                client.Timeout = TimeSpan.FromSeconds(20);
            }).AddHttpMessageHandler<Number3ClientDelegatingHandler>()
            .AddPolicyHandler((provider, request) =>
            {
                return HttpPolicyExtensions
                        .HandleTransientHttpError()
                        .Or<TimeoutRejectedException>()
                        .WaitAndRetryAsync(2, retryAttempt => TimeSpan.FromSeconds(1));
            });


            services.AddHttpClient(HttpClientName.Number2Service, client =>
            {
                client.BaseAddress = new Uri(Helper.Settings.Domain.Number2Service);
                client.Timeout = TimeSpan.FromSeconds(20);
            }).ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler()
            {
                Proxy = new WebProxy()
                {
                    UseDefaultCredentials = false,
                    Address = new Uri($"{CoreUtility.CoreSettings.ProxySettings.Url}:{CoreUtility.CoreSettings.ProxySettings.Port}"),
                    BypassProxyOnLocal = true,
                }
            }).AddHttpMessageHandler<Number2ClientDelegatingHandler>();


            services.AddHttpClient(HttpClientName.Number3Service, client =>
            {
                client.BaseAddress = new Uri(Helper.Settings.Domain.Number3Service);
                client.Timeout = TimeSpan.FromSeconds(20);
            }).AddHttpMessageHandler<Number3ClientDelegatingHandler>();

            return services;
        }

        public static IServiceCollection AddInfrastructureLayer(this IServiceCollection services, IConfiguration Configuration)
        {
            services.Configure<AppSettings>(Configuration.GetSection(nameof(AppSettings)));
            services.AddHttpClientServices();
            return services;
        }

        public static IApplicationBuilder AddInfrastructureLayer(this IApplicationBuilder app, IWebHostEnvironment env)
        {
            AppSettingServices.Services = app.ApplicationServices;
            Helper.ServiceProvider = app.ApplicationServices;
            return app;
        }
    }
}
