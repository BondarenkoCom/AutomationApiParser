using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using Google.Apis.Auth.OAuth2;
using AutoParser.WebDriver;
using System.Reflection;

namespace AutoParser.Helpers
{
    public class ReadGoogleSheets
    {
        private static SheetsService sheetsService;
        private readonly NewApiWebDriver _apiWebDriver = new NewApiWebDriver();

        public async Task<string> GetDataFromGoogleSheets()
        {
            sheetsService = sheetsService ?? InitializeSheetsService();

            //TODO make count <= 100 check all columns
            for (int count = 0, rangeCount = 1; count <= 100; count++, rangeCount++)
            {
                try
                {
                    var spreadsheetId = JsonReader.GetValues().SpreadsheetId;

                    //TODO make read from JSON UrlRange,RatingRange

                    //получаем урл если он не пустой пишем в следующий столбец И  следующий столбец должен содержать только одну дату
                    //в D1  пишем дату

                    DateTime today = DateTime.Today;
                    string dateString = today.ToString("dd.MM.yyyy");

                    var UrlRange = $"C{rangeCount}";

                    //TODO make loop next char [E,F,G,H,I,J,K,l,M,N,O,P] 
                    var RatingRange = $"D{rangeCount}";
                    var NextRange = $"E{rangeCount}";
                    var RangeAfterUrl = rangeCount+2;
                    char[] charGoogleSheets = {'A', 'B', 'C', 'D', 'F', 'G', 'H', 'I','J', 
                                                'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R',
                                                 'S','T','U','V','W','X','Y'};

                    int headCharGoogleSheetsCounter = 1;
                    var RangeData = $"{charGoogleSheets[RangeAfterUrl]}{headCharGoogleSheetsCounter}";

                    var request_1_row_urls = sheetsService.Spreadsheets.Values.Get(spreadsheetId, UrlRange);
                    var request_2_row = sheetsService.Spreadsheets.Values.Get(spreadsheetId, RatingRange);

                    var responseUrl = request_1_row_urls.Execute();
                    var responseRow = request_2_row.Execute();


                    if (responseUrl.Values != null && responseRow.Values == null)
                    {
                        foreach (var item in responseUrl.Values)
                        {
                            var stringUri = item[0].ToString();
                            if (stringUri.Contains("URLS"))
                            {
                                Console.WriteLine($"I am Url row - {stringUri}");
                                
                                ImportInformationToGoogleDocs.PushToGoogleSheets(
                                    dateString,
                                    null,
                                    null,
                                    null,
                                    null,
                                    RangeData);
                            }
                            else
                            {
                                await _apiWebDriver.RunDriverClient(stringUri, RatingRange);
                            }

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