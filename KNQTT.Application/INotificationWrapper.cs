using MediatR;

namespace KNQTT.Application
{
    public interface INotificationWrapper : INotification
    {
    }

    public interface INotificationHandlerWrapper<T> : INotificationHandler<T> where T : INotificationWrapper
    {
    }
}
