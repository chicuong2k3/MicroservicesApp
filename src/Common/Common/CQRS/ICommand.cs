﻿using MediatR;

namespace Common.CQRS
{
    public interface ICommand<out TResponse> : IRequest<TResponse> where TResponse : notnull
    {

    }

    public interface ICommand : ICommand<Unit>
    {

    }
}
