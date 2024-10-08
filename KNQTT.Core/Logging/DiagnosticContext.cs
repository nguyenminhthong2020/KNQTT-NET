using Infrastructure.Core.Consts;
using Infrastructure.Core.CoreOptions;
using Infrastructure.Core.Utilities;
using Microsoft.AspNetCore.Http;
using Serilog;
using System;
using System.Linq;

namespace Infrastructure.Core.Logging
{
    /// <summary>
    /// 
    /// </summary>
    public static class DiagnosticContext
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="diagnosticContext"></param>
        /// <param name="httpContext"></param>
        public static void EnrichFromRequest(
            IDiagnosticContext diagnosticContext, HttpContext httpContext)
        {
            try
            {
                string accountName = string.Empty;
                try
                {
                    accountName = CoreUtility.GetAccountName()?.ToLower() ?? "None";
                }
                catch
                {
                    accountName = "None";
                }

                var request = httpContext.Request;
                diagnosticContext.Set("Host", request.Host);
                diagnosticContext.Set("Source", ProjectConfig.Options.ProjectName);
                diagnosticContext.Set("SourceType", "USER-LOG");
                diagnosticContext.Set("Protocol", request.Protocol);
                diagnosticContext.Set("Scheme", request.Scheme);
                if (request.QueryString.HasValue) diagnosticContext.Set("QueryString", request.QueryString.Value);
                diagnosticContext.Set("ContentType", httpContext.Response.ContentType);
                diagnosticContext.Set("User", accountName.ToLower());

                string[] arr = {
                    CoreConsts.HeaderAuthorization.ToLower()
                };
                diagnosticContext.Set("Header", request.Headers.Where(x => Array.IndexOf(arr, x.Key.ToLower()) > -1));

                var endpoint = httpContext.GetEndpoint();
                if (endpoint is object)
                {
                    var temp = endpoint.DisplayName.IndexOf("(");
                    string folder = temp > 0 ? endpoint.DisplayName.Substring(0, temp).Trim() : endpoint.DisplayName;
                    diagnosticContext.Set("LogFolder", folder.ToLower());
                    diagnosticContext.Set("EndpointName", httpContext.Request.Path);
                }
                try
                {
                    diagnosticContext.Set("Pod", Environment.MachineName);
                }
                catch
                {
                    diagnosticContext.Set("Pod", "error_get_machine_name");
                }
            }
            catch (Exception ex)
            {
                string temp = string.Empty;
                try
                {
                    diagnosticContext.Set("ErrorLogging", ex.ToString());
                }
                catch (Exception ex2)
                {
                    temp = ex2.ToString();
                }
            }
        }
    }
}
