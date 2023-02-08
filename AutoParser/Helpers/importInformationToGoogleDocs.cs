using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using Google.Apis.Auth.OAuth2;

namespace AutoParser.Helpers
{
    public static class ImportInformationToGoogleDocs
    {
        private static SheetsService sheetsService;
        public static string PushToGoogleSheets(string host= null,string ranking= null, string reviewBody = null, string dataTime = null, string author = null)
        {
            if (sheetsService == null)
            {
                string pathToKey = JsonReader.GetValues().PathToKey;
                var credential = GoogleCredential.FromFile(pathToKey);
                sheetsService = new SheetsService(new Google.Apis.Services.BaseClientService.Initializer()
                {
                    HttpClientInitializer = credential,
                    ApplicationName = JsonReader.GetValues().ApplicationName
                });
            }

            var values = new List<IList<object>>
            {
                new List<object> { host, ranking, dataTime, reviewBody, author, Environment.UserName },
            };

            try
            {
                foreach (var value in values)
                {
                    ValueRange requestBody = new ValueRange
                    {
                        Values = new List<IList<object>> { value }
                    };

                    var spreadsheetId = JsonReader.GetValues().SpreadsheetId;
                    var range = JsonReader.GetValues().SheetRange;

                    var request = sheetsService.Spreadsheets.Values.Append(requestBody, spreadsheetId, range);
                    request.InsertDataOption = SpreadsheetsResource.ValuesResource.AppendRequest.InsertDataOptionEnum.INSERTROWS;
                    request.ValueInputOption = SpreadsheetsResource.ValuesResource.AppendRequest.ValueInputOptionEnum.RAW;
                    request.Execute();
                    Console.WriteLine($"send to Google Sheets");
                    System.Threading.Thread.Sleep(TimeSpan.FromSeconds(60));
                   
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error while sending data to Google Sheets: {ex.Message}");
                return ex.Message;
            }   
            return null;
        }
    }
}