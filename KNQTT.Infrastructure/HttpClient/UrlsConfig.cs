using Infrastructure.Core.Multiversion;

namespace KNQTT.Infrastructure.HttpClient
{
    /// <summary>
    /// 
    /// </summary>
    public class UrlsConfig
    {
        
        /// <summary>
        /// Service deploy
        /// </summary>
        public class Number1
        {
            const string _routePrefix = "/number1/" + ServiceInstance.ServiceVersion + "/api/";
            /// <summary>
            /// 
            /// </summary>
            public const string Get = _routePrefix + "v1.0/get";
            

        }
        /// <summary>
        /// 
        /// </summary>
        public class Number2Service
        {
            const string _routePrefix = "/number2";
            /// <summary>
            /// 
            /// </summary>
            public const string Get = _routePrefix + "api/get";
        }
    }
}