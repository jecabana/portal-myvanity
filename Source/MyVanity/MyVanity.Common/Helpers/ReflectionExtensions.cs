using System;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace MyVanity.Common.Helpers
{
    public class ReflectionExtensions
    {
        public static PropertyInfo GetPropertyInfo<TSource, TProperty>(Expression<Func<TSource, TProperty>> propertyLambda)
        {
            var type = typeof(TSource);

            var member = propertyLambda.Body as MemberExpression;

            if (member == null)
                throw new ArgumentException(string.Format("Expression '{0}' refers to a method, not a property.", propertyLambda));

            var propInfo = member.Member as PropertyInfo;

            if (propInfo == null)
                throw new ArgumentException(string.Format("Expression '{0}' refers to a field, not a property.", propertyLambda));

            if (type != propInfo.ReflectedType && !type.IsSubclassOf(propInfo.ReflectedType))
                throw new ArgumentException(string.Format("Expresion '{0}' refers to a property that is not from type {1}.", propertyLambda, type));

            return propInfo;
        }

        public static PropertyInfo GetProperty(Type type, string name)
        {
            if (name == null)
                return null;

            var props = name.Split('.');

            var property = type.GetProperties().SingleOrDefault(x => x.Name.ToLower() == props[0].ToLower());

            if (property == null || props.Count() == 1)
            {
                return property;
            }

            var left = string.Join(".", props, 1, props.Count() - 1);

            return GetProperty(property.PropertyType, left);
        }

        public static object GetPropValue(object target, string propName)
        {
            return target.GetType().GetProperty(propName).GetValue(target, null);
        }

        /// <summary>
        /// Gets the Description for a given enum value
        /// </summary>
        /// <param name="value">Enum item</param>
        /// <returns>The description of the item or the enum casted to string</returns>
        public static string GetEnumDescription(Enum value)
        {
            var attribute = value.GetType()
                                 .GetField(value.ToString())
                                 .GetCustomAttributes(typeof(DescriptionAttribute), false)
                                 .SingleOrDefault() as DescriptionAttribute;

            return attribute == null ? value.ToString() : attribute.Description;
        }

        public static Expression<Func<TEntity, TResult>> GetExpression<TEntity, TResult>(string prop)
        {
            var param = Expression.Parameter(typeof(TEntity), "p");
            var parts = prop.Split('.');

            Expression parent = parts.Aggregate<string, Expression>(param, Expression.Property);
            //Expression conversion = Expression.Convert(parent, typeof(object));                                   //not supported by Linq2Entities

            //var tryExpression = Expression.TryCatch(Expression.Block(typeof(object), conversion),                 //not supported by Linq2Entities
            //                                        Expression.Catch(typeof(object), Expression.Constant(null)));

            return Expression.Lambda<Func<TEntity, TResult>>(parent, param);
        }

        public static Expression<Func<TEntity, TResult>> GetPropertyAccessorWithType<TEntity, TResult>(string propertyName)
        {
            var param = Expression.Parameter(typeof(TEntity), "p");
            var member = Expression.Property(param, propertyName);

            var expression = Expression.Lambda<Func<TEntity, TResult>>(member, param);
            return expression;
        }
    }
}
