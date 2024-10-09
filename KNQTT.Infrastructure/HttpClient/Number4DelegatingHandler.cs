using Infrastructure.Core.Database;
using Infrastructure.Core.Logging;
using Infrastructure.Core.Models;
using KNQTT.Infrastructure.Consts;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Headers;
using System.Text;

namespace KNQTT.Infrastructure.HttpClient
{
    public class Number4DelegatingHandler : DelegatingHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(
          HttpRequestMessage request,
          CancellationToken cancellationToken)
        {
            string accessToken = "";
            try
            {
                accessToken = RedisDb.StringGet(CacheKeys.Token);
                if (string.IsNullOrEmpty(accessToken?.Trim()))
                {
                    accessToken = await GetAccessToken();
                }
            }
            catch
            {
                accessToken = await GetAccessToken();
            }

            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            var response = await base.SendAsync(request, cancellationToken);

            if (response.StatusCode == HttpStatusCode.Unauthorized || response.StatusCode == HttpStatusCode.Forbidden || string.IsNullOrEmpty(accessToken))
            {
                accessToken = await GetAccessToken();
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                response = await base.SendAsync(request, cancellationToken);
            }
            return response;
        }

        private async Task<string> GetAccessToken()
        {
            var log = new StringBuilder();
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Post, string.Concat(Helper.Settings.Domain.Number5Service, UrlsConfig.Number2Service.Get));
                string user = Helper.Settings.SecuritySettings.ClientId;
                string pass = Helper.Settings.SecuritySettings.ClientSecret;

                using (System.Net.Http.HttpClient httpClient = new System.Net.Http.HttpClient())
                {
                    string headerValue = string.Format("Basic {0}", Convert.ToBase64String(Encoding.Default.GetBytes(string.Format("{0}:{1}", user, pass))));
                    log.AppendLine("- Header value: " + headerValue);
                    log.AppendLine("- Url: " + string.Concat(Helper.Settings.Domain.Number5Service, UrlsConfig.Number2Service.Get));
                    request.Headers.Add("Authorization", headerValue);
                    var content = new { GrantType = "client_credentials" };
                    request.Content = new StringContent(JsonConvert.SerializeObject(content), Encoding.UTF8, "application/json");
                    var objResp = await httpClient.SendAsync(request);
                    if (objResp.IsSuccessStatusCode)
                    {
                        var result = await objResp.Content.ReadAsStringAsync();
                        var objContent = JsonConvert.DeserializeObject<InternalBaseDto<TokenDto>>(result);
                        log.AppendLine(result);
                        if (objContent.IsSuccess && objContent.Data != null && !string.IsNullOrWhiteSpace(objContent.Data.AccessToken))
                        {
                            try
                            {
                                RedisDb.StringSet(CacheKeys.Token, objContent.Data.AccessToken, (int)objContent.Data.AccessTokenLifeTime - 100);
                            }
                            catch (Exception ex)
                            {
                                log.AppendLine($"Exception: {ex}");
                            }
                            return objContent.Data.AccessToken;
                        }
                        else
                        {
                            throw new Exception("error_get_access_token");
                        }
                    }
                    else
                    {
                        var result = await objResp.Content.ReadAsStringAsync();
                        log.AppendLine($"ResponseCode: {objResp.StatusCode}");
                        log.AppendLine($"Response: {result}");
                        throw new Exception("error_get_access_token");
                    }
                }
            }
            catch (Exception ex)
            {
                log.AppendLine($"Exception: {ex}");
                throw new Exception("error_get_access_token_omnisell");
            }
            finally
            {
                LoggingHelper.SetLogStep(log.ToString());
            }
        }

        public class TokenDto
        {
            public string AccessToken { get; set; }
            public int AccessTokenLifeTime { get; set; }
            public string RefreshToken { get; set; }
        }
    }
}
