using System;
using System.Collections.Generic;
using System.Linq;

namespace Clone
{
    public class BasicClone<T> : IClone<T> where T : new()
    {

        public T FullClone()
        {
            CheckObjectType();

            var newObj = new T();
            var type = GetType();
            foreach (var propertyInfo in type.GetProperties())
            {
                var value = propertyInfo.GetValue(this);
                if (IsIListType(value.GetType()))
                {
                    IList<BasicClone<T>> test = value as IList<BasicClone<T>>;
                    var newValue = ((IList<BasicClone<T>>)value).Select(x => x.FullClone());
                    propertyInfo.SetValue(newObj, newValue, null);

                }
                else
                {
                    propertyInfo.SetValue(newObj, value, null);
                }
            }

            return newObj;
        }

        /// <summary>
        /// Create new object and copy all normal properties value to new object.
        /// Please note that this method doesn't support clone properties which have type of List.
        /// </summary>
        /// <returns>New imitated object</returns>
        public T ShallowClone()
        {
            CheckObjectType();

            return (T)MemberwiseClone();
        }

        private void CheckObjectType()
        {
            if (GetType() != typeof(T))
            {
                throw new ArgumentException("Type " + GetType() + " does not implement interface " + typeof(T));
            }
        }

        private static bool IsIListType(Type type)
        {
            return type.IsGenericType && type.GetInterfaces().Any(x => x == typeof(IList<BasicClone<T>>));
        }
    }
}
