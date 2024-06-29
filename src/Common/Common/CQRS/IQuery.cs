using MediatR;

namespace Common_.CQRS
{
    public interface IQuery<out TResponse> : IRequest<TResponse> where TResponse : notnull
    {
    }
    public interface IQuery : IQuery<Unit>
    {

    }
}
