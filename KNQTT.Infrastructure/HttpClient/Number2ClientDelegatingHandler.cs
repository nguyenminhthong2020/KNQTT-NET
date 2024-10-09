using Infrastructure.Core.Consts;
using Infrastructure.Core.Utilities;

namespace KNQTT.Infrastructure.HttpClient
{
    public class Number2ClientDelegatingHandler : DelegatingHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(
           HttpRequestMessage request,
           CancellationToken cancellationToken)
        {
            request.Headers.Add(CoreConsts.HeaderCorrelationId, CoreUtility.GetHeaderByKey(CoreConsts.HeaderCorrelationId));
            request.Headers.Add(CoreConsts.HeaderSandbox, CoreUtility.GetHeaderByKey(CoreConsts.HeaderSandbox));
            return base.SendAsync(request, cancellationToken);
        }
    }
}
