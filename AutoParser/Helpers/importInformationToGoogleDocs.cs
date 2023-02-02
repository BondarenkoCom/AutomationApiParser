using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using Google.Apis.Auth.OAuth2;
using static Google.Apis.Sheets.v4.SpreadsheetsResource.ValuesResource.UpdateRequest;

namespace AutoParser.Helpers
{
    public static class ImportInformationToGoogleDocs
    {

        public static void SheetsInit()
        {

        }

        public static string PushToGoogleSheets(string benefits, string dataTime, string author)
        {
            string pathToKey = JsonReader.GetValues().PathToKey;
            //string pathToKey = Path.Combine(Environment.CurrentDirectory, "farmaceptical-reviews.json"); ;

            var credential = GoogleCredential.FromFile(pathToKey);
            var pcName = Environment.UserName;
            var sheetsService = new SheetsService(new Google.Apis.Services.BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = JsonReader.GetValues().ApplicationName
            });

            //TODO Reader from datas reviews
            var values= new List<IList<object>>
            {
                new List<object> {  dataTime, benefits, author, pcName},
            };

            foreach (var value in values)
            {
              ValueRange requestBody = new ValueRange
              {
                  Values = new List<IList<object>> { value}
              };
                
              var spreadsheetId = JsonReader.GetValues().SpreadsheetId;
              var range = JsonReader.GetValues().SheetRange;

              var request = sheetsService.Spreadsheets.Values.Append(requestBody, spreadsheetId, range);
              request.InsertDataOption = SpreadsheetsResource.ValuesResource.AppendRequest.InsertDataOptionEnum.INSERTROWS;
              request.ValueInputOption = SpreadsheetsResource.ValuesResource.AppendRequest.ValueInputOptionEnum.RAW;
              request.Execute();
            }
            //TODO make try catch error catcher
            return null;
        }
    }
}
