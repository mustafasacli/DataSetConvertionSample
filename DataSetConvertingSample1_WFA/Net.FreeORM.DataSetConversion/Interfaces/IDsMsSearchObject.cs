namespace Net.FreeORM.DataSetConversion.Interfaces
{
    public interface IDsMsSearchObject
    {
        string[] GetParameterNames();

        object[] GetParameterValues();
    }
}