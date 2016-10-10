using System;

namespace Net.FreeORM.DataSetConversion.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public sealed class DsTableAttribute : Attribute
    {
        public string TableName { get; set; }
    }
}