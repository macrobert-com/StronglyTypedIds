using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace MacRobert.AspNetCore.StronglyTypedIds;

public static class StronglyTypedIdModelBinderProviderExtensions
{
    public static void AddStronglyTypedIdModelBinders<TUnderlyingData>(this IList<IModelBinderProvider> modelBinderProviders)
    {
        modelBinderProviders.Insert(0, new StronglyTypedIdModelBinderProvider<TUnderlyingData>());
    }
}