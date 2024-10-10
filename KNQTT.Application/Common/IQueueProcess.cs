namespace KNQTT.Application.Common
{
    public interface IQueueProcess
    {
        public Task<bool> PushQueue();
    }
}
