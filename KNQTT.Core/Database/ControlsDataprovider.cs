using System.Data;
using static Infrastructure.Core.Configuration.CoreAppSettingDetail;

namespace Infrastructure.Core.Database
{
    public static class CoreControlsDataprovider
    {
        private static ConnectionStringSettings connectionStrings;

        static CoreControlsDataprovider()
        {
            connectionStrings = new ConnectionStringSettings();
        }

        [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
        public class ControlsDataproviderAttribute : Attribute
        {
            public ControlsDataproviderAttribute(string db, string name, CommandType type)
            {
                this.Database = db;
                this.Name = name;
                this.Type = type;
            }
            public string Database { get; private set; }
            public string Name { get; private set; }
            public CommandType Type { get; private set; }
        }
    }
}
