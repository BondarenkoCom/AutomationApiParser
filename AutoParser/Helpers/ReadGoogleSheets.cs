using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using Google.Apis.Auth.OAuth2;
using AutoParser.WebDriver;
using System.Reflection;
using System.Globalization;
using System.Text.RegularExpressions;
using AutoParser.Helpers.HelpersGetValueSheets;

//        var stringUri = item[0].ToString();
//        await _apiWebDriver.RunDriverClient(stringUri, RatingRange);
//запрос на чтение Даты (брать из таблицы)

//сделать запрос на все A1 до Y1
//проверить сущетсвует ли новая дата из string date , если ее нету то мы свободную букву колонки и пишем туда новые данные


namespace AutoParser.Helpers
{
    public class ReadGoogleSheets
    {
        private static SheetsService sheetsService;
        private readonly NewApiWebDriver _apiWebDriver = new NewApiWebDriver();
        public HelpersSheet helpersSheet = new HelpersSheet();

        public async Task<string> GetDataFromGoogleSheets()
        {
            // sheetsService = sheetsService ?? InitializeSheetsService();

            //string date = DateTime.Today.ToString("dd.MM.yyyy");
            //GetNextRatingColumn use this method for change char

            var dataSheets = await helpersSheet.GetHeaderValues();
            Console.WriteLine($"this data from Sheets - {dataSheets}");

            return null;
        }
    }
}