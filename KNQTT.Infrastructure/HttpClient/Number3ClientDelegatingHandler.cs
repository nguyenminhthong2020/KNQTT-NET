using Infrastructure.Core.Consts;
using Infrastructure.Core.Utilities;

namespace KNQTT.Infrastructure.HttpClient
{
    public class Number3ClientDelegatingHandler : DelegatingHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(
           HttpRequestMessage request,
           CancellationToken cancellationToken)
        {
            // internal 
            request.Headers.Add(CoreConsts.HeaderCorrelationId, CoreUtility.GetHeaderByKey(CoreConsts.HeaderCorrelationId));
            request.Headers.Add(CoreConsts.HeaderSandbox, CoreUtility.GetHeaderByKey(CoreConsts.HeaderSandbox));
            request.Headers.Add(CoreConsts.HeaderAuthorization, CoreUtility.CoreSettings.SecuritySettings.AuthenticationKey);

            return base.SendAsync(request, cancellationToken);
        }
    }
}

