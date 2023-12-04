using MacRobert.StronglyTypeIds;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace MacRobert.EntityFrameworkCore.StronglyTypedIds.ValueConversion;

public class StronglyTypedIdToValueConverter<T, U> : ValueConverter<T, U> where T : IStronglyTypedId<U>
{
    public StronglyTypedIdToValueConverter(ConverterMappingHints? mappingHints = null)
        : base(
            convertToProviderExpression: x => x.Value,
            convertFromProviderExpression: x => (T)Activator.CreateInstance(typeof(T), new object[] { x })!,
            mappingHints)
    { }
}
