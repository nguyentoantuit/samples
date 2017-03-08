using System;
using System.Collections.Generic;
using System.Linq;

namespace Clone.Extensions
{
    internal static class CommonExtension
    {
        public static bool IsIListOfIClone(this Type type)
        {
            if (type.IsIListType())
            {
                return type.GetGenericArguments().Any(arg => arg.GetInterfaces().Any(iArg => iArg.GUID == typeof(IClone<>).GUID));
            }

            return false;
        }

        public static bool IsIListType(this Type type)
        {
            return type.IsGenericType && type.GetInterfaces().Any(x => x.GetGenericTypeDefinition() == typeof(IList<>));
        }

        public static IList<T> CloneICloneList<T>(this IList<T> test) where T : IClone<T>
        {
            return test.Select(x => x.FullClone()).ToList();
        }
    }
}
