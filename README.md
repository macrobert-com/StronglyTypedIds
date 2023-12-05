# StronglyTypedIds
In this package, we provide tools to cleanly integrate Strongly Typed IDs into ASP.NET Core Applications so they can be treated just as any other value type.

Strongly Typed IDs are a simple yet effective technique used in Domain-Driven Design (DDD) to enhance the clarity and safety of your code, particularly when dealing with the IDs of different entities.

## What are Strongly Typed IDs?

In many applications, the IDs of entities are often represented as primitive types, like int, long, Guid, or Ulid. However, this can sometimes lead to bugs or confusion when working with multiple different entities, as it's easy to accidentally use the ID from one entity in a place that expects the ID of a different entity.

To avoid these issues, a Strongly Typed ID wraps the ID in a custom type specific to the entity it identifies. This provides compile-time safety, ensuring that you can't accidentally use an ID of the wrong type.

### Example
```csharp
public record OrderId(Ulid Value) : IStronglyTypedId<Ulid>;
```
In the example above the ```OrderId``` is clearly associated with an Order (by Convention) and is passed around or compared like any other value-type.
Note also that because the ```OrderId``` is declared using the C# ```record``` type it also benefits from the value-comparison semantics associated with records.

## Benefits of Using Strongly Typed IDs

- Type Safety: With Strongly Typed IDs, you get compile-time safety. You can't accidentally pass a CustomerId when a method expects an OrderId.

- Expressiveness: Code is often read more times than it's written and Strongly Typed IDs make your code easier to understand.

- Domain Constraints: Strongly Typed IDs can enforce domain constraints. For example, if an ID should always be positive, that can be enforced in the type itself.

- Encapsulation: You can change the ID's internal representation without affecting the rest of the code.

The use of Strongly Typed IDs aligns well with the principles of Domain-Driven Design, where creating a rich, type-safe model of the domain is a key goal.
