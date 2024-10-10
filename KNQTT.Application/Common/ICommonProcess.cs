namespace KNQTT.Application.Common
{
    public interface ICommonProcess
    {
        /// <summary>
        /// Lấy danh sách tất cả Service. Hàm này có dùng cache
        /// </summary>
        /// <returns></returns>
        Task<List<object>> GetAllService();

    }
}
