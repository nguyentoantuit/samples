using System;
using System.Collections;
using System.Collections.Generic;

namespace Clone.Helpers
{
    public static class CommonHelper
    {
        public static IList CreateListFromType(Type type)
        {
            Type listType = typeof(List<>);
            Type[] typeArgs = { type };
            Type makeme = listType.MakeGenericType(typeArgs);

            return Activator.CreateInstance(makeme) as IList;
        }

        public static bool CheckType<T>(T obj, Type type)
        {
            return obj.GetType() == type;
        }
    }
}
