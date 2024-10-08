namespace Infrastructure.Core.CoreOptions
{
    internal class ProjectConfig
    {
        public static CoreOption Options { get; set; }
    }

    public class CoreOption
    {
        /// <summary>
        /// Thư mục ghi log
        /// </summary>
        public  string LogFolder { get; set; }

        /// <summary>
        /// Các endpoint không ghi log (vd health)
        /// </summary>
        public  List<string> ExcludeEndpoint { get; set; }

        /// <summary>
        /// Tên project
        /// </summary>
        public  string ProjectName { get; set; }

        /// <summary>
        /// Refix của route (vd auth, account)
        /// </summary>
        public  string RoutePrefix { get; set; }

        /// <summary>
        /// Path file master data
        /// Ex: Data/MasterData.json
        /// </summary>
        public string MasterFilePath { get; set; }

        /// <summary>
        /// (Optional)
        /// Project chạy rule engine
        /// </summary>
        public string ExecuteRuleProjectName { get; set; }
    }
}
