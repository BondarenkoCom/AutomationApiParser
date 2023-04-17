namespace AutoParser.Interfaces
{
    public interface IResponseSorter
    {
        Dictionary<string, Func<string, string, string>> GetResponseSorterMethods();
    }
}
