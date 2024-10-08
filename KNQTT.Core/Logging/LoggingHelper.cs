using D2DAggregation.Infrastructure.Core.Logging;
using Infrastructure.Core.Utilities;
using Newtonsoft.Json;
using Serilog;
using System.Diagnostics;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Core.Logging
{
    public static class LoggingHelper
    {
        private static IDiagnosticContext _diagnosticContext;

        public static void Config(IDiagnosticContext diagnosticContext)
        {
            _diagnosticContext = diagnosticContext;
        }

        public static void SetLogStep(string msg)
        {
            CoreUtility.SetCustomLog(msg.ToString());
        }

        public static string GetLogStep()
        {
            return CoreUtility.GetCustomLog();
        }

        public static void SetProperty(string property, object obj, bool destructureObjects = false)
        {
            _diagnosticContext.Set(property, obj, destructureObjects);
            if (CoreUtility.CoreSettings.Observability != null && CoreUtility.CoreSettings.Observability.Enable.Equals(1))
            {
                if (destructureObjects)
                {
                    foreach (var item in obj.GetType().GetProperties())
                    {
                        var tagName = $"{property}.{item.Name}";
                        var tagValue = item.GetValue(obj);
                        Activity.Current.SetTag(tagName, tagValue);
                    }
                }
                else
                {
                    Activity.Current.SetTag(property, obj);
                }
            }
        }

        /// <summary>
        /// Ghi log các task không cần chờ kết quả
        /// </summary>
        /// <param name="logObject"></param>
        public static void SetAsyncLog(CoreAsynLogcObject logObject)
        {
            _ = Task.Run(() =>
            {
                //CoreAsyncLog.InsertLog(logObject);
                StringBuilder sb = new StringBuilder();
                sb.AppendLine($"\n-IP Address: " + GetIPAddress());
                sb.AppendLine("-Log: \n" + JsonConvert.SerializeObject(logObject));
                Console.WriteLine(sb.ToString());
            });
        }

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

    public static class StringBuilderExtensions
    {
        public static void AppendLog(this StringBuilder stringBuilder, string text)
        {
            stringBuilder.AppendLine(text);
            CoreUtility.SetCustomLog(text);
        }
    }
}
