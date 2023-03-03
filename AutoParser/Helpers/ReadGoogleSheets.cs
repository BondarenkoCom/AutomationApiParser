using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using Google.Apis.Auth.OAuth2;
using AutoParser.WebDriver;
using System.Reflection;
using System.Globalization;

namespace AutoParser.Helpers
{
    public class ReadGoogleSheets
    {
        private static SheetsService sheetsService;
        private readonly NewApiWebDriver _apiWebDriver = new NewApiWebDriver();

        public async Task<string> GetDataFromGoogleSheets()
        {
            sheetsService = sheetsService ?? InitializeSheetsService();

            for (int count = 0, rangeCount = 1; count <= 100; count++, rangeCount++)
            {
                try
                {
                    var spreadsheetId = JsonReader.GetValues().SpreadsheetId;

                    var UrlRange = $"C{rangeCount}";

                    var RatingRange = $"D{rangeCount}";
                    var NextRange = $"E{rangeCount}";
                    //var NextRange = $"{(char)('E')}";

                    var request_1_row_urls = sheetsService.Spreadsheets.Values.Get(spreadsheetId, UrlRange);
                    var request_2_row = sheetsService.Spreadsheets.Values.Get(spreadsheetId, RatingRange);

                    var responseUrl = request_1_row_urls.Execute();
                    var responseRow = request_2_row.Execute();

                    string date = DateTime.Today.ToString("dd.MM.yyyy");


                    if (responseUrl.Values != null && responseRow.Values == null)
                    {
                        foreach (var item in responseUrl.Values)
                        {
                            if (item.Contains("URLS"))
                            {
                                ImportInformationToGoogleDocs.PushToGoogleSheets(
                                date,
                                null,
                                null,
                                null,
                                null,
                                RatingRange);
                            }
                            else
                            {
                               var stringUri = item[0].ToString();
                               await _apiWebDriver.RunDriverClient(stringUri, RatingRange);
                            }
                        }
                    }
                    else if (responseUrl.Values != null && responseRow.Values != null)
                    {
                        
                        foreach (var item in responseUrl.Values)
                        {
                            if (NextRange != null)
                            {
                                NextRange = $"F{rangeCount}";
                            }
                            var stringUri = item[0].ToString();
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
            string KeyName = JsonReader.GetValues().PathToKey;
            string basePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string pathToKey = Path.Combine(basePath, "keys", $"{KeyName}");

            var credential = GoogleCredential.FromFile(pathToKey);
            return new SheetsService(new Google.Apis.Services.BaseClientService.Initializer
            {
                HttpClientInitializer = credential,
                ApplicationName = JsonReader.GetValues().ApplicationName
            });
        }
    }
}