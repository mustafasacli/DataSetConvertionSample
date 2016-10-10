using Net.FreeORM.DataSetConversion.Interfaces;
using System;

namespace DataSetConvertingSample1_WFA
{
    public class Person : IDsObject
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string SurName { get; set; }

        public int? DepartmentId { get; set; }

        public int Age { get; set; }

        public DateTime BirthDate { get; set; }

        public string GetTableName()
        {
            return "Tbl_Person";
        }
    }
}