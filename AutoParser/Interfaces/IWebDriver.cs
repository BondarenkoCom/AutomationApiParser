
namespace AutoParser.Interfaces
{
    public interface IWebDriver
    {
        Task<string> RunDriverClient(string url);
        void StatusTestCode();
        void ListenGoogleSheets();
    }
}
