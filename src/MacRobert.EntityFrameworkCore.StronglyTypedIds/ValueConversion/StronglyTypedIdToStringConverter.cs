using MacRobert.EntityFrameworkCore.StronglyTypedIds;
using MacRobert.StronglyTypeIds;

namespace MacRobert.EntityFrameworkCore.StronglyTypedIds.ValueConversion;

public class StronglyTypedIdToStringConverter<T, U> : CompositeValueConverter<T, U, string> where T : IStronglyTypedId<U>
{
    public StronglyTypedIdToStringConverter()
        : base(
            new StronglyTypedIdToValueConverter<T, U>(),
            new ValueToStringConverter<U>())
    { }
}