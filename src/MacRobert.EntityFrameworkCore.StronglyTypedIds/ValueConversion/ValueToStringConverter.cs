using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.ComponentModel;

namespace MacRobert.EntityFrameworkCore.StronglyTypedIds.ValueConversion;

public class ValueToStringConverter<T> : ValueConverter<T, string>
{
    public ValueToStringConverter(ConverterMappingHints mappingHints = null)
        : base(
            convertToProviderExpression: x => x.ToString()!,
            convertFromProviderExpression: x => (T)TypeDescriptor.GetConverter(typeof(T)).ConvertFromInvariantString(x)!,
            mappingHints: mappingHints)
    { }
}
