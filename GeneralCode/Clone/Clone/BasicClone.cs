using Clone.Extensions;
using Clone.Helpers;
using System;
using System.Collections;

namespace Clone
{
    public class BasicClone<T> : IClone<T> where T : new()
    {
        private const string MemberwiseCloneMethod = "MemberwiseClone";

        public T FullClone()
        {
            CheckObjectType();
            var fullCloneName = System.Reflection.MethodBase.GetCurrentMethod().Name;

            var newObj = new T();
            var type = GetType();
            foreach (var propertyInfo in type.GetProperties())
            {
                var value = propertyInfo.GetValue(this);
                if (value.GetType().IsIListType())
                {
                    var listValue = value as IList;
                    IList destList = Activator.CreateInstance(value.GetType()) as IList;
                    if (value.GetType().IsIListOfIClone())
                    {
                        CloneListItems(listValue, destList, fullCloneName);
                    }
                    else
                    {
                        // Process with normal list
                        CloneListItems(listValue, destList, MemberwiseCloneMethod);

                    }
                    propertyInfo.SetValue(newObj, destList, null);
                }
                else
                {
                    propertyInfo.SetValue(newObj, value, null);
                }
            }

            return newObj;
        }

        private static void CloneListItems(IList listValue, IList destList, string methodName)
        {
            if (listValue != null && listValue.Count > 0)
            {
                foreach (var item in listValue)
                {
                    var type = item.GetType();

                    var method = type.GetMethod(methodName);
                    if (method != null)
                    {
                        destList.Add(method.Invoke(item, null));
                    }
                    else
                    {
                        destList.Add(item);
                    }
                }
            }
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
            if (!CommonHelper.CheckType(this, typeof(T)))
            {
                throw new ArgumentException("Type " + GetType() + " does not implement interface " + typeof(T));
            }
        }
    }
}
