namespace Net.FreeORM.DataSetConversion.Interfaces
{
    public interface IDsSearchObject
    {
        /// <summary>
        /// Gets Parameter List of Search.
        /// </summary>
        /// <returns>Returns object array.</returns>
        object[] GetSearchParameters();
    }
}