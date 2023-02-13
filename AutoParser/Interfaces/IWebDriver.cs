
namespace AutoParser.Interfaces
{
    public interface IWebDriver
    {
        Task<string> RunDriverClient(string url, string RatingRange);
        void StatusTestCode();
        void ListenGoogleSheets();
    }
}
