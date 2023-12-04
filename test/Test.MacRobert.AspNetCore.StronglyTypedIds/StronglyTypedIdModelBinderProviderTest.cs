using MacRobert.AspNetCore.StronglyTypedIds;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;

namespace Test.MacRobert.AspNetCore.StronglyTypeIds;

public class StronglyTypedIdModelBinderProviderTest
{
    [Fact]
    public void GetBinder_ReturnsCorrectBinderForStronglyTypedId()
    {
        // Arrange
        var modelType = typeof(GuidTestId);
        var modelMetadataProvider = new EmptyModelMetadataProvider();
        var modelMetadata = modelMetadataProvider.GetMetadataForType(modelType);
        var modelBinderProviderContext = new StubModelBinderProviderContext(modelMetadata);

        var provider = new StronglyTypedIdModelBinderProvider<Guid>();

        // Act
        var binder = provider.GetBinder(modelBinderProviderContext);

        // Assert
        Assert.IsType<StronglyTypedIdModelBinder<Guid>>(binder);
    }

    [Fact]
    public void GetBinder_ReturnsNullForNonStronglyTypedId()
    {
        // Arrange
        var modelType = typeof(int);  // Non-strongly-typed ID type
        var modelMetadataProvider = new EmptyModelMetadataProvider();
        var modelMetadata = modelMetadataProvider.GetMetadataForType(modelType);
        var modelBinderProviderContext = new StubModelBinderProviderContext(modelMetadata);

        var provider = new StronglyTypedIdModelBinderProvider<Guid>();

        // Act
        var binder = provider.GetBinder(modelBinderProviderContext);

        // Assert
        Assert.Null(binder);
    }

    public class StubModelBinderProviderContext : ModelBinderProviderContext
    {
        public StubModelBinderProviderContext(ModelMetadata metadata)
        {
            Metadata = metadata;
        }

        public override ModelMetadata Metadata { get; }

        public override BindingInfo BindingInfo => throw new NotImplementedException();

        public override IModelMetadataProvider MetadataProvider => throw new NotImplementedException();

        public override IModelBinder CreateBinder(ModelMetadata metadata) => throw new NotImplementedException();
    }

}