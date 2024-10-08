using Infrastructure.Core.Consts;
using Infrastructure.Core.Enums;
using Infrastructure.Core.Exceptions;
using Infrastructure.Core.Logging;
using Infrastructure.Core.Models;
using Infrastructure.Core.Utilities;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json;
using System.Net.Http.Json;
using System.Text;

namespace Infrastructure.Core.Authorization
{
    /// <summary>
    /// Check policy
    /// </summary>
    public class PermissionHandler : AuthorizationHandler<PermissionRequirement>
    {
        private readonly IHttpClientFactory _clientFactory;
        public PermissionHandler(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        /// <summary>s
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="requirement"></param>
        /// <returns></returns>
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
        {
            if (context.User != null && context.User.Identity.IsAuthenticated)
            {
                var log = new StringBuilder("Check policy");
                try
                {
                    var requestObj = new
                    {
                        Function = requirement.Function,
                        AccountId = Convert.ToInt64(context.User.Claims.First(x => x.Type.Equals(CoreConsts.ClaimAccountId)).Value)
                    };
                    string jsonData = JsonConvert.SerializeObject(requestObj);
                    log.AppendLine($"Input: {jsonData}");

                    var data = new StringContent(jsonData, Encoding.UTF8, "application/json");
                    var url = CoreUtility.CoreSettings.ServiceSettings.IdentityValidatePolicy;
                    var client = _clientFactory.CreateClient();
                    client.DefaultRequestHeaders.Add(CoreConsts.HeaderAuthorization, string.Concat("Bearer ", CoreUtility.CoreSettings.SecuritySettings.AuthenticationKey));
                    client.DefaultRequestHeaders.Add(CoreConsts.HeaderSandbox, string.Concat("Bearer ", CoreUtility.GetHeaderByKey(CoreConsts.HeaderSandbox)));

                    var validateResponse = client.PostAsync(url, data).GetAwaiter().GetResult();
                    log.AppendLine($"ResponseCode: {validateResponse.StatusCode}");
                    if (validateResponse.IsSuccessStatusCode)
                    {
                        var validateResult = validateResponse.Content.ReadFromJsonAsync<InternalBaseDto<NullDataType>>().GetAwaiter().GetResult();
                        log.AppendLine($"Response: {JsonConvert.SerializeObject(validateResult)}");
                        if (validateResult.Code == (int)ResultCode.Ok)
                        {
                            context.Succeed(requirement);
                            return Task.CompletedTask;
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new CoreException(ex.ToString());
                }
                finally
                {
                    LoggingHelper.SetLogStep(log.ToString());
                }
            }

            context.Fail();
            return Task.CompletedTask;
        }
    }
}
