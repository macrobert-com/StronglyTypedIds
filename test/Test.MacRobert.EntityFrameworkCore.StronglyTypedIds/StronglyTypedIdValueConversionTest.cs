using MacRobert.EntityFrameworkCore.StronglyTypedIds.ValueConversion;
using MacRobert.StronglyTypeIds;

namespace Test.MacRobert.EntityFrameworkCore.StronglyTypeIds;

public record CompanyId(Ulid Value) : IStronglyTypedId<Ulid>;

public record MemberId(Guid Value) : IStronglyTypedId<Guid>;

public class StronglyTypedIdValueConversionTest
{
    static readonly Guid TestGuid = Guid.Parse("5b431432-26a6-4a06-b318-206755e0a1bc");
    static readonly Ulid TestUlid = Ulid.Parse("01HGSQ59GCKF4DK5XTWFEQ2PA3");

    [Fact]
    public void CanConvert_String_To_Ulid_StronglyTypedId()
    {
        var sourceValue = "01HGSQ59GCKF4DK5XTWFEQ2PA3";
        var referenceIdValue = new CompanyId(TestUlid);
        var converter = new StronglyTypedIdToStringConverter<CompanyId, Ulid>();
        var stronglyTypedIdValue = (CompanyId)converter.ConvertFromProvider(sourceValue)!;
        string providerValue = (string)converter.ConvertToProvider(stronglyTypedIdValue)!;

        Assert.Equal(sourceValue, providerValue);
        Assert.Equal(referenceIdValue, stronglyTypedIdValue);
    }

    [Fact]
    public void CanConvert_String_To_Guid_StronglyTypedId()
    {
        var sourceValue = "5b431432-26a6-4a06-b318-206755e0a1bc";
        var referenceIdValue = new MemberId(TestGuid);
        var converter = new StronglyTypedIdToStringConverter<MemberId, Guid>();
        var stronglyTypedIdValue = (MemberId)converter.ConvertFromProvider(sourceValue)!;
        string providerValue = (string)converter.ConvertToProvider(stronglyTypedIdValue)!;

        Assert.Equal(sourceValue, providerValue);
        Assert.Equal(referenceIdValue, stronglyTypedIdValue);
    }

    [Fact]
    public void CanConvert_StronglyTypedUlid_To_Guid()
    {
        var referenceIdValue = new CompanyId(TestUlid);
        var converter = new StronglyTypedUlidToGuidValueConverter<CompanyId>();
        var providerGuid = (Guid)converter.ConvertToProvider(referenceIdValue)!;
        Assert.Equal("018c3372-a60c-9bc8-d997-bae3dd715943", providerGuid.ToString());

        var testValue = (CompanyId)converter.ConvertFromProvider(providerGuid)!;
        Assert.Equal(referenceIdValue, testValue);
    }
}
