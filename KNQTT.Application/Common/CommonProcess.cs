using Infrastructure.Core.Database;
using Infrastructure.Core.Utilities;

namespace KNQTT.Application.Common
{
    public class CommonProcess : ICommonProcess
    {
        private readonly ICoreHttpClient _coreHttpClient;
        private readonly IQuery _query;
        public CommonProcess(ICoreHttpClient coreHttpClient, IQuery query)
        {
            _coreHttpClient = coreHttpClient;
            _query = query;
        }


        public async Task<List<object>> GetAllService()
        {
            return default;
        }

    }
}
