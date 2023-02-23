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

            //TODO make count <= 100 check all columns
            for (int count = 0, rangeCount = 2; count <= 100; count++, rangeCount++)
            {
                try
                {
                    var spreadsheetId = JsonReader.GetValues().SpreadsheetId;

                    //TODO make read from JSON UrlRange,RatingRange
                    var UrlRange = $"C{rangeCount}";
                    var RatingRange = $"D{rangeCount}";
                    //TODO make loop next char [E,F,G,H,I,J,K,l,M,N,O,P] 
                    var NextRange = $"E{rangeCount}";

                    var request_1_row_urls = sheetsService.Spreadsheets.Values.Get(spreadsheetId, UrlRange);
                    var request_2_row = sheetsService.Spreadsheets.Values.Get(spreadsheetId, RatingRange);

                    var responseUrl = request_1_row_urls.Execute();
                    var responseRow = request_2_row.Execute();

                   if (responseUrl.Values != null && responseRow.Values == null)
                    {
                        foreach (var item in responseUrl.Values)
                        {
                            var stringUri = item[0].ToString();
                            await _apiWebDriver.RunDriverClient(stringUri, RatingRange);
                        }
                    }
                    else if (responseUrl.Values != null && responseRow.Values != null)
                    {
                        foreach (var item in responseUrl.Values)
                        {
                            var stringUri = item[0].ToString();
                            
                            Console.WriteLine($"is not null - {stringUri}");
                            await _apiWebDriver.RunDriverClient(stringUri, NextRange);
                        }
                    }
                    else
                    {
                        Console.WriteLine("Row is empty");
                    }

                    if (count == 20)
                    {
                        Console.WriteLine("Update counter and 60 second hold for API");
                        await Task.Delay(TimeSpan.FromSeconds(60));
                        count = 0;
                    }
                }
                catch (Exception ex)
                {
                    var CatchRatingRange = $"D{rangeCount}";
                    Console.WriteLine($"Error while sending/receiving data to Google Sheets: {ex.Message}, {CatchRatingRange}");
                    ImportInformationToGoogleDocs.PushToGoogleSheets(
                           ex.Message,
                           null,
                           null,
                           null,
                           null,
                           CatchRatingRange);
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