using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace MacRobert.EntityFrameworkCore.StronglyTypedIds.ValueConversion;

public class UlidToGuidValueConverter : ValueConverter<Ulid, Guid>
{
    public UlidToGuidValueConverter()
        : base(
            convertToProviderExpression: x => x.ToGuid(),
            convertFromProviderExpression: x => new Ulid(x))
    { }
}