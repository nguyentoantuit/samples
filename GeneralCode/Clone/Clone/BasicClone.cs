using Clone.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;

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
                if (value.GetType().IsIListOfIClone())
                {
                    var listValue = value as IList;
                    if (listValue.Count > 0)
                    {
                        Type d1 = typeof(List<>);

                        Type[] typeArgs = { listValue[0].GetType() };

                        Type makeme = d1.MakeGenericType(typeArgs);

                        IList destList = Activator.CreateInstance(makeme) as IList;

                        foreach (var ite in listValue)
                        {
                            var ICloneType = ite.GetType();
                            var method = ICloneType.GetMethod("FullClone");
                            var test = method.Invoke(ite, null);
                            destList.Add(test);
                        }

                        propertyInfo.SetValue(newObj, destList, null);
                    }
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
    }
}
