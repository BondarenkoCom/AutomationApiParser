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

                        int i = 0;
                        foreach (var ranking in sorterResultRanking)
                        {
                            Console.WriteLine($"{i} Run Sender To google sheets");
                            ImportInformationToGoogleDocs.PushToGoogleSheets(
                                host,
                                ranking,
                                sorterResultBenefits[i],
                                sorterResultDate[i],
                                sorterResultAuthor[i]);
                            i++;
                        }
                        break;
                    //TODO Make read from Json
                    case "doctu.ru":
                        response.EnsureSuccessStatusCode();
                        var responseContentDocTu = await response.Content.ReadAsStringAsync();

                        ResponseSorter responseSorterRankingDoctu = new ResponseSorter();
                        var sorterResultRankingDocTu = responseSorterRankingDoctu.HtmlConverter(responseContentDocTu,
                            JsonReader.GetValues().RankingStarsItemPropNameDoctu).ToArray();

                        ResponseSorter responseSorterdoctuNamesClass = new ResponseSorter();
                        var sorterResultdoctuNamesClassDocTu = responseSorterdoctuNamesClass.HtmlConverter(responseContentDocTu,
                            JsonReader.GetValues().doctuNamesClass).ToArray();


                        int j = 0;
                        foreach (var ranking in sorterResultRankingDocTu)
                        {
                            Console.WriteLine($"{j} Run Sender To google sheets");
                            ImportInformationToGoogleDocs.PushToGoogleSheets(host,
                                sorterResultRankingDocTu[j],
                                sorterResultdoctuNamesClassDocTu[j]);
                            j++;
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
    }
}