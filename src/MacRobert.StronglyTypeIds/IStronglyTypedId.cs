namespace MacRobert.StronglyTypeIds;

public interface IStronglyTypedId<T>
{
    public T Value { get; }
}
