using System;
using System.Linq;

namespace MyVanity.Model
{
    public static class ModelConverterHelper
    {
        /// <summary>
        /// Maps all properties from one object to another
        /// </summary>
        /// <typeparam name="TSource">Type of the source object</typeparam>
        /// <typeparam name="TDestination">Type of the destination object</typeparam>
        /// <param name="sample">Source sample to use</param>
        /// <param name="toFill">Destination item</param>
        /// <remarks>Currently, the method will overwrite the destination, but it will be fixed</remarks>
        /// <returns>The mapped object</returns>
        public static TDestination CopyObjectProperties<TSource, TDestination>(TSource sample, TDestination toFill)
        {
            if (toFill.Equals(default(TDestination)))
            {
                toFill = Activator.CreateInstance<TDestination>();
            }

            var sourceProperties = typeof(TSource).GetProperties();
            var destinProperties = typeof(TDestination).GetProperties();

            foreach (var property in sourceProperties)
            {
                var existing = destinProperties.SingleOrDefault(x => x.Name == property.Name && x.PropertyType.IsAssignableFrom(property.PropertyType));

                if (existing == null)
                    continue;

                if (!existing.CanWrite)
                    continue;

                var value = property.GetValue(sample, new object[] { });
                existing.SetValue(toFill, value, new object[] { });
            }

            return toFill;
        }
    }
}
