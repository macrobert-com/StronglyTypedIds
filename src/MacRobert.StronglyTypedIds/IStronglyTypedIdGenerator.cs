namespace MacRobert.StronglyTypeIds;

public interface IStronglyTypedIdGenerator<TUnderlyingType>
{
    TStronglyTypedId New<TStronglyTypedId>() where TStronglyTypedId : IStronglyTypedId<TUnderlyingType>;
}
