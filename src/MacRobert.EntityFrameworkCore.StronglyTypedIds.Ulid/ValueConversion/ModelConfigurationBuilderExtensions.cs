using MacRobert.EntityFrameworkCore.StronglyTypedIds.ValueConversion;
using MacRobert.Reflection;
using MacRobert.StronglyTypeIds;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace MacRobert.EntityFrameworkCore.StronglyTypedIds.ValueConversion;

public static class ModelConfigurationBuilderExtensionsUlid
{
    /// <summary>
    /// StronglyTypedIds that use Ulid as an underlying type may be successfully persisted as a Guid for those DBMS's that don't support Ulid natively.
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="assemblies"></param>
    /// <returns></returns>
    public static ModelConfigurationBuilder ConfigureStronglyTypedUlidAsGuid(this ModelConfigurationBuilder builder, IEnumerable<Assembly> assemblies)
    {
        Action<IReadOnlyCollection<Type>> configureStronglyTypedIds = stronglyTypedIds =>
        {
            foreach (var type in stronglyTypedIds)
            {
                builder
                    .Properties(type)
                    .HaveConversion(typeof(StronglyTypedUlidToGuidValueConverter<>).MakeGenericType(type));
            }
        };

        foreach (var assembly in assemblies)
        {
            var scanner = new AssemblyScanner(assembly);
            var typedIdTypes = scanner.ScanForImplementations<IStronglyTypedId<Ulid>>();
            configureStronglyTypedIds(typedIdTypes);
        }

        return builder;
    }
}