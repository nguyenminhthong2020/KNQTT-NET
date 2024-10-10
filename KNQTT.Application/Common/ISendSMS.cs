namespace KNQTT.Application.Common
{
    public interface ISendSMS
    {
        Task<(bool, string)> ResetShortLink();
        Task<(bool, string)> ResetPreview();
    }
}
