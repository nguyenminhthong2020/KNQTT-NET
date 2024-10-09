namespace KNQTT.Infrastructure
{
    public interface ICoreHttpClientCustomize
    {
        Task<T> PostAsync<T>(string clientName, string uri, byte[] fileBytes, string fileName, string formName) where T : class;
        Task<T> PostAsync<T>(string clientName, string uri, object reqObj, Dictionary<String, String> Header = null) where T : class;
    }
}
