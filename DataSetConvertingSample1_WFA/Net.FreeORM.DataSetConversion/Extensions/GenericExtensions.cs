using Net.FreeORM.DataSetConversion.Interfaces;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Net.FreeORM.DataSetConversion.Extensions
{
    public static class GenericExtensions
    {
        public static T Copy<T>(this T _t) where T : IDsObject
        {
            T rT = Activator.CreateInstance<T>();

            if (_t != null)
            {
                PropertyInfo[] propsArr = typeof(T).GetProperties();
                object val = null;
                foreach (PropertyInfo prpInf in propsArr)
                {
                    val = prpInf.GetValue(_t);
                    prpInf.SetValue(rT, val);
                }
            }

            return rT;
        }

        public static T AsType<T>(this object obj, bool allowNullable = false) where T : class
        {
            T rT = obj as T;

            if (!allowNullable)
            {
                if (rT == null)
                {
                    rT = Activator.CreateInstance<T>();
                }
            }

            return rT;
        }

        public static Dictionary<string, object> GetDifferences<T>(this T tSource, T tDestination) where T : IDsObject
        {
            Dictionary<string, object> diff = new Dictionary<string, object>();

            PropertyInfo[] propsArr = typeof(T).GetProperties();

            object valSrc, valDest;

            foreach (PropertyInfo prpInf in propsArr)
            {
                valSrc = prpInf.GetValue(tSource);
                valDest = prpInf.GetValue(tDestination);
                if (!object.Equals(valSrc, valDest))
                {
                    diff.Add(prpInf.Name, valDest);
                }
            }

            return diff;
        }
    }
}