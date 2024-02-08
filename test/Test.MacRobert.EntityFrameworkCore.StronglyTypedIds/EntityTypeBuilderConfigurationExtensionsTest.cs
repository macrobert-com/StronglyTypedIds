using MacRobert.EntityFrameworkCore.StronglyTypedIds.ValueConversion;
using MacRobert.StronglyTypeIds;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Test.MacRobert.EntityFrameworkCore.StronglyTypeIds;

public class EntityTypeBuilderConfigurationExtensionsTest
{
    public record TestEntityId(Ulid Value) : IStronglyTypedId<Ulid>;

    public class TestEntity
    {
        public TestEntityId Id { get; }
    }

    public class TestConfiguration : IEntityTypeConfiguration<TestEntity>
    {
        public void Configure(EntityTypeBuilder<TestEntity> builder)
        {
            builder.HasStronglyTypedKey(t => t.Id);
        }
    }

    [Fact]
    public void HasStronglyTypedKey_Applies_Key_And_Index()
    {
        // Arrange
        var builder = new ModelBuilder();
        var configuration = new TestConfiguration();
        // Act
        builder.ApplyConfiguration(configuration);
        var model = builder.FinalizeModel();
        var entityTypeMetadata = model.FindEntityType(typeof(TestEntity));

        // Assert PK
        Assert.NotNull(entityTypeMetadata);
        var primaryKeyMetadata = entityTypeMetadata.FindPrimaryKey();
        Assert.NotNull(primaryKeyMetadata);
        Assert.Equal(nameof(TestEntity.Id), primaryKeyMetadata.Properties[0].Name);

        // Assert Index
        var indexMetadata = entityTypeMetadata.GetIndexes().SingleOrDefault(i => i.Properties.Any(p => p.Name == nameof(TestEntity.Id)));
        Assert.NotNull(indexMetadata);
        Assert.True(indexMetadata.IsUnique);
    }
}