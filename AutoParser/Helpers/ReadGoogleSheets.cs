using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using AutoParser.WebDriver;
using AutoParser.Helpers.HelpersGetValueSheets;

namespace AutoParser.Helpers
{
    public class ReadGoogleSheets
    {
        private static SheetsService sheetsService;
        private readonly NewApiWebDriver _apiWebDriver = new NewApiWebDriver();
        public HelpersSheet helpersSheet = new HelpersSheet();

        public async Task<string> GetDataFromGoogleSheets()
        {
            var dataSheets = await helpersSheet.GetHeaderValues();
            Console.WriteLine($"this data from Sheets - {dataSheets}");

            return null;
        }
    }
}