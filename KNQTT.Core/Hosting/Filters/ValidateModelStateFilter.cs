using Infrastructure.Core.Enums;
using Infrastructure.Core.Logging;
using Infrastructure.Core.Models;
using Infrastructure.Core.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;

namespace Infrastructure.Core.Hosting
{
    /// <summary>
    /// 
    /// </summary>
    public class ValidateModelStateFilter : ActionFilterAttribute
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.ModelState.IsValid)
            {
                return;
            }

            var validationErrors = string.Join(" | ", context.ModelState
                .Keys
                .SelectMany(k => context.ModelState[k].Errors)
                .Select(e => e.ErrorMessage)
                .ToArray());
            LoggingHelper.SetLogStep($"error_validate_model: {validationErrors}");

            var response = ResultObject.Error(
                $"Thông tin không hợp lệ. Vui lòng kiểm tra lại.",
                CoreUtility.IsDevelopment() ? validationErrors : string.Empty,
                errorId: CoreUtility.GetErrorCode("m"),
                code: ResultCode.ErrorInputInvalid);

            context.HttpContext.Response.StatusCode = 200;
            context.Result = new ContentResult()
            {
                Content = JsonConvert.SerializeObject(response),
                ContentType = "application/json; charset=utf-8"
            };
        }
    }
}
