using AutoParser.Helpers;
using AutoParser.Interfaces;
using System.Net;

namespace AutoParser.WebDriver
{
    internal class ApiWebDriver : IWebDriver
    {
        public async Task<string> RunDriverClient(string url, string ratingRange)
        {
            var uri = new Uri(url);
            var host = uri.Host;
            Console.WriteLine($"{host} - this site name");
            Console.WriteLine($"ratingRange from ApiWebDriver class - {ratingRange}");

            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(url),
            };

            using (var response = await client.SendAsync(request))
            {
                switch (host)
                {
                    case "doctu.ru":
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($"ratingRange from case doctu switch - {ratingRange}");
                        Console.ResetColor();

                        response.EnsureSuccessStatusCode();
                        var responseContentDocTu = await response.Content.ReadAsStringAsync();
                        Console.WriteLine($"Doctu case - {response}");
                        var responseSorterRankingDoctu = new ResponseSorter();
                        var sorterResultRankingDocTu = responseSorterRankingDoctu.HtmlConverter(responseContentDocTu,
                            JsonReader.GetValues().RankingStarsItemPropNameDoctu);

                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($"this sorterResultRankingDocTu 1- {sorterResultRankingDocTu}");
                        Console.ResetColor();

                        int j = 0;
                        foreach (var ranking in sorterResultRankingDocTu)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine($"ratingRange from foreach Sender - {ratingRange}");
                            Console.ResetColor();

                            Console.WriteLine($"{j} Run Sender To Google Sheets");
                            ImportInformationToGoogleDocs.PushToGoogleSheets(
                                sorterResultRankingDocTu,
                                null,
                                null,
                                null,
                                null,
                                ratingRange);
                            j++;
                        }
                        break;
                }
                return null;
            }
        }


        public void StatusTestCode()
        {
            //TODO Check code status and return string status code
            throw new NotImplementedException();
        }

        public void ListenGoogleSheets()
        {
            HttpListener httpListener = new HttpListener();
            httpListener.Prefixes.Add("http://localhost:8080/");
            httpListener.Start();

            Console.WriteLine("Listening...");

            HttpListenerContext context = httpListener.GetContext();
            HttpListenerResponse response = context.Response;

            Console.WriteLine(response);
            httpListener.Stop();
        }
    }
}