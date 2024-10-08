using Infrastructure.Core.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Core.Utilities
{
    public static partial class CoreUtility
    {
        public static CoreAppSettings CoreSettings => CoreAppSettingServices.Get;
        private static IHttpClientFactory _httpClientFactory;
        private static Random _coreRandomErorCode = new Random(GetSeedRandom());

        public static void ConfigureHttpClientFactory(IHttpClientFactory httpClient)
        {
            _httpClientFactory = httpClient;
        }

        public static IHttpClientFactory HttpClientFactory()
        {
            return _httpClientFactory;
        }

        public static string GetEnvironmentUrl(string path)
        {
            return path.Replace("{environment}", CoreSettings.CommonSettings.StaticStorage);
        }

        public static bool IsDevelopment()
        {
            return Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT").ToLower().Equals("development");
        }

        public static bool IsStaging()
        {
            return Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT").ToLower().Equals("staging");
        }

        public static bool IsProduction()
        {
            return Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT").ToLower().Equals("production");
        }

        public static bool IsLocal()
        {
            try
            {
                return _context.Request.Host.Host.Equals("localhost", StringComparison.OrdinalIgnoreCase);
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <param name="secretKey"></param>
        /// <returns></returns>
        public static string CreateHMAC256(string input, string secretKey)
        {
            byte[] inputs = Encoding.UTF8.GetBytes(input);
            byte[] keys = Encoding.UTF8.GetBytes(secretKey);
            HMACSHA256 hmac256 = new HMACSHA256(keys);
            byte[] hashByte = hmac256.ComputeHash(inputs);

            StringBuilder sb = new StringBuilder();
            foreach (byte x in hashByte)
            {
                sb.Append(string.Format("{0:x2}", x));
            }
            return sb.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <param name="secretKey"></param>
        /// <returns></returns>
        public static byte[] CreateHMAC256Byte(string input, string secretKey)
        {
            byte[] inputs = Encoding.UTF8.GetBytes(input);
            byte[] keys = Encoding.UTF8.GetBytes(secretKey);
            HMACSHA256 hmac256 = new HMACSHA256(keys);
            byte[] hashByte = hmac256.ComputeHash(inputs);
            return hashByte;
        }

        /// <summary>
        /// Tạo mã code lỗi
        /// c: mã lỗi chung của Core
        /// </summary>
        /// <returns></returns>
        internal static string GetErrorCode(string prefix = "c")
        {
            var randomValue = _coreRandomErorCode.Next(12345610, 98976910);
            return randomValue.ToString().Insert(0, $"0x{prefix}{DateTime.Now.Day}");
        }

        /// <summary>
        /// Seed random error code
        /// </summary>
        /// <returns></returns>
        private static int GetSeedRandom()
        {
            using (var rng = new RNGCryptoServiceProvider())
            {
                var intBytes = new byte[4];
                rng.GetBytes(intBytes);
                return BitConverter.ToInt32(intBytes, 0);
            }
        }
        public static void PushMessage(string text)
        {
            HttpClient _httpClient = new HttpClient();
            try
            {
                List<string> account = new List<string>() { "conglt16@fpt.com", "khiembt@fpt.com", "phongdh10@fpt.com", "tutt25@fpt.com", "hungnv165@fpt.com" };

                var proxy = new WebProxy
                {
                    Address = new Uri("http://isc-proxy.hcm.fpt.vn:80"),
                    BypassProxyOnLocal = true,
                    UseDefaultCredentials = false,
                };

                // Create a client handler that uses the proxy
                var httpClientHandler = new HttpClientHandler
                {
                    Proxy = proxy,
                    UseProxy = true,
                };
                _httpClient = new HttpClient(handler: httpClientHandler, disposeHandler: true);
                foreach (var item in account)
                {
                    Task.Run(() =>
                    {
                        var content = new
                        {
                            email = item,
                            text = text,
                            Context = ""
                        };
                        var httpReq = new HttpRequestMessage()
                        {
                            Content = new StringContent(JsonConvert.SerializeObject(content), Encoding.UTF8, "application/json"),
                            Method = HttpMethod.Post,
                            RequestUri = new System.Uri(string.Concat("https://alerts.soc.fpt.net", string.Format("/webhooks/{0}/facebook", "KPBVo4TYzaUblOaqpG8rwYBj8uiGBttP")))
                        };
                        var rpSendBotChat = _httpClient.SendAsync(httpReq);
                    });
                }
            }
            catch (Exception ex)
            {
            }

        }
    }
}
