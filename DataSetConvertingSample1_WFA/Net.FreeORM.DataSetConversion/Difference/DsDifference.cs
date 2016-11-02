using Net.FreeORM.DataSetConversion.Interfaces;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Net.FreeORM.DataSetConversion.Difference
{
    public class DsDifference<T> where T : IDsObject
    {
        public DsDifference(T t1, T t2)
        {
            if (t1 == null || t2 == null)
                throw new Exception("Objects can not ne null.");

            this.TypeOfObject = typeof(T);

            PropertyInfo[] propsArr = typeof(T).GetProperties();

            object valSrc, valDest;
            List<DsDifferenceParam> lst = new List<DsDifferenceParam>();
            foreach (PropertyInfo prpInf in propsArr)
            {
                valSrc = prpInf.GetValue(t1);
                valDest = prpInf.GetValue(t2);
                if (!object.Equals(valSrc, valDest))
                {
                    lst.Add(new DsDifferenceParam(prpInf.Name,
                        Nullable.GetUnderlyingType(prpInf.PropertyType) ?? prpInf.PropertyType, valSrc, valDest));
                }
            }

            this.DifferenceList = lst;
        }

        public Type TypeOfObject { get; private set; }

        public Dictionary<string, object> Differences { get; private set; }

        public List<DsDifferenceParam> DifferenceList { get; private set; }
    }
}