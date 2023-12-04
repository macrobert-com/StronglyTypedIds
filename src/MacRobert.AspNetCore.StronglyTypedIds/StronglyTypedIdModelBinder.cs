using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel;

namespace MacRobert.AspNetCore.StronglyTypedIds;

public class StronglyTypedIdModelBinder<T> : IModelBinder
{
    public Task BindModelAsync(ModelBindingContext bindingContext)
    {
        var modelName = bindingContext.ModelName;
        var valueProviderResult = bindingContext.ValueProvider.GetValue(modelName);

        if (valueProviderResult == ValueProviderResult.None ||
            string.IsNullOrEmpty(valueProviderResult.FirstValue))
        {
            return Task.CompletedTask;
        }

        var value = valueProviderResult.FirstValue;
        var converter = TypeDescriptor.GetConverter(typeof(T));
        if (converter != null && converter.IsValid(value))
        {
            // Acquire the underlying value as the underlying Id-type.
            var convertedValue = (T)converter.ConvertFromString(value)!;
            // Create a boxed instance of the StrongTypeId, guided by the ModelType
            var instance = Activator.CreateInstance(bindingContext.ModelType, convertedValue);
            bindingContext.Result = ModelBindingResult.Success(instance);
        }
        else
        {
            bindingContext.ModelState.TryAddModelError(modelName, $"{typeof(T).Name} Conversion Error");
        }

        return Task.CompletedTask;
    }
}
