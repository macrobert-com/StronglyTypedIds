using System.Reflection;

namespace MacRobert.Reflection;

public class AssemblyScanner
{
    private readonly Assembly targetAssembly;

    public AssemblyScanner(Assembly targetAssembly)
    {
        this.targetAssembly = targetAssembly;
    }

    public IReadOnlyDictionary<Type, TAttribute> ScanForAttribute<TAttribute>() where TAttribute : Attribute
    {
        var result = new Dictionary<Type, TAttribute>();
        var types = targetAssembly.GetTypes();
        var typesWithSchemaAttribute = types.Where(t => t.GetCustomAttributes(typeof(TAttribute), false).Length > 0);
        foreach (var type in typesWithSchemaAttribute)
        {
            var attribute = (TAttribute)type.GetCustomAttributes(typeof(TAttribute), false).First();
            result.Add(type, attribute);
        }
        return result;
    }

    public IReadOnlyCollection<Type> ScanForImplementations<TInterface>()
    {
        var result = new List<Type>();
        var targetTypes = targetAssembly.GetTypes();
        var implementationTypes = targetTypes.Where(t => typeof(TInterface).IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract);

        // Print the names of the types that implement the ICustomCommand interface
        foreach (var type in implementationTypes)
        {
            result.Add(type);
        }
        return result;
    }
}

