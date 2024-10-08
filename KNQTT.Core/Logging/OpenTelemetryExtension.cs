using Infrastructure.Core.Consts;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using System;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;

namespace Infrastructure.Core.Logging
{
    /// <summary>
    /// OpenTelemetry
    /// </summary>
    public static class OpenTelemetryExtension
    {
        /// <summary>
        /// array dùng để lọc header
        /// </summary>
        private static string[] headerArr = {
            CoreConsts.HeaderAuthorization.ToLower()
        };
        /// <summary>
        /// 
        /// </summary>
        /// <param name="builder"></param>
        public static void Build(this TracerProviderBuilder builder, IConfiguration Configuration)
        {
            builder
            .SetResourceBuilder(ResourceBuilder.CreateDefault().AddService(
                Configuration.GetValue<string>("CoreAppSettings:Observability:ServiceName")
                , serviceVersion: "1.0"))
            .AddAspNetCoreInstrumentation(o =>
            {
                o.Filter = (ctx) =>
                {
                    return ctx.Request.Method.Equals("POST");
                };
                o.EnrichWithHttpRequest = (activity, httpRequest) =>
                {
                    activity.SetTag("TraceId", activity.TraceId);
                    activity.SetTag("Pod", Environment.MachineName);
                    activity.SetTag("RequestProtocol", httpRequest.Protocol);
                    activity.SetTag("RequestHeader", JsonConvert.SerializeObject(httpRequest?.Headers.Where(x => Array.IndexOf(headerArr, x.Key.ToLower()) > -1)));
                    activity.SetTag("Timestamp", activity.StartTimeUtc.AddHours(7).ToString("MMM-dd-yyyy HH:mm:ss.fff",
                                            CultureInfo.InvariantCulture));
                };
                o.EnrichWithHttpResponse = (activity, HttpResponse) =>
                {
                    activity.SetTag("Elapsed", activity.Duration.TotalMilliseconds);
                };
            })
            .AddHttpClientInstrumentation(o =>
            {
                o.EnrichWithHttpRequestMessage = (activity, httpRequest) =>
                {
                    activity.SetTag("TraceId", activity.TraceId);
                    activity.SetTag("RequestHeader", JsonConvert.SerializeObject(httpRequest?.Headers.Where(x => Array.IndexOf(headerArr, x.Key.ToLower()) > -1)));
                    if (httpRequest?.Content != null)
                    {
                        var t = httpRequest.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                        activity.SetTag("RequestParameter", t);
                    }
                    else
                    {
                        activity.SetTag("RequestParameter", "");
                    }
                    activity.SetTag("Timestamp", activity.StartTimeUtc.AddHours(7).ToString("MMM-dd-yyyy HH:mm:ss.fff",
                                            CultureInfo.InvariantCulture));
                };
                o.EnrichWithHttpResponseMessage = (activity, httpResponse) =>
                {
                    if (httpResponse?.Content != null)
                    {
                        var t = httpResponse.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                        activity.SetTag("ResponseData", t);
                    }
                    else
                    {
                        activity.SetTag("ResponseData", "");
                    }
                    activity.SetTag("Elapsed", activity.Duration.TotalMilliseconds);
                };
                o.EnrichWithException = (activity, ex) =>
                {
                    activity.SetTag("Exception.Type", ex.GetType());
                    activity.SetTag("Exception.Message", ex.Message);
                    activity.SetTag("Exception.Stacktrace", ex.StackTrace);
                };
            })
            .AddSqlClientInstrumentation(o =>
            {
                o.SetDbStatementForText = true;
                o.Enrich = (activity, eventName, rawObject) =>
                {
                    if (eventName.Equals("OnCustom"))
                    {
                        if (rawObject is SqlCommand cmd)
                        {
                            activity.SetTag("db.Parameters", string.Join(",", cmd.Parameters.Cast<SqlParameter>().ToList().Select(p => $"{p.ParameterName}={p.Value}")));
                            activity.SetTag("http.timestamp", activity.StartTimeUtc.AddHours(7).ToString("MMM-dd-yyyy HH:mm:ss.fff",
                                                    CultureInfo.InvariantCulture));
                        }
                    }
                };
            });

            switch (Configuration.GetValue<string>("CoreAppSettings:Observability:Exporter").ToLower())
            {
                case "jaeger":
                    UriBuilder a = new UriBuilder(
                        scheme: "http"
                        , host: Configuration.GetValue<string>("CoreAppSettings:Observability:AgentHost")
                        , port: Configuration.GetValue<int>("CoreAppSettings:Observability:AgentPort")
                        , pathValue: "api/traces");
                    builder.AddJaegerExporter(o =>
                    {
                        o.Endpoint = a.Uri;
                        o.Protocol = OpenTelemetry.Exporter.JaegerExportProtocol.HttpBinaryThrift;
                    });
                    break;
                default:
                    builder.AddConsoleExporter();
                    break;
            }
        }
    }
}