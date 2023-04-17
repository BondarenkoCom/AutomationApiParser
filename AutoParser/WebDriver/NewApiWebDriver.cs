using AutoParser.Helpers;
using AutoParser.Interfaces;
using System.Net;

namespace AutoParser.WebDriver
{
    internal class NewApiWebDriver : IWebDriver
    {
        private readonly IResponseSorter _responseSorterMethodProvider;

        public NewApiWebDriver(IResponseSorter responseSorterMethodProvider)
        {
            _responseSorterMethodProvider = responseSorterMethodProvider;
        }

       
        public async Task<string> RunDriverClient(string url, string ratingRange)
        {
            var uri = new Uri(url);
            var host = uri.Host;

            if(host.Contains("www"))
            {
                host = host.Replace("www.", "");
            }

            var client = new HttpClient();

            // Set User-Agent header to mimic a browser
            client.DefaultRequestHeaders.TryAddWithoutValidation("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/89.0.4389.82 Safari/537.36");

            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(url),
            };

            using (var response = await client.SendAsync(request))
            {
                var responseSorterMethods = _responseSorterMethodProvider.GetResponseSorterMethods();

                if (responseSorterMethods.TryGetValue(host, out var method))
                {
                    response.EnsureSuccessStatusCode();
                    var responseContent = await response.Content.ReadAsStringAsync();

                    var sorterResult = method(responseContent, ratingRange);

                    int j = 0;
                    foreach (var ranking in sorterResult)
                    {
                        ImportInformationToGoogleDocs.PushToGoogleSheets(
                            sorterResult,
                            null,
                            null,
                            null,
                            null,
                            ratingRange);
                        j++;
                    }
                }
                else
                {
                    string errorMes = $"Check JSON";
                    //Console.WriteLine(errorMes);
                    ImportInformationToGoogleDocs.PushToGoogleSheets(
                        errorMes,
                        null,
                        null,
                        null,
                        null,
                        ratingRange);
                }
                return null;
            }
        }
        //TODO make time sleep for requests

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

            //Console.WriteLine(response);
            httpListener.Stop();
        }
    }
}