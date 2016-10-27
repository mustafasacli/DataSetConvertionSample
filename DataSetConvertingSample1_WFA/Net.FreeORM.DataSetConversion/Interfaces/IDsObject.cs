using System;

namespace Net.FreeORM.DataSetConversion.Interfaces
{
    public interface IDsObject : ICloneable
    {
        string GetTableName();
    }
}