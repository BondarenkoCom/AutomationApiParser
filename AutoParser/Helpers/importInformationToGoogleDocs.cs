using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using Google.Apis.Auth.OAuth2;

namespace AutoParser.Helpers
{
    public class ImportInformationToGoogleDocs
    {
        private static SheetsService sheetsService;
        private static int requestCounter = 0;

        public static string PushToGoogleSheets(
            string ranking = null, string host = null, string reviewBody = null, 
            string dataTime = null, string author = null, string RatingRange = null)
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
                new List<object> { ranking, host , dataTime, reviewBody, author },
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
                    var range = RatingRange;

                    var request = sheetsService.Spreadsheets.Values.Update(requestBody, spreadsheetId, range);
                    request.ValueInputOption = SpreadsheetsResource.ValuesResource.UpdateRequest.ValueInputOptionEnum.RAW;
                    request.Execute();

                    requestCounter++;

                    if (requestCounter == 10)
                    {
                        Thread.Sleep(TimeSpan.FromSeconds(10));
                        requestCounter = 0;
                    }
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