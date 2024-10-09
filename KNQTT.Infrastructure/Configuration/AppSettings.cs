using static KNQTT.Infrastructure.Configuration.AppSettingDetail;

namespace KNQTT.Infrastructure.Configuration
{
    public class AppSettings
    {
        public DomainSettings Domain { get; set; }
        public SecuritySettings SecuritySettings { get; set; }
        /// <summary>
        /// Thông tin server elasticsearch
        /// </summary>
        public ElasticsearchSettings ElasticsearchSettings { get; set; }
    }

    public class AppSettingDetail
    {
        public class DomainSettings
        {
            public string Number1Service { get; set; }
            public string Number2Service { get; set; }
            public string Number3Service { get; set; }
            public string Number4Service { get; set; }
            public string Number5Service { get; set; }
        }
        public class SecuritySettings
        {
            public string ClientId { get; set; }
            public string ClientSecret { get; set; }
        }
        public class ElasticsearchSettings
        {
            /// <summary>
            /// Url của server elastic
            /// </summary>
            public string Url { get; set; }
            /// <summary>
            /// Port kết nối elastic
            /// </summary>
            public int Port { get; set; }
            /// <summary>
            /// User đăng nhập
            /// </summary>
            public string User { get; set; }
            /// <summary>
            /// Mật khẩu đăng nhập elastic
            /// </summary>
            public string Pass { get; set; }
        }
    }
}
