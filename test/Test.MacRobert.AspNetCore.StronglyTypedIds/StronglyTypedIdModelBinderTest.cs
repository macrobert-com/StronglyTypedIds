using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using MacRobert.AspNetCore.StronglyTypedIds;

namespace Test.MacRobert.AspNetCore.StronglyTypeIds;

public class StronglyTypedIdModelBinderTest
{
    static readonly Guid TestGuid = Guid.Parse("5b431432-26a6-4a06-b318-206755e0a1bc");
    static readonly Ulid TestUlid = Ulid.Parse("01HGSQ59GCKF4DK5XTWFEQ2PA3");

    [Fact]
    public async Task BindModelAsync_BindsToGuid()
    {
        // Arrange
        var httpContext = new DefaultHttpContext();
        var modelState = new ModelStateDictionary();
        var metadataProvider = new EmptyModelMetadataProvider();
        var modelMetadata = metadataProvider.GetMetadataForType(typeof(GuidTestId));
        var valueProvider = new TestValueProvider(new Dictionary<string, string>
        {
            { nameof(GuidTestId.Value), TestGuid.ToString() } // Values supplied from the httpcontext are strings...
        });

        var binderProviderContext = new DefaultModelBindingContext
        {
            ModelMetadata = modelMetadata,
            ModelState = modelState,
            ActionContext = new ActionContext(httpContext, new RouteData(), new ActionDescriptor()),
            ModelName = nameof(GuidTestId.Value),
            ValueProvider = valueProvider,
        };

        var binder = new StronglyTypedIdModelBinder<Guid>();

        // Act 
        await binder.BindModelAsync(binderProviderContext);

        // Assert
        Assert.True(modelState.IsValid);
        Assert.IsType<GuidTestId>(binderProviderContext.Result.Model);
        Assert.Equal(TestGuid, ((GuidTestId)binderProviderContext.Result.Model).Value);
    }

    [Fact]
    public async Task BindModelAsync_BindsToUlid()
    {
        // Arrange
        var httpContext = new DefaultHttpContext();
        var modelState = new ModelStateDictionary();
        var metadataProvider = new EmptyModelMetadataProvider();
        var modelMetadata = metadataProvider.GetMetadataForType(typeof(UlidTestId));
        var valueProvider = new TestValueProvider(new Dictionary<string, string>
        {
            { nameof(UlidTestId.Value), TestUlid.ToString() } // Values supplied from the httpcontext are strings...
        });

        var binderProviderContext = new DefaultModelBindingContext
        {
            ModelMetadata = modelMetadata,
            ModelState = modelState,
            ActionContext = new ActionContext(httpContext, new RouteData(), new ActionDescriptor()),
            ModelName = nameof(UlidTestId.Value),
            ValueProvider = valueProvider,
        };

        var binder = new StronglyTypedIdModelBinder<Ulid>();

        // Act 
        await binder.BindModelAsync(binderProviderContext);

        // Assert
        Assert.True(modelState.IsValid);
        Assert.IsType<UlidTestId>(binderProviderContext.Result.Model);
        Assert.Equal(TestUlid, ((UlidTestId)binderProviderContext.Result.Model).Value);
    }
}
