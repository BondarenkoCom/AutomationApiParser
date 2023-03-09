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
            for (int rangeCount = 1; rangeCount <= 100; rangeCount++)
            {
                try
                {
                    //понять почему я пишу данные вообще во все колонки 
                    //мне нужно писать все только в Одну колонку пока не столкнуть с Row is empty
                    //после это заканчивать цикл чтобы получить новую букву для новой колонки
                    Console.WriteLine($"This range letter frim method GetRangeByUrls - {rangeLetter}");
                    var _readGoogle = new InitGoogleSheet();
                    var resultAuth = _readGoogle.InitializeSheetsService();
                    var spreadsheetId = JsonReader.GetValues().SpreadsheetId;
                    var UrlRange = $"C{rangeCount}";
                    var ResultRange = $"{rangeLetter}{rangeCount}";
                    var DateRange = $"{rangeLetter}1";

                    var request_1_row_urls = resultAuth.Spreadsheets.Values.Get(spreadsheetId, UrlRange);
                    var request_2_row = resultAuth.Spreadsheets.Values.Get(spreadsheetId, ResultRange);
                    var request_3_date = resultAuth.Spreadsheets.Values.Get(spreadsheetId, DateRange);

                    var responseUrl = request_1_row_urls.Execute();
                    var responseRow = request_2_row.Execute();
                    var responseDate = request_3_date.Execute();

                    var day = DateTime.Now.Day.ToString();
                    var dayObject = (object)day;

                    //&& responseRow.Values.Contains(dayObject)
                    if (responseUrl.Values != null && responseRow.Values == null)
                    {
                        Console.WriteLine($"data is exist - {responseUrl.Values[0]}");

                        if (responseDate.Values.Contains(dayObject))
                        {
                            foreach (var item in responseUrl.Values)
                            {
                                var stringUri = item[0].ToString();
                                await _apiWebDriver.RunDriverClient(stringUri, ResultRange);
                            }
                        }

                        //foreach (var item in responseUrl.Values)
                        //{
                        //    var stringUri = item[0].ToString();
                        //    await _apiWebDriver.RunDriverClient(stringUri, ResultRange);
                        //}
                    }
                    else
                    {
                        Console.WriteLine("Row is empty or invalid data(Data only today)");
                    }
                    continue;

                    if (rangeCount == 10)
                    {
                        Console.WriteLine("Update counter and 60 second hold for API");
                        await Task.Delay(TimeSpan.FromSeconds(3));
                        rangeCount = 0;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    continue;
                    //break;
                }
            }
            return null;
        }
    }
}
