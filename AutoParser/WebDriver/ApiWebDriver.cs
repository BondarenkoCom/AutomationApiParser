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

                        response.EnsureSuccessStatusCode();
                        var responseContentDocTu = await response.Content.ReadAsStringAsync();
                        Console.WriteLine($"Doctu case - {response}");
                        var responseSorterRankingDoctu = new ResponseSorter();
                        var sorterResultRankingDocTu = responseSorterRankingDoctu.HtmlConverter(responseContentDocTu,
                            JsonReader.GetValues().RankingStarsItemPropNameDoctu);

                        int j = 0;
                        foreach (var ranking in sorterResultRankingDocTu)
                        {
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
                    default:
                        string errorMes = $"Check JSON settings for url - {url}";
                        Console.WriteLine(errorMes);
                        ImportInformationToGoogleDocs.PushToGoogleSheets(
                            errorMes,
                            null,
                            null,
                            null,
                            null,
                            ratingRange);
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