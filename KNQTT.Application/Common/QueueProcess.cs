using Infrastructure.Core.Utilities;

namespace KNQTT.Application.Common
{
    public class QueueProcess : IQueueProcess
    {
        private readonly ICoreHttpClient _coreHttpClient;

        public QueueProcess(ICoreHttpClient coreHttpClient)
        {
            _coreHttpClient = coreHttpClient;
        }
        
        public async Task<bool> PushQueue()
        {
            return default;
        }
    }
}
