using Infrastructure.Core.Models;
using MediatR;

namespace KNQTT.Application.Infrastructure
{
    public interface IRequestWrapper<T> : IRequest<ResultObject<T>>
    {
    }

    public interface IRequestHandlerWrapper<TIn, TOut> : IRequestHandler<TIn, ResultObject<TOut>>
        where TIn : IRequestWrapper<TOut>
    {
    }
}
