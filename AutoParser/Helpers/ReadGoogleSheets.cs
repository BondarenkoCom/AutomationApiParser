using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using Google.Apis.Auth.OAuth2;
using AutoParser.WebDriver;


namespace AutoParser.Helpers
{
    public class ReadGoogleSheets
    {
        private static SheetsService sheetsService;
        private readonly ApiWebDriver _apiWebDriver = new ApiWebDriver();

        public async Task<string> GetDataFromGoogleSheets()
        {
            sheetsService = sheetsService ?? InitializeSheetsService();

            for (int count = 0, rangeCount = 2; count <= 100; count++, rangeCount++)
            {
                try
                {
                    var spreadsheetId = JsonReader.GetValues().SpreadsheetId;
                    var UrlRange = $"C{rangeCount}";
                    var RatingRange = $"D{rangeCount}";
                    var request = sheetsService.Spreadsheets.Values.Get(spreadsheetId, UrlRange);
                    var response = request.Execute();

                    if (response.Values != null)
                    {
                        foreach (var item in response.Values)
                        {
                            var stringUri = item[0].ToString();
                            await _apiWebDriver.RunDriverClient(stringUri, RatingRange);
                        }
                    }
                    else
                    {
                        Console.WriteLine("Row is empty");
                    }

                    if (count == 30)
                    {
                        await Task.Delay(TimeSpan.FromSeconds(60));
                        count = 0;
                        Console.WriteLine("Update counter and 60 second hold for API");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error while sending/receiving data to Google Sheets: {ex.Message}");
                    continue;
                }
            }

            return null;
        }

        private SheetsService InitializeSheetsService()
        {
            string pathToKey = JsonReader.GetValues().PathToKey;
            var credential = GoogleCredential.FromFile(pathToKey);
            return new SheetsService(new Google.Apis.Services.BaseClientService.Initializer
            {
                HttpClientInitializer = credential,
                ApplicationName = JsonReader.GetValues().ApplicationName
            });
        }
    }
}