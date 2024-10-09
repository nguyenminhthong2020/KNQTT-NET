using Infrastructure.Core.Exceptions;
using Infrastructure.Core.Logging;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace KNQTT.Infrastructure
{
    public class CoreHttpClientCustomize : ICoreHttpClientCustomize
    {
        private readonly IHttpClientFactory _clientFactory;
        public CoreHttpClientCustomize(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="clientName"></param>
        /// <param name="uri"></param>
        /// <param name="reqObj"></param>
        /// <returns></returns>
        public async Task<T> PostAsync<T>(string clientName, string uri, byte[] fileBytes, string fileName, string formName) where T : class
        {
            var log = new StringBuilder();
            try
            {
                // create form-data
                using var form = new MultipartFormDataContent();
                var fileContent = new ByteArrayContent(fileBytes);
                fileContent.Headers.ContentType = MediaTypeHeaderValue.Parse("multipart/form-data");
                form.Add(fileContent, formName, fileName);
                var clientFactory = _clientFactory.CreateClient(clientName);
                var client = await clientFactory.PostAsync(uri, form);
                log.AppendLine($"RequestUri: {client.RequestMessage.RequestUri.OriginalString}");

                if (client.IsSuccessStatusCode || client.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    var jsonStringResult = await client.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<T>(jsonStringResult);
                    log.AppendLine($"ResponseCode: {client.StatusCode}");
                    log.AppendLine($"Response: {JsonConvert.SerializeObject(result)}");

                    return result;
                }
                else
                {
                    var result = await client.Content.ReadAsStringAsync();
                    log.AppendLine($"ResponseCode: {client.StatusCode}");
                    log.AppendLine($"Response: {result}");

                    throw new HttpException("error_http_status_code");
                    //return default(T);
                }
            }
            catch (Exception ex)
            {

                log.AppendLine($"Exception: {ex}");
                throw new HttpException(ex.ToString());
            }
            finally
            {
                LoggingHelper.SetLogStep(log.ToString());
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="clientName"></param>
        /// <param name="uri"></param>
        /// <param name="reqObj"></param>
        /// <returns></returns>
        public async Task<T> PostAsync<T>(string clientName, string uri, object reqObj, Dictionary<String, String> header = null) where T : class
        {
            var log = new StringBuilder();
            try
            {
                var jsonString = JsonConvert.SerializeObject(reqObj);
                log.AppendLine(string.Concat($"Before call api url: {uri}", Environment.NewLine, $"Input: {jsonString}"));

                var clientFactory = _clientFactory.CreateClient(clientName);

                if (header != null)
                {
                    foreach (KeyValuePair<string, string> entry in header)
                    {
                        clientFactory.DefaultRequestHeaders.Add(entry.Key, entry.Value);
                    }
                }
                var data = new StringContent(jsonString, Encoding.UTF8, "application/json");
                var client = await clientFactory.PostAsync(uri, data);
                log.AppendLine($"RequestUri: {client.RequestMessage.RequestUri.OriginalString}");

                if (client.IsSuccessStatusCode)
                {
                    var jsonStringResult = await client.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<T>(jsonStringResult);
                    log.AppendLine($"ResponseCode: {client.StatusCode}");
                    log.AppendLine($"Response: {JsonConvert.SerializeObject(result)}");

                    return result;
                }
                else
                {
                    var result = await client.Content.ReadAsStringAsync();
                    log.AppendLine($"ResponseCode: {client.StatusCode}");
                    log.AppendLine($"Response: {result}");

                    throw new HttpException("error_http_status_code");
                    //return default(T);
                }
            }
            catch (Exception ex)
            {
                log.AppendLine($"Exception: {ex}");
                throw new HttpException(ex.ToString());
            }
            finally
            {
                LoggingHelper.SetLogStep(log.ToString());
            }
        }
    }
}
