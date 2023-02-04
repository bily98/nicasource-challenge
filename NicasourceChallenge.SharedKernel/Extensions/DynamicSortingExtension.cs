using Ardalis.Specification;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NicasourceChallenge.SharedKernel.Extensions
{
    public static class DynamicSortingExtension
    {
        public static ISpecificationBuilder<TSource> ThenBy<TSource>(
            this ISpecificationBuilder<TSource> specificationBuilder, string propertyName)
        {
            return BuildQuery(specificationBuilder, propertyName, OrderTypeEnum.ThenBy);
        }

        public static ISpecificationBuilder<TSource> ThenBy<TSource>(
            this ISpecificationBuilder<TSource> specificationBuilder, Expression<Func<TSource, object>> keySelector)
        {
            ((List<(Expression<Func<TSource, object>>, OrderTypeEnum)>) specificationBuilder.Specification
                    .OrderExpressions)
                .Add((keySelector, OrderTypeEnum.ThenBy));

            return specificationBuilder;
        }

        public static ISpecificationBuilder<TSource> OrderBy<TSource>(
            this ISpecificationBuilder<TSource> specificationBuilder, string propertyName)
        {
            return BuildQuery(specificationBuilder, propertyName, OrderTypeEnum.OrderBy);
        }

        public static ISpecificationBuilder<TSource> ThenBy<TSource>(
            this ISpecificationBuilder<TSource> specificationBuilder, string propertyName,
            bool isDescending)
        {
            if (isDescending)
                return BuildQuery(specificationBuilder, propertyName, OrderTypeEnum.ThenByDescending);
            else
                return BuildQuery(specificationBuilder, propertyName, OrderTypeEnum.ThenBy);
        }

        public static ISpecificationBuilder<TSource> OrderBy<TSource>(
            this ISpecificationBuilder<TSource> specificationBuilder, string propertyName,
            bool isDescending)
        {
            if (isDescending)
                return BuildQuery(specificationBuilder, propertyName, OrderTypeEnum.OrderByDescending);
            else
                return BuildQuery(specificationBuilder, propertyName, OrderTypeEnum.OrderBy);
        }

        public static ISpecificationBuilder<TSource> ThenByDescending<TSource>(
            this ISpecificationBuilder<TSource> specificationBuilder, string propertyName)
        {
            return BuildQuery(specificationBuilder, propertyName, OrderTypeEnum.ThenByDescending);
        }

        public static ISpecificationBuilder<TSource> OrderByDescending<TSource>(
            this ISpecificationBuilder<TSource> specificationBuilder,
            string propertyName)
        {
            return BuildQuery(specificationBuilder, propertyName, OrderTypeEnum.OrderByDescending);
        }

        static ISpecificationBuilder<TSource> BuildQuery<TSource>(ISpecificationBuilder<TSource> specificationBuilder,
            string propertyName, OrderTypeEnum orderTypeEnum)
        {
            // Initalized entity type
            var entityType = typeof(TSource);

            // Declare a type for lambda expression
            Type funcType = typeof(Func<,>).MakeGenericType(typeof(TSource), typeof(object));

            // Getting a info of property if exists in entity
            var propertyInfo = entityType.GetProperty(propertyName);
            if (propertyInfo == null)
                throw new ArgumentOutOfRangeException(nameof(propertyName), "Unknown column " + propertyName);

            // Is property is a collection
            bool isCollection = propertyInfo.PropertyType.GetInterfaces()
                .Any(x => x == typeof(IEnumerable));

            // Declare the argument and property of entity for lambda expression
            var arg = Expression.Parameter(entityType, "x"); // x =>
            var property = Expression.Property(arg, propertyName); // x.Property

            // Validating if is a collection
            if (!(isCollection && propertyInfo.PropertyType != typeof(string)))
            {
                // Create a lambda expression
                var selector = Expression.Lambda(funcType, Expression.Convert(property, typeof(object)),
                    new ParameterExpression[] {arg}); // x => x.Property

                // Add lambda expression to OrderExpressions Specification
                ((List<(Expression<Func<TSource, object>>, OrderTypeEnum)>) specificationBuilder.Specification
                        .OrderExpressions)
                    .Add(((Expression<Func<TSource, object>>) selector, orderTypeEnum));
            }

            return specificationBuilder;
        }
    }
}
