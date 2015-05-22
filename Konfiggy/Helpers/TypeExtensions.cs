using System;
using System.Collections;
using System.Linq;

namespace KonfiggyFramework.Helpers
{
    internal static class TypeExtensions
    {
        public static IEnumerable CastGeneric(this IEnumerable self, Type innerType)
        {
            var methodInfo = typeof(Enumerable).GetMethod("Cast");
            var genericMethod = methodInfo.MakeGenericMethod(innerType);
            return genericMethod.Invoke(null, new[] { self }) as IEnumerable;
        }
    }
}