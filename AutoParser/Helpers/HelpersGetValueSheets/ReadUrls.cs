using AutoParser.WebDriver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoParser.Helpers.HelpersGetValueSheets
{
    public class ReadUrls
    {
        private readonly NewApiWebDriver _apiWebDriver = new NewApiWebDriver();

        public async Task<string> GetRangeByUrls(string rangeLetter)
        {

            var _readGoogle = new InitGoogleSheet();
            var resultAuth = _readGoogle.InitializeSheetsService();
            var spreadsheetId = JsonReader.GetValues().SpreadsheetId;
            //var UrlRange = $"C{cellValue}";
            var UrlRange = $"C2";

            var request_1_row_urls = resultAuth.Spreadsheets.Values.Get(spreadsheetId, UrlRange);
            //var request_2_row = resultAuth.Spreadsheets.Values.Get(spreadsheetId, rangeLetter);

            var responseUrl = request_1_row_urls.Execute();
            // var responseRow = request_2_row.Execute();

            if (responseUrl.Values != null)
            {
                foreach (var item in responseUrl.Values)
                {
                    var stringUri = item[0].ToString();
                    await _apiWebDriver.RunDriverClient(stringUri, rangeLetter);
                }
            }

            return null;
        }
    }
}
