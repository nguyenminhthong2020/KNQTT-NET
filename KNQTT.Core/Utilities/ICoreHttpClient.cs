using System;
using System.Threading.Tasks;

namespace Infrastructure.Core.Utilities
{
    public interface ICoreHttpClient
    {
        Task<T> GetAsync<T>(string clientName, string uri) where T : class;
        Task<T> GetAsync<T>(string clientName, string uri, TimeSpan timeout) where T : class;
        Task<T> GetAsync<T>(string clientName, string uri, object reqObj) where T : class;
        Task<T> GetAsync<T>(string clientName, string uri, object reqObj, TimeSpan timeout) where T : class;
        Task<T> PostAsync<T>(string clientName, string uri, object reqObj) where T : class;
        Task<T> PostAsync<T>(string clientName, string uri, object reqObj, TimeSpan timeout) where T : class;
    }
}
