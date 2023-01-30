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
        ResponseSorter _responseSorter = new ResponseSorter();

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
                response.EnsureSuccessStatusCode();
                var responseContent = await response.Content.ReadAsStringAsync();
                
                //return responseContent;
                var sortContent = _responseSorter.HtmlConverter(responseContent);
                return sortContent;
            }
        }

        public void StatusTestCode()
        {
            throw new NotImplementedException();
        }
    }
}