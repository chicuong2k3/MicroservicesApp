using MediatR;

namespace Common_.CQRS
{
    public interface ICommandHandler<in TCommand, TResponse> 
        : IRequestHandler<TCommand, TResponse> 
        where TCommand : ICommand<TResponse>
        where TResponse : notnull
    {
    }
    public interface ICommandHandler<in TCommand> : ICommandHandler<TCommand, Unit>
        where TCommand : ICommand
    {
    }
}
