using Infrastructure.Core.CoreOptions;
using Infrastructure.Core.Enums;
using Infrastructure.Core.Exceptions;
using Infrastructure.Core.Logging;
using Infrastructure.Core.Models;
using Infrastructure.Core.Utilities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using System.Net;

namespace Infrastructure.Core.Hosting
{
    /// <summary>
    /// Extension
    /// </summary>
    public static class ApplicationBuilderExtension
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseCustomStaticFiles(this IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseStaticFiles();
            return app;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseCustomExceptionHandler(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(options => options.Run(async context =>
            {
                var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();
                var exception = exceptionHandlerPathFeature.Error;
                string errorId = CoreUtility.GetErrorCode("g");
                string message = $"Đã xảy ra lỗi trong quá trình xử lý. Vui lòng liên hệ nhóm KNQTT.";

                LoggingHelper.SetProperty("ErrorId:", errorId);
                LoggingHelper.SetProperty("UnhandledException:", new ExceptionDto(exception), true);

                var response = ResultObject.Error(message,
                    CoreUtility.IsDevelopment() ? exception.ToString() : string.Empty,
                    errorId: errorId,
                    code: ResultCode.ErrorException);

                context.Response.ContentType = "application/json; charset=utf-8";
                context.Response.StatusCode = (int)HttpStatusCode.OK;
                await context.Response.WriteAsync(JsonConvert.SerializeObject(response));
            }));

            return app;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        /// <param name="apiVersion"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseCustomSwagger(this IApplicationBuilder app, IWebHostEnvironment env)
        {
            //if (!env.IsProduction())
            //{
            //    app.UseSwagger(c =>
            //    {
            //        c.RouteTemplate = string.Concat(ProjectConfig.Options.RoutePrefix, "/docs/{documentName}/swagger.json");
            //    });
            //    app.UseSwaggerUI(c =>
            //    {
            //        c.SwaggerEndpoint($"/{ProjectConfig.Options.RoutePrefix}/docs/v1/swagger.json", $"KNQTT API Document {env.EnvironmentName}");
            //        c.RoutePrefix = string.Concat(CoreOptions.ProjectConfig.Options.RoutePrefix, "/docs");
            //    });
            //}
            //else
            //{
            //    // TODO
            //}
            return app;
        }
    }
}
