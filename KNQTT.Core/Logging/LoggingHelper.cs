using D2DAggregation.Infrastructure.Core.Logging;
using Infrastructure.Core.Utilities;
using Serilog;
using System.Diagnostics;
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
                CoreAsyncLog.InsertLog(logObject);
            });
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
