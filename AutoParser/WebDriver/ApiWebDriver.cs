using AutoParser.Helpers;
using AutoParser.Interfaces;
using System.Net;

namespace AutoParser.WebDriver
{
    internal class ApiWebDriver : IWebDriver
    {
        public async Task<string> RunDriverClient(string url)
        {
            var uri = new Uri(url);
            var host = uri.Host;
            Console.WriteLine($"{host} - this site name");

            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(url),
            };

            using (var response = await client.SendAsync(request))
            {
                //Make Switch for different sites

                switch (host)
                {
                     case "uteka.ru":
                     response.EnsureSuccessStatusCode();
                     var responseContent = await response.Content.ReadAsStringAsync();
                     
                     ResponseSorter responseSorterClassBenefits = new ResponseSorter();
                     var sorterResultBenefits = responseSorterClassBenefits.HtmlConverter(responseContent,
                         JsonReader.GetValues().ReviewBodyClassname).ToArray();
                     
                     ResponseSorter responseSorterClassDateTime = new ResponseSorter();
                     var sorterResultDate = responseSorterClassDateTime.HtmlConverter(responseContent,
                         JsonReader.GetValues().DataTimeClassname).ToArray();
                     
                     ResponseSorter responseSorterAuthorsClass = new ResponseSorter();
                     var sorterResultAuthor = responseSorterAuthorsClass.HtmlConverter(responseContent,
                         JsonReader.GetValues().AuthorsClassname).ToArray();
                     
                     ResponseSorter responseSorterRankingProp = new ResponseSorter();
                     var sorterResultRanking = responseSorterRankingProp.HtmlConverter(responseContent,
                         JsonReader.GetValues().RankingStarsItemPropName).ToArray();
                     
                     for (int i = 0; i < 900; i++)
                     {
                         Console.WriteLine($"{i} Run Sender To google sheets");
                     
                         ImportInformationToGoogleDocs.PushToGoogleSheets(
                           sorterResultRanking[i],
                           sorterResultBenefits[i],
                           sorterResultDate[i],
                           sorterResultAuthor[i]);
                     }
                        break;
                    case "doctu.ru":
                        response.EnsureSuccessStatusCode();
                        var responseContentDocTu = await response.Content.ReadAsStringAsync();

                        ResponseSorter responseSorterRankingDoctu = new ResponseSorter();
                        var sorterResultRankingDocTu = responseSorterRankingDoctu.HtmlConverter(responseContentDocTu,
                            JsonReader.GetValues().RankingStarsItemPropName).ToArray();

                        for (int i = 0; i < 900; i++)
                        {
                            Console.WriteLine($"{i} Run Sender To google sheets");
                            ImportInformationToGoogleDocs.PushToGoogleSheets( sorterResultRankingDocTu[i]);
                        }
                        break;
                }
                return null;
            }
        }

        public void StatusTestCode()
        {
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

        public string CheckWebSiteInformation(string url)
        {
            //TODO 1 make return title
            //TODO 2 make checker "What the site?" for choose json file with data for correct parsing

            var uri = new Uri(url);
            var host = uri.Host;
            return host;
        }
    }
}