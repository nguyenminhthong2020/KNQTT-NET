using System.Reflection;
using static Infrastructure.Core.Configuration.CoreAppSettingDetail;
using static Infrastructure.Core.Database.CoreControlsDataprovider;

namespace KNQTT.Infrastructure.Database
{
    public static class ControlsDataprovider
    {
        /// <summary>
        /// 
        /// </summary>
        private static ConnectionStringSettings connectionStrings;

        public static class StoreAndFunctionInfo
        {
            /// <summary>
            /// StoreName
            /// </summary>
            //[ControlsDataprovider(nameof(connectionStrings.DatabaseRead), "Database..StoreName", CommandType.StoredProcedure)]
            //public static string StoreName { get => "StoreName"; }
        }

        /// <summary>
        /// get value attribute
        /// </summary>
        /// <param name="propertyName">property cần lấy</param>
        /// <returns></returns>
        public static ControlsDataproviderAttribute GetAttribute(string propertyName)
        {
            PropertyInfo propertyInfo = typeof(StoreAndFunctionInfo).GetProperty(propertyName.ToString());
            ControlsDataproviderAttribute attribute = (ControlsDataproviderAttribute)Attribute.GetCustomAttribute(propertyInfo, typeof(ControlsDataproviderAttribute));
            if (attribute != null)
            {
                return attribute;
            }
            return null;
        }
    }
}
