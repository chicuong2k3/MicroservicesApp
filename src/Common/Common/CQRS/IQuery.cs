using MediatR;

namespace Common.CQRS
{
    public interface IQuery<out TResponse> : IRequest<TResponse> where TResponse : notnull
    {
    }
    public interface IQuery : IQuery<Unit>
    {

    }
}
