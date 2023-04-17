using AutoParser.WebDriver;
using AutoParser.Helpers.HelpersGetValueSheets;
using AutoParser.Factory;

namespace AutoParser.Helpers
{
    public class ReadGoogleSheets
    {
        public HelpersSheet helpersSheet = new HelpersSheet();
        private readonly ResponseSorterFactory _responseSorterFactory = new ResponseSorterFactory();
        private readonly NewApiWebDriver _apiWebDriver;

        public ReadGoogleSheets()
        {
            _apiWebDriver = new NewApiWebDriver(_responseSorterFactory);
        }

        public async Task<string> GetDataFromGoogleSheetsWithRetry()
        {
            // Retry configuration
            int maxRetries = 40;
            int retryCount = 0;
            int waitTimeInSeconds = 30;

            while (retryCount < maxRetries)
            {
                try
                {
                    return await GetDataFromGoogleSheets();
                }
                catch (Exception ex)
                {
                    if (ex.Message.Contains("Quota exceeded") && ex.Message.Contains("TooManyRequests"))
                    {
                        retryCount++;
                        Console.WriteLine($"Quota exceeded: Too many requests. Waiting {waitTimeInSeconds} seconds before retrying... ({retryCount}/{maxRetries})");
                        await Task.Delay(TimeSpan.FromSeconds(waitTimeInSeconds));
                    }
                    else
                    {
                        Console.WriteLine($"An unexpected error occurred: {ex.Message}");
                        throw;
                    }
                }
            }

            return null;
        }

        public async Task<string> GetDataFromGoogleSheets()
        {
            var dataSheets = await helpersSheet.GetHeaderValues();
            Console.WriteLine($"this data from Sheets - {dataSheets}");

            return null;
        }
    }
}
