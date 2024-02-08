namespace MacRobert.StronglyTypeIds;

public class StronglyTypedGuidIdGenerator : IStronglyTypedGuidIdGenerator
{
    public TStronglyTypedId New<TStronglyTypedId>() where TStronglyTypedId : IStronglyTypedId<Guid>
    {
        return (TStronglyTypedId)Activator.CreateInstance(typeof(TStronglyTypedId), new object[] { Guid.NewGuid() })!;
    }
}