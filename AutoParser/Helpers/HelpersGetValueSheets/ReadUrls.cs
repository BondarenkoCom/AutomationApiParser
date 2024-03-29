﻿using AutoParser.WebDriver;
using System.Globalization;

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
            var today = DateTime.Today;

            for (int rangeCount = 1, countTimer = 1; rangeCount <= 400; rangeCount++, countTimer++)
            {
                try
                {
                    var UrlRange = $"C{rangeCount}";
                    var ResultRange = $"{rangeLetter}{rangeCount}";
                    var DateRange = $"{rangeLetter}1";

                    var request_1_row_urls = resultAuth.Spreadsheets.Values.Get(spreadsheetId, UrlRange);
                    var request_2_row = resultAuth.Spreadsheets.Values.Get(spreadsheetId, ResultRange);
                    var request_3_date = resultAuth.Spreadsheets.Values.Get(spreadsheetId, DateRange);

                    var responseUrl = request_1_row_urls.Execute();
                    var responseRow = request_2_row.Execute();
                    var responseDate = request_3_date.Execute();

                    foreach (var item in responseDate.Values)
                    {
                        var isDate = DateTime.TryParseExact(item[0].ToString(), "dd.MM.yyyy",
                        CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime result);

                        if (isDate && result == today)
                        {
                            if (responseUrl.Values != null && responseRow.Values == null)
                            {
                                foreach (var url in responseUrl.Values)
                                {
                                    Console.WriteLine($"Result range - {ResultRange}");

                                    var stringUri = url[0].ToString();
                                    await _apiWebDriver.RunDriverClient(stringUri, ResultRange);
                                }
                            }
                        }
                        else
                        {
                            string error = "Row is empty or invalid data(Data only today)";
                            Console.WriteLine("Error, not today date");
                            return error;
                        }
                    }

                    if (countTimer == 10)
                    {
                        Console.WriteLine("Update counter and 30 second hold for API from ReadUrls");
                        await Task.Delay(TimeSpan.FromSeconds(30));
                        countTimer = 0;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    continue;
                }
            }
            return null;
        }
    }
}
