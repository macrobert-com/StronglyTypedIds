using MacRobert.StronglyTypeIds;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace MacRobert.AspNetCore.StronglyTypedIds;

public class StronglyTypedIdModelBinderProvider<T> : IModelBinderProvider
{
    public IModelBinder GetBinder(ModelBinderProviderContext context)
    {
        ArgumentNullException.ThrowIfNull(context, nameof(context));

        if (context.Metadata.ModelType.GetInterfaces().Any(
            x =>
                x.IsGenericType &&
                x.GetGenericTypeDefinition() == typeof(IStronglyTypedId<>) &&
                x.GenericTypeArguments[0] == typeof(T)))
        {
            return new StronglyTypedIdModelBinder<T>();
        }

        return null;
    }
}
