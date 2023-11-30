namespace MacRobert.AspNetCore.StronglyTypeIds;

public interface IStronglyTypedId<T>
{
    public T Value { get; }
}

