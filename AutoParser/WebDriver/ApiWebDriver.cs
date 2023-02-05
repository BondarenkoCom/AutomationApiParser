using AutoParser.Helpers;
using AutoParser.Interfaces;
using System.Net;

namespace AutoParser.WebDriver
{
    internal class ApiWebDriver : IWebDriver
    {
        public async Task<string> RunDriverClient(string url)
        {

            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(url),
            };

            using (var response = await client.SendAsync(request))
            {
                List<string> resultCombine = new List<string>();

                response.EnsureSuccessStatusCode();
                var responseContent = await response.Content.ReadAsStringAsync();
                
                ResponseSorter responseSorterClassBenefits = new ResponseSorter();
                var sorterResultBenefits = responseSorterClassBenefits.HtmlConverter(responseContent,
                    JsonReader.GetValues().ReviewBodyClassname).ToArray();
                
                ResponseSorter responseSorterClassDateTime = new ResponseSorter();
                var sorterResultDate = responseSorterClassBenefits.HtmlConverter(responseContent,
                    JsonReader.GetValues().DataTimeClassname).ToArray();
                
                ResponseSorter responseSorterAuthorsClass = new ResponseSorter();
                var sorterResultAuthor = responseSorterClassBenefits.HtmlConverter(responseContent,
                    JsonReader.GetValues().AuthorsClassname).ToArray();
                
                ResponseSorter responseSorterRankingProp = new ResponseSorter();
                var sorterResultRanking = responseSorterRankingProp.HtmlConverter(responseContent,
                    JsonReader.GetValues().RankingStarsItemPropName).ToArray();

                for (int i = 0; i < sorterResultDate.Length; i++)
                {
                  ImportInformationToGoogleDocs.PushToGoogleSheets(
                      sorterResultBenefits[i], 
                      sorterResultDate[i],
                      sorterResultAuthor[i],
                      sorterResultRanking[i]);
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