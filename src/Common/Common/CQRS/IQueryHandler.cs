using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common_.CQRS
{
    public interface IQueryHandler<in TQuery, TResponse>
        : IRequestHandler<TQuery, TResponse> 
        where TQuery : IQuery<TResponse>
        where TResponse : notnull
    {
    }
    public interface IQueryHandler<in TQuery> : IQueryHandler<TQuery, Unit>
        where TQuery : IQuery
    {
    }
}
