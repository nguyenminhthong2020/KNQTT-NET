using Infrastructure.Core.Consts;
using Infrastructure.Core.Enums;
using Infrastructure.Core.Models;
using Infrastructure.Core.Utilities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Net;

namespace Infrastructure.Core.Authentication
{
    /// <summary>
    /// Hanlde các event xử lý JWT
    /// </summary>
    public static class JwtEventsHandler
    {

        /// <summary>
        /// Invoked before a challenge is sent back to the caller.
        /// </summary>
        /// <returns></returns>
        public static Func<JwtBearerChallengeContext, Task> OnChallengeHandler()
        {
            return context =>
            {
                context.HandleResponse();
                var response = ResultObject.Error("Unauthorized.",
                    context.AuthenticateFailure?.ToString() ?? string.Empty,
                    code: ResultCode.ErrorAuthorizationFail);

                context.HttpContext.Response.ContentType = "application/json; charset=utf-8";
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.OK;
                context.HttpContext.Response.WriteAsync(JsonConvert.SerializeObject(response)).Wait();
                return Task.CompletedTask;
            };
        }

        /// <summary>
        /// Invoked if exceptions are thrown during request processing. The exceptions will be re-thrown after this event unless suppressed.
        /// </summary>
        /// <returns></returns>
        public static Func<AuthenticationFailedContext, Task> OnAuthenticationFailedHandler()
        {
            return context =>
            {
                return Task.CompletedTask;
            };
        }

        /// <summary>
        /// Invoked before a challenge is sent back to the caller.
        /// </summary>
        /// <returns></returns>
        public static Func<ForbiddenContext, Task> OnForbiddenHandler()
        {
            return context =>
            {
                var response = ResultObject.Warning("Bạn không có quyền sử dụng chức năng này!",
                    string.Empty,
                   code: ResultCode.WarningForbidden);

                context.HttpContext.Response.ContentType = "application/json; charset=utf-8";
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.OK;
                context.HttpContext.Response.WriteAsync(JsonConvert.SerializeObject(response)).Wait();
                return Task.CompletedTask;
            };
        }

        /// <summary>
        /// Invoked before a challenge is sent back to the caller.
        /// </summary>
        /// <returns></returns>
        public static Func<TokenValidatedContext, Task> OnTokenValidated()
        {
            return context =>
            {
                // Bắt phải có api-key
                // if (CoreUtility.IsProduction())
                {
                    var apiKey = context.HttpContext.Request.Headers[CoreConsts.HeaderGatewayKey];
                    if (apiKey != CoreUtility.CoreSettings.SecuritySettings.AuthenticationKey)
                    {
                        context.Fail("api-key invalid.");
                        return Task.CompletedTask;
                    }
                }
                return Task.CompletedTask;
            };
        }
    }
}