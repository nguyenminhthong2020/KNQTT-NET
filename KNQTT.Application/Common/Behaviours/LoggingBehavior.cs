using Infrastructure.Core.Enums;
using Infrastructure.Core.Logging;
using MediatR;
using Microsoft.Extensions.Logging;

namespace KNQTT.Application.Behaviors
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TRequest"></typeparam>
    /// <typeparam name="TResponse"></typeparam>
    public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly ILogger<LoggingBehavior<TRequest, TResponse>> _logger;
        public LoggingBehavior(ILogger<LoggingBehavior<TRequest, TResponse>> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <param name="next"></param>
        /// <returns></returns>
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var response = await next();
            try
            {
                var result = (dynamic)response;
                var code = (int)((ResultCode)result.Code);
                LoggingHelper.SetProperty("ResponseCode", code);
            }
            catch (Exception ex)
            {
                string exMsg = ex.Message;
            }
            return response;
        }
    }
}
