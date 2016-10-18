using Net.FreeORM.DataSetConversion.Attributes;
using Net.FreeORM.DataSetConversion.Extensions;
using Net.FreeORM.DataSetConversion.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
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

        #region [ DsObject To DataTable-DataSet Converting ]

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
            string tableName = string.Empty;

            #region [ Get Table Name ]

            try
            {
                T pT = Activator.CreateInstance<T>();
                tableName = pT.GetTableName();
            }
            catch { }

            if (string.IsNullOrWhiteSpace(tableName))
            {
                try
                {
                    var attr = (DsTableAttribute)typeof(T).GetCustomAttributes(typeof(DsTableAttribute), false).First();
                    tableName = attr.TableName ?? string.Empty;
                }
                catch { }
            }

            if (string.IsNullOrWhiteSpace(tableName))
                tableName = typeof(T).Name;

            #endregion

            dt.TableName = tableName;

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

        #endregion [ DsObject To DataTable-DataSet Converting ]
    }
}