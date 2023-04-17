namespace AutoParser.Interfaces
{
    public interface IResponseSorterFactory
    {
        //Base interface for future factory
        IResponseSorter CreateResponseSorter(string projectName);
    }
}
