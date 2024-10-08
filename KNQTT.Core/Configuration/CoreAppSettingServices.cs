using Infrastructure.Core.Exceptions;
using Microsoft.Extensions.Options;

namespace Infrastructure.Core.Configuration
{
    public static class CoreAppSettingServices
    {
        static IServiceProvider services = null;

        /// <summary>
        /// Provides static access to the framework's services provider
        /// </summary>
        public static IServiceProvider Services
        {
            get { return services; }
            set
            {
                if (services != null)
                {
                    throw new CoreException("Can't set once a value has already been set.");
                }
                services = value;
            }
        }

        /// <summary>
        /// Configuration settings from appsetting.json.
        /// </summary>
        public static CoreAppSettings Get
        {
            get
            {
                var s = services.GetService(typeof(IOptionsMonitor<CoreAppSettings>)) as IOptionsMonitor<CoreAppSettings>;
                CoreAppSettings config = s.CurrentValue;

                return config;
            }
        }
    }
}
