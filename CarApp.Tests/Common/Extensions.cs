using System.Linq.Expressions;
using System.Reflection;

namespace CarApp.Tests.Common;

public static class Extensions
{
    public static void SetReadonlyCollection<TEntity, TItem>(
        this TEntity entity, Expression<Func<TEntity, IReadOnlyCollection<TItem>>> propertySelector, IEnumerable<TItem> enumerable) where TEntity : notnull
    {
        var expressionBody = (MemberExpression)propertySelector.Body;
        var propertName = expressionBody.Member.Name;
        var fieldName = $"_{char.ToLower(propertName[0]) + propertName[1..]}";

        var type = entity.GetType();
        FieldInfo? fieldInfo = null;
        while (fieldInfo == null)
        {
            if (type != null)
            {
                fieldInfo = type.GetField(fieldName, BindingFlags.NonPublic | BindingFlags.Instance);
                type = type.BaseType;
            }
        }

        fieldInfo.SetValue(entity, enumerable.ToList());
    }

    public static void SetPrivateField<TEntity>(
        this TEntity entity, string fieldName, object value) where TEntity : notnull
    {
        var field = entity
            .GetType()
            .GetField(fieldName, BindingFlags.NonPublic | BindingFlags.Instance);

        if (field == null)
        {
            throw new InvalidOperationException();

        }

        field.SetValue(entity, value);
    }

    public static void SetReadonlyProperty<TEntity, TProp>(
        this TEntity entity, Expression<Func<TEntity, TProp>> propertySelector, TProp value)
    {
        var member = ((MemberExpression)propertySelector.Body).Member;
        if (member is FieldInfo info)
        {
            info.SetValue(entity, value);
        }
        else
        {
            ((PropertyInfo)member).SetValue(entity, value, null);
        }
    }
}
