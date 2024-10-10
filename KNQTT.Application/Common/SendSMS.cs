using Infrastructure.Core.Utilities;

namespace KNQTT.Application.Common
{
    public class SendSMS : ISendSMS
    {
        private readonly ICoreHttpClient _coreHttpClient;
        private readonly ICommonProcess _commonProcess;
        public SendSMS(ICoreHttpClient coreHttpClient, ICommonProcess commonProcess)
        {
            _coreHttpClient = coreHttpClient;
            _commonProcess = commonProcess;
        }
        public async Task<(bool, string)> ResetShortLink()
        {
            return default;
        }

        public async Task<(bool, string)> ResetPreview()
        {
            return default;
        }
    }
}
