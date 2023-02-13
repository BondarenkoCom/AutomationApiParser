namespace AutoParser.Interfaces
{
    public interface IGoogleSheetsDriver
    {
        string PushToGoogleSheets(string host, string ranking, string reviewBody, string dataTime, string author);

        string GetDataFromGoogleSheets();
    }
}
