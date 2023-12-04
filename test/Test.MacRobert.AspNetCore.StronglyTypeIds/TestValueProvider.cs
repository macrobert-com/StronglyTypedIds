using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Primitives;

namespace Test.MacRobert.AspNetCore.StronglyTypeIds;

public class TestValueProvider : IValueProvider
{
    private readonly Dictionary<string, string> _values;

    public TestValueProvider(Dictionary<string, string> values)
    {
        _values = values;
    }

    public bool ContainsPrefix(string prefix)
    {
        return _values.ContainsKey(prefix);
    }

    public ValueProviderResult GetValue(string key)
    {
        if (_values.TryGetValue(key, out var val))
        {
            return new ValueProviderResult(new StringValues(val.ToString()));
        }

        return ValueProviderResult.None;
    }
}

