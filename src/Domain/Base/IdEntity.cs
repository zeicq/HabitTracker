namespace Domain.Base;

public interface IEntityId<T>
{
    public T Id { get; set; }
}