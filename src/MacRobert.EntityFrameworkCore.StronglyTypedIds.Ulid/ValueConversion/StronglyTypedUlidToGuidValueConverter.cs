using MacRobert.StronglyTypeIds;

namespace MacRobert.EntityFrameworkCore.StronglyTypedIds.ValueConversion;

public class StronglyTypedUlidToGuidValueConverter<T>
    : CompositeValueConverter<T, Ulid, Guid>
    where T : IStronglyTypedId<Ulid>
{
    public StronglyTypedUlidToGuidValueConverter()
        : base(
            new StronglyTypedIdToValueConverter<T, Ulid>(),
            new UlidToGuidValueConverter())
    { }
}
