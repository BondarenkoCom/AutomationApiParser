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

        public static string PushToGoogleSheets()
        {

            string pathToKey = JsonReader.GetValues().PathToKey;
            
            var credential = GoogleCredential.FromFile(pathToKey);

            var sheetsService = new SheetsService(new Google.Apis.Services.BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = JsonReader.GetValues().ApplicationName
            });

            //TODO Reader from datas reviews
            var values= new List<IList<object>>
            {
                new List<object> {"Samus", "TEXT TEXT TEXT TEXT TEXT TEXT", "5 stars"},
                new List<object> {"Ada", "TEXT TEXT TEXT TEXT TEXT TEXT", "5 stars"},
                new List<object> {"Ashley", "TEXT TEXT TEXT TEXT TEXT TEXT", "5 stars"},
                new List<object> {"Motoko",  "TEXT TEXT TEXT TEXT TEXT TEXT", "5 stars"},
            };

            var spreadsheetId = JsonReader.GetValues().SpreadsheetId;
            var range = JsonReader.GetValues().SheetRange;

            ValueInputOptionEnum valueInputOption = ValueInputOptionEnum.USERENTERED;

            var updateRequest = sheetsService.Spreadsheets.Values.Update(new ValueRange() { Values = values }, spreadsheetId, range);
            updateRequest.ValueInputOption = valueInputOption;

            var updateResponse = updateRequest.Execute();
            return updateResponse.ToString();
        }
    }
}
