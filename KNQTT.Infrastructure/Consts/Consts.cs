namespace KNQTT.Infrastructure.Consts
{
    public class PolicyConsts
    {
        public const string MyPolicy = "MyPolicy";

        /// <summary>
        /// key: tên policy
        /// value: nhóm chức năng tương ứng
        /// </summary>
        public static readonly Dictionary<string, string> Policies = new Dictionary<string, string>
        {
        };
    }

    public class HttpClientName
    {
        public const string Number1Service = "Number1Service";
        public const string Number2Service = "Number2Service";
        public const string Number3Service = "Number3Service";
        public const string Number4Service = "Number4Service";
        public const string Number5Service = "Number5Service";
    }

    public class DeviceConsts
    {
        public const string DeviceIOS = "IOS";
        public const string DeviceAndroid = "Android";
        public const string DeviceDesktop = "Desktop";
        public const string DeviceWebSDK = "WebSDK";
        public const string DeviceMacOS = "MacOS";
        public const string DeviceLinux = "Linux";

        public static readonly List<string> Devices = new List<string>
        {
            DeviceIOS,
            DeviceAndroid,
            DeviceDesktop,
            DeviceWebSDK,
            DeviceMacOS,
            DeviceLinux
        };
    }


    public class DateTimeFormatConsts
    {
        public const string GeneralFormat = "dd/MM/yyyy HH:mm:ss";
        public const string ddMMyyyy = "dd/MM/yyyy";
    }
    public class ElasticSearchIndex
    {
        public const string Location = "location";
        public const string District = "district";
        public const string Ward = "ward";
        public const string Street = "liststreet";
        public const string Address = "address";
    }
}
