using MacRobert.StronglyTypeIds;

namespace Test.MacRobert.AspNetCore.StronglyTypeIds;

public record UlidTestId(Ulid Value) : IStronglyTypedId<Ulid>;

