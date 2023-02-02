using AutoParser.Helpers;
using AutoParser.Interfaces;
using AutoParser.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoParser.WebDriver
{
    internal class ApiWebDriver : IWebDriver
    {
        //ResponseSorter _responseSorter = new ResponseSorter();

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

                string benefitsClassname = "review__content ui-text ui-text_size_l ui-text_type_high ui-text_responsive";
                string dataTimeClassname = "review__date ui-text ui-text_size_m ui-text_type_high ui-text_responsive";
                string authorsClassname = "ui-title ui-title_size_m ui-title_type_high ui-title_responsive";

                ResponseSorter responseSorterClassBenefits = new ResponseSorter();
                var sorterResultBenefits = responseSorterClassBenefits.HtmlConverter(responseContent, benefitsClassname).ToArray();

                ResponseSorter responseSorterClassDateTime = new ResponseSorter();
                var sorterResultDate = responseSorterClassBenefits.HtmlConverter(responseContent, dataTimeClassname).ToArray();

                ResponseSorter responseSorterAuthorsClass = new ResponseSorter();
                var sorterResultAuthor = responseSorterClassBenefits.HtmlConverter(responseContent, authorsClassname).ToArray();

                for (int i = 0; i < sorterResultDate.Length; i++)
                {
                  ImportInformationToGoogleDocs.PushToGoogleSheets(sorterResultBenefits[i], sorterResultDate[i], sorterResultAuthor[i]);
                }

                return null;
            }
        }

        public void StatusTestCode()
        {
            throw new NotImplementedException();
        }
    }
}