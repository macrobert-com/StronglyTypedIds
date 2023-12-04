using MacRobert.StronglyTypeIds;

namespace Test.MacRobert.AspNetCore.StronglyTypeIds;

public record GuidTestId(Guid Value) : IStronglyTypedId<Guid>;

