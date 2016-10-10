using Net.FreeORM.DataSetConversion.Extensions;
using Net.FreeORM.DataSetConversion.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Reflection;

namespace Net.FreeORM.DataSetConversion.Conversion
{
    public static class DsObjectConverter
    {
        #region [ DataTable To DsObject Converting ]

        public static IList<T> ToList<T>(this DataTable table) where T : IDsObject
        {
            IList<T> result = new List<T>();

            foreach (DataRow row in table.Rows)
            {
                var item = CreateItemFromRow<T>(row);
                result.Add(item);
            }

            return result;
        }

        public static T CreateItemFromRow<T>(DataRow row) where T : IDsObject
        {
            T item = Activator.CreateInstance<T>();
            IList<PropertyInfo> properties = typeof(T).GetProperties();
            object val;
            foreach (var property in properties)
            {
                val = null;
                if (row[property.Name] != DBNull.Value)
                    val = row[property.Name];

                property.SetValue(item, val, null);
            }
            return item;
        }

        #endregion [ DataTable To DsObject Converting ]

        #region [ DsObject To DataTable/DataSet Converting ]

        public static DataSet GenerateDataSetFromDsObject<T>(this List<T> listOfT) where T : IDsObject
        {
            DataSet ds = new DataSet();

            DataTable dt = GenerateDataTableFromDsObject(listOfT);
            ds.Tables.Add(dt);

            return ds;
        }

        public static DataTable GenerateDataTableFromDsObject<T>(this List<T> listOfT) where T : IDsObject
        {
            DataTable dt = new DataTable();

            dt = ListExtensions.ToDataTable(listOfT);

            T pT = Activator.CreateInstance<T>();
            dt.TableName = pT.GetTableName();
            // Generate DataTable
            /*
            Type typT = typeof(T);
            PropertyInfo[] propInfoList = typT.GetProperties();
            foreach (PropertyInfo prp in propInfoList)
            {
                if (Nullable.GetUnderlyingType(prp.PropertyType) == null)
                    dt.Columns.Add(prp.Name, prp.PropertyType);
            }

            DataRow dr;
            foreach (var item in listOfT)
            {
                dr = null;
                dr = dt.NewRow();
                item.GenerateRow(ref dr);
                dt.Rows.Add(dr);
            }
            */
            return dt;
        }

        public static void GenerateRow<T>(this T t, ref DataRow row) where T : IDsObject
        {
            PropertyInfo[] propInfoList = typeof(T).GetProperties();

            //PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(T));
            object val = null;
            foreach (PropertyInfo prp in propInfoList)
            {
                val = prp.GetValue(t, null);
                if (val == null)
                    row[prp.Name] = DBNull.Value;
                else
                    row[prp.Name] = val;
            }
        }

        #endregion [ DsObject To DataTable/DataSet Converting ]
    }
}