using Clone.Extensions;
using Clone.Helpers;
using System;
using System.Collections;

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
                if (value.GetType().IsIListType())
                {
                    if (value.GetType().IsIListOfIClone())
                    {
                        IList destList;
                        var listValue = value as IList;
                        if (listValue.Count > 0)
                        {
                            destList = CommonHelper.CreateListFromType(listValue[0].GetType());
                            foreach (var ite in listValue)
                            {
                                var ICloneType = ite.GetType();
                                var method = ICloneType.GetMethod("FullClone");
                                var test = method.Invoke(ite, null);
                                destList.Add(test);
                            }

                            propertyInfo.SetValue(newObj, destList, null);
                        }
                        else
                        {
                            // Set empty List
                        }
                    }
                    else
                    {
                        // Process with normal list
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
