using Infrastructure.Core.Enums;
using Infrastructure.Core.Exceptions;
using Infrastructure.Core.Logging;
using Infrastructure.Core.Models;
using Infrastructure.Core.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using System.Net;

namespace Infrastructure.Core.Hosting
{
    /// <summary>
    /// Exception filter
    /// </summary>
    public class HttpGlobalExceptionFilter : IExceptionFilter
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public void OnException(ExceptionContext context)
        {
            string errorId = CoreUtility.GetErrorCode("e");
            string message = $"Đã xảy ra lỗi trong quá trình xử lý.\nVui lòng liên hệ nhóm KNQTT.";
            string errorMessage = context.Exception.ToString();
            LoggingHelper.SetProperty("Exception:", new ExceptionDto(context.Exception), true);

            if (context.Exception.GetType() == typeof(CustomValidationException))
            {
                errorId = CoreUtility.GetErrorCode("v");
                var validationException = (CustomValidationException)context.Exception;
                message = $"Thông tin không hợp lệ. Vui lòng kiểm tra lại.";
                LoggingHelper.SetProperty("ValidateException:", validationException.Errors.SelectMany(x => x.Value));
                errorMessage = string.Join(",", validationException.Errors.SelectMany(x => x.Value));
            }

            if (context.Exception.GetType() == typeof(HttpException))
            {
                errorId = CoreUtility.GetErrorCode("h");
                message = $"Đã xảy ra lỗi trong quá trình xử lý.\nVui lòng thử lại hoặc liên hệ nhóm KNQTT.";
            }

            if (context.Exception.GetType() == typeof(CoreException))
            {
                errorId = CoreUtility.GetErrorCode("c");
                message = $"Đã xảy ra lỗi trong quá trình xử lý.\nVui lòng liên hệ nhóm KNQTT.";
            }

            var response = ResultObject.Error(message, "error_exception", errorId: errorId, code: ResultCode.ErrorException);
            LoggingHelper.SetProperty("ErrorId:", errorId);
            LoggingHelper.SetProperty("ResponseCode", (int)response.Code);

            if (CoreUtility.IsDevelopment())
            {
                response.Message.ExMessage = errorMessage;
            }

            context.Result = new ContentResult()
            {
                Content = JsonConvert.SerializeObject(response),
                ContentType = "application/json; charset=utf-8",
                StatusCode = (int)HttpStatusCode.OK
            };
            context.ExceptionHandled = true;
        }
    }
}

