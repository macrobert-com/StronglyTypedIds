namespace MacRobert.com.AspNetCore.StronglyTypeIds;

public interface IStronglyTypedId<T>
{
    public T Value { get; }
}

