namespace Infrastructure.Core.Database
{
    public interface IQuery
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dataprovider"></param>
        /// <param name="param"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        IEnumerable<T> Query<T>(string dataprovider, object param = null, int commandTimeout = 20);

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dataprovider"></param>
        /// <param name="param"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        Task<IEnumerable<T>> QueryAsync<T>(string dataprovider, object param = null, int commandTimeout = 20);

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <param name="dataprovider"></param>
        /// <param name="param"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        (IEnumerable<T1>, IEnumerable<T2>) QueryMulitple<T1, T2>(string dataprovider, object param = null, int commandTimeout = 20);

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <param name="dataprovider"></param>
        /// <param name="param"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        Task<(IEnumerable<T1>, IEnumerable<T2>)> QueryMulitpleAsync<T1, T2>(string dataprovider, object param = null, int commandTimeout = 20);

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        /// <param name="dataprovider"></param>
        /// <param name="param"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        (IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>) QueryMulitple<T1, T2, T3>(string dataprovider, object param = null, int commandTimeout = 20);

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        /// <param name="dataprovider"></param>
        /// <param name="param"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        Task<(IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>)> QueryMulitpleAsync<T1, T2, T3>(string dataprovider, object param = null, int commandTimeout = 20);

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        /// <typeparam name="T4"></typeparam>
        /// <param name="dataprovider"></param>
        /// <param name="param"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        (IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>, IEnumerable<T4>) QueryMulitple<T1, T2, T3, T4>(string dataprovider, object param = null, int commandTimeout = 20);

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="connectionString"></param>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        IEnumerable<T> Query<T>(string connectionString, string sql, object param = null, int commandTimeout = 20);

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <param name="connectionString"></param>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        (IEnumerable<T1>, IEnumerable<T2>) QueryMulitple<T1, T2>(string connectionString, string sql, object param = null, int commandTimeout = 20);

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <param name="connectionString"></param>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        Task<(IEnumerable<T1>, IEnumerable<T2>)> QueryMulitpleAsync<T1, T2>(string connectionString, string sql, object param = null, int commandTimeout = 20);

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        /// <param name="connectionString"></param>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        (IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>) QueryMulitple<T1, T2, T3>(string connectionString, string sql, object param = null, int commandTimeout = 20);

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        /// <param name="connectionString"></param>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        Task<(IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>)> QueryMulitpleAsync<T1, T2, T3>(string connectionString, string sql, object param = null, int commandTimeout = 20);

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        /// <typeparam name="T4"></typeparam>
        /// <param name="connectionString"></param>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        (IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>, IEnumerable<T4>) QueryMulitple<T1, T2, T3, T4>(string connectionString, string sql, object param = null, int commandTimeout = 20);

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="connectionString"></param>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        Task<IEnumerable<T>> QueryAsync<T>(string connectionString, string sql, object param = null, int commandTimeout = 20);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        int Execute(string connectionString, string sql, object param = null, int commandTimeout = 20);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        Task<int> ExecuteAsync(string connectionString, string sql, object param = null, int commandTimeout = 20);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        T ExecuteScalar<T>(string connectionString, string sql, object param = null, int commandTimeout = 20);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        Task<T> ExecuteScalarAsync<T>(string connectionString, string sql, object param = null, int commandTimeout = 20);
    }
}