using Infrastructure.Core.Utilities;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace KNQTT.Infrastructure.HttpClient
{
    public class Number1ClientDelegatingHandler : DelegatingHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(
           HttpRequestMessage request,
           CancellationToken cancellationToken)
        {
            // gữa các môi trường không thay đổi key
            request.Headers.Add("api-key", "KNQTT-2024");

            return base.SendAsync(request, cancellationToken);
        }
    }
}

