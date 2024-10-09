using static Infrastructure.Core.Configuration.CoreAppSettingDetail;

namespace Infrastructure.Core.Configuration
{
    public class CoreAppSettings
    {
        public CommonSettings CommonSettings { get; set; }
        public ConnectionStringSettings ConnectionStringSettings { get; set; }
        public RedisSettings RedisSettings { get; set; }
        public ProxySettings ProxySettings { get; set; }
        public MongoSettings MongoSettings { get; set; }
        public ServiceSettings ServiceSettings { get; set; }
        public SecuritySettings SecuritySettings { get; set; }
        public LoggingSettings Logging { get; set; }
        public ObservabilitySettings Observability { get; set; }
    }

    public class CoreAppSettingDetail
    {
        public class CommonSettings
        {
            /// <summary>
            /// Host lưu trữ 
            /// </summary>
            public string StaticStorage { get; set; }

            /// <summary>
            /// 1: bảo trì
            /// 0: không bảo trì
            /// </summary>
            public int IsMaintain { get; set; }
        }

        public class ConnectionStringSettings
        {
            /// <summary>
            /// Kết nối dùng để ghi db 
            /// </summary>
            public string DatabaseWrite { get; set; }

            /// <summary>
            /// Kết nối dùng để ghi db 
            /// </summary>
            public string DatabaseRead { get; set; }
        }

        public class RedisSettings
        {
            /// <summary>
            /// Kết nối dùng để ghi
            /// </summary>
            public string ServerWrite { get; set; }

            /// <summary>
            /// Kết nối dùng để đọc
            /// </summary>
            public string ServerRead { get; set; }

            /// <summary>
            /// DB
            /// </summary>
            public int DatabaseNumber { get; set; }
        }

        public class LoggingSettings
        {
            /// <summary>
            /// 1: Ghi log file
            /// 0: Không ghi log file
            /// </summary>
            public int EnableTxt { get; set; }

            /// <summary>
            /// 1: Ghi log ELK
            /// 0: Không ghi log ELK
            /// </summary>
            public int EnableElastic { get; set; }

            /// <summary>
            /// Url ghi log ELK
            /// </summary>
            public string EsUrl { get; set; }

            /// <summary>
            /// Topic ghi log ELK
            /// </summary>
            public string EsTopic { get; set; }
        }

        public class ProxySettings
        {
            /// <summary>
            /// Url proxy
            /// </summary>
            public string Url { get; set; }

            /// <summary>
            /// Port proxy
            /// </summary>
            public string Port { get; set; }
        }

        public class MongoSettings
        {
            /// <summary>
            /// Kết nối dùng để ghi
            /// </summary>
            public string ServerWrite { set; get; }

            /// <summary>
            /// Kết nối dùng để đọc
            /// </summary>
            public string ServerRead { set; get; }
        }

        public class ServiceSettings
        {
            /// <summary>
            /// Service dùng chứng thực
            /// </summary>
            public string Identity { get; set; }
            public string IdentityValidatePolicy { get => string.Concat(this.Identity, "/auth/api/v1/policy/validate"); }
        }

        public class SecuritySettings
        {
            public string AuthenticationKey { get; set; }
            public string DecryptKey { get; set; }
        }

        public class ObservabilitySettings
        {
            /// <summary>
            /// 0: disable
            /// 1: enable
            /// </summary>
            public int Enable { get; set; }
        }
    }
}
