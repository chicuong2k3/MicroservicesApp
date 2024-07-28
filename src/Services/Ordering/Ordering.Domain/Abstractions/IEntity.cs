namespace Ordering.Domain.Abstractions;

public interface IEntity
{
    DateTime? CreatedAt { get; set; }
    DateTime? LastModifiedAt { get; set; }
}

public interface IEntity<T> : IEntity
{
    T Id { get; set; }
}
