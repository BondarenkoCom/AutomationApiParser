using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static AutoParser.Helpers.ReadGoogleSheets;

namespace AutoParser.Helpers.HelpersGetValueSheets
{
    public class HelpersSheet
    {
        public async Task<string> GetHeaderValues()
        {
            var _readGoogle = new InitGoogleSheet();
            var resultAuth = _readGoogle.InitializeSheetsService();

            try
            {

                var spreadsheetId = JsonReader.GetValues().SpreadsheetId;
                var rangeCount = 0;
                var dateRange = "A1:Y1";
                
                var request = resultAuth.Spreadsheets.Values.Get(spreadsheetId, dateRange);
                var response = await request.ExecuteAsync();
                var values = response.Values;

                if (values != null && values.Count > 0)
                {
                    var columnsFirst = values.First().Count;

                    var columnTasks = new List<Task<string>>();
                    for (int i = 0; i < columnsFirst; i++)
                    {
                        columnTasks.Add(ProcessColumn(values, i));
                    }

                    await Task.WhenAll(columnTasks);

                    return columnTasks.FirstOrDefault(t => t.Result != null)?.Result;
                }

                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error while getting header values: {ex.Message}");
                return null;
            }
        }

        private async Task<string> ProcessColumn(IList<IList<object>> values, int column)
        {
          
            for (int count = 0 , rangeCount = 0; count <= 1; count++)
            {
               
                try
                {

                    var cellValue = values[0][column].ToString();
                    var cellValueIndex = values[0].IndexOf(cellValue) + 1; // add 1 to get the actual index
                    string rangeLetter = null;
                    rangeLetter = GetRange.GetRangeLetter(cellValueIndex);
                    var RatingRange = $"{rangeLetter}1";
                    Console.WriteLine($"This index range - {cellValueIndex}");

                    var isDate = DateTime.TryParseExact(cellValue, "dd.MM.yyyy", 
                        CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime result);

                    ReadUrls beginRequest = new ReadUrls();

                    if (isDate)
                    {
                        Console.WriteLine($"cell value - {cellValue}," +
                            $"Is data - {result}," +
                            $"This rating range - {RatingRange}," +
                            $"Is range value - {rangeLetter},");

                        var resultsByUrls = await beginRequest.GetRangeByUrls(rangeLetter);
                        Console.WriteLine($"ranges from Google Sheets - {resultsByUrls}");  
                    }
                    else
                    {
                        Console.WriteLine($"Is not data - {cellValue}");
                        break;
                    }

                    if (count == 10)
                    {
                        Console.WriteLine("Update counter and 60 second hold for API");
                        await Task.Delay(TimeSpan.FromSeconds(10));
                        count = 0;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error while processing column {column}: {ex.Message}");
                    continue;
                }
            }

            return null;
        }
    }
}
