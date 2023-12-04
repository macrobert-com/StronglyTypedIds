using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Linq.Expressions;

namespace MacRobert.EntityFrameworkCore.StronglyTypedIds.ValueConversion;

public class CompositeValueConverter<TModel, TMiddle, TProvider> : ValueConverter<TModel, TProvider>
{
    public CompositeValueConverter(ValueConverter lhsConverter, ValueConverter rhsConverter, ConverterMappingHints? mappingHints = null)
        : base(
            Compose((Expression<Func<TModel, TMiddle>>)lhsConverter.ConvertToProviderExpression, (Expression<Func<TMiddle, TProvider>>)rhsConverter.ConvertToProviderExpression),
            Compose((Expression<Func<TProvider, TMiddle>>)rhsConverter.ConvertFromProviderExpression, (Expression<Func<TMiddle, TModel>>)lhsConverter.ConvertFromProviderExpression), mappingHints)
    { }

    private static Expression<Func<TIn, TOut>> Compose<TIn, TOut>(Expression<Func<TIn, TMiddle>> upper, Expression<Func<TMiddle, TOut>> lower)
    {
        return Expression.Lambda<Func<TIn, TOut>>(ReplacingExpressionVisitor.Replace(lower.Parameters.Single(), upper.Body, lower.Body), new ParameterExpression[1] { upper.Parameters.Single() });
    }
}