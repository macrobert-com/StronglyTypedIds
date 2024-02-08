using MacRobert.Reflection;
using MacRobert.StronglyTypeIds;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Reflection;

namespace MacRobert.EntityFrameworkCore.StronglyTypedIds.ValueConversion;

public static class ModelConfigurationBuilderExtensions
{
    public static ModelConfigurationBuilder ConfigureStronglyTypedIds<T>(this ModelConfigurationBuilder builder, IEnumerable<Assembly> assemblies, int fieldWidth)
    {
        Action<IReadOnlyCollection<Type>> configureStronglyTypedIds = stronglyTypedIds =>
        {
            foreach (var type in stronglyTypedIds)
            {
                builder
                    .Properties(type)
                    .HaveConversion(typeof(StronglyTypedIdToStringConverter<,>).MakeGenericType(type, typeof(T)))
                    .HaveMaxLength(fieldWidth);
            }
        };

        foreach (var assembly in assemblies)
        {
            var scanner = new AssemblyScanner(assembly);
            var typedIdTypes = scanner.ScanForImplementations<IStronglyTypedId<T>>();
            configureStronglyTypedIds(typedIdTypes);
        }

        return builder;
    }
}
