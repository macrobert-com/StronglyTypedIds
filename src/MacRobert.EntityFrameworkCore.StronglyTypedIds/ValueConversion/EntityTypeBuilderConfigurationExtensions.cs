using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Linq.Expressions;

namespace MacRobert.EntityFrameworkCore.StronglyTypedIds.ValueConversion;

public static class EntityTypeBuilderConfigurationExtensions
{
    public static EntityTypeBuilder<TEntity> HasStronglyTypedKey<TEntity, TEntityId>(this EntityTypeBuilder<TEntity> builder, Expression<Func<TEntity, TEntityId>> propertyExpression) where TEntity : class
    {
        builder.HasKey(propertyExpression.ConvertToBoxedExpression());
        builder.Property(propertyExpression)
            .IsRequired();

        builder.HasIndex(propertyExpression.ConvertToBoxedExpression())
            .IsUnique();

        return builder;
    }

    private static Expression<Func<TEntity, object?>> ConvertToBoxedExpression<TEntity, TEntityId>(this Expression<Func<TEntity, TEntityId>> keyAccessorExpression)
        where TEntity : class
    {
        var visitor = new ConvertToObjectTypeVisitor();
        Expression body = visitor.Visit(keyAccessorExpression.Body);

        return Expression.Lambda<Func<TEntity, object?>>(body, keyAccessorExpression.Parameters);
    }

    /// <summary>
    /// Visit the Expression AST then return the body as a boxed value.
    /// </summary>
    private class ConvertToObjectTypeVisitor : ExpressionVisitor
    {
        protected override Expression VisitMember(MemberExpression keyAccessorBody)
        {
            Expression boxedExpression = base.VisitMember(keyAccessorBody);
            if (boxedExpression.Type != typeof(object))
            {
                boxedExpression = Expression.Convert(boxedExpression, typeof(object));
            }
            return boxedExpression;
        }
    }
}