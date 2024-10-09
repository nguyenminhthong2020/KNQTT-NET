using Confluent.Kafka;
using Infrastructure.Core.CoreOptions;
using Infrastructure.Core.Utilities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace KNQTT.Infrastructure.Core.Logging
{
    public static class CoreAsyncLog
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_data"></param>
        internal static void InsertLog(CoreAsynLogcObject logcObject)
        {
            try
            {
                string data = JsonConvert.SerializeObject(logcObject);
                var cfg = new ProducerConfig
                {
                    BootstrapServers = CoreUtility.CoreSettings.Logging.EsUrl,
                    ClientId = GetIPAddress(),
                    MessageSendMaxRetries = 1,
                    MessageTimeoutMs = 5000
                };

                using (var producer = new ProducerBuilder<string, string>(cfg).Build())
                {
                    try
                    {
                        var dr = producer.ProduceAsync(CoreUtility.CoreSettings.Logging.EsTopic, new Message<string, string> { Key = null, Value = data });
                        producer.Flush();
                    }
                    catch (ProduceException<string, string> e)
                    {
                        Console.WriteLine(e.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private static string GetIPAddress()
        {
            string result = string.Empty;
            try
            {
                string hostName = Dns.GetHostName();
                IPAddress[] ipAddress = Dns.GetHostAddresses(hostName);
                result = string.Join(",", ipAddress.Where(ip => ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork));
            }
            catch
            {
                result = string.Empty;
            }
            return result;
        }

    }

    /// <summary>
    /// Class dùng ghi log async
    /// </summary>
    public class CoreAsynLogcObject
    {
        public CoreAsynLogcObject()
        {
            Properties = new Dictionary<string, object>();
        }

        public string Parameter { get; set; }
        public string ResponseData { get; set; }
        public string CustomLog { get; set; }
        /// <summary>
        /// Account user
        /// </summary>
        public string User { get; set; }
        /// <summary>
        /// Tên method or chức năng
        /// </summary>
        public string EndpointName { get; set; }
        public string Source { private set; get; } = ProjectConfig.Options.ProjectName;
        public string SourceType { private set; get; } = "USER-ASYNC-LOG";
        /// <summary>
        /// Các thuộc tính mở rộng
        /// </summary>
        [JsonExtensionData]
        public IDictionary<string, object> Properties { get; set; }
    }
}
