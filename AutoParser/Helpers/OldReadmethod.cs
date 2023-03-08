using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoParser.Helpers
{
    internal class OldReadmethod
    {
        public void GetCell()
        {
            //запрос на чтение Даты (брать из таблицы)

            //сделать запрос на все A1 до Y1
            //проверить сущетсвует ли новая дата из string date , если ее нету то мы свободную букву колонки и пишем туда новые данные
            /*
                        switch (date)
                        {
                            case "06.03.2023":
                                RatingRange = $"D{rangeCount}";
                                break;
                            case "07.03.2023":
                                RatingRange = $"E{rangeCount}";
                                break;
                            case "05.03.2023":
                                RatingRange = $"F{rangeCount}";
                                break;
                             //add more cases for other dates as needed
                            default:
                                Console.WriteLine("Invalid date");
                                break;
                        }

                        var request_1_row_urls = sheetsService.Spreadsheets.Values.Get(spreadsheetId, UrlRange);
                        var request_2_row = sheetsService.Spreadsheets.Values.Get(spreadsheetId, RatingRange);

                        var responseUrl = request_1_row_urls.Execute();
                        var responseRow = request_2_row.Execute();

                        switch (responseUrl.Values?[0][0])
                        {
                            case "URLS":
                                if (responseUrl.Values != null)
                                {
                                    Console.WriteLine($"{RatingRange} - date is Exist");
                                }
                                else
                                {
                                    Console.WriteLine("Row data is null");
                                }
                                break;
                            default:
                                //if (responseUrl.Values != null && responseRow.Values == null)
                                //{
                                //    foreach (var item in responseUrl.Values)
                                //    {
                                //        var stringUri = item[0].ToString();
                                //        await _apiWebDriver.RunDriverClient(stringUri, RatingRange);
                                //    }
                                //}
                                //else
                                //{
                                //    Console.WriteLine("Row is empty, or data is null");
                                //}
                                break;
                        }

                        if (count == 20)
                        {
                            Console.WriteLine("Update counter and 60 second hold for API");
                            await Task.Delay(TimeSpan.FromSeconds(60));
                            count = 0;
                        }
                    }
                    catch (Exception ex)
                    {
                        var CatchRatingRange = $"D{rangeCount}";
                        Console.WriteLine($"Error while sending/receiving data to Google Sheets: {ex.Message}, {CatchRatingRange}");
                        ImportInformationToGoogleDocs.PushToGoogleSheets(ex.Message, null, null, null, null, CatchRatingRange);
                        continue;
                    }
                }
                return null;
         }
    }
            */
        }
    }
}