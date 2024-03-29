﻿using AutoParser.Helpers;
using AutoParser.Interfaces;
using System.Net;

namespace AutoParser.WebDriver
{
    internal class NewApiWebDriver : IWebDriver
    {
        public async Task<string> RunDriverClient(string url, string ratingRange)
        {
            var uri = new Uri(url);
            var host = uri.Host;

            if(host.Contains("www"))
            {
                host = host.Replace("www.", "");
            }
            Console.WriteLine($"Url - {host}");

            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(url),
            };

            using (var response = await client.SendAsync(request))
            {
                var responseSorterMethods = new Dictionary<string, Func<string, string, string>>
                {
                    { "doctu.ru", (responseSort, propName) => new ResponseSorter().HtmlConverter(responseSort, JsonReader.GetValues().RankingStarsItemPropNameDoctu) },
                    { "all-doc.ru", (responseSort, propName) => new ResponseSorter().HtmlConverterForAllDoc(responseSort, JsonReader.GetValues().RankingStarsItemPropNameAllDoc) },
                    //{ "spb.callmedic.ru", (responseSort, propName) => new ResponseSorter().HtmlConverter(responseSort, JsonReader.GetValues().RankingStarsItemPropNameCallmedic) },
                    { "doktorlaser.ru", (responseSort, propName) => new ResponseSorter().HtmlConverterForDoctorLaser(responseSort, JsonReader.GetValues().RankingStarsItemPropNameDoktorlaser) },
                    { "gastromir.com", (responseSort, propName) => new ResponseSorter().HtmlConverterForGastomir(responseSort, JsonReader.GetValues().RankingStarsItemPropNameGastomir) },
                    { "syktyvkar.infodoctor.ru", (responseSort, propName) => new ResponseSorter().HtmlConverter(responseSort, JsonReader.GetValues().RankingStarsItemPropNameInfodoctor) },
                    { "kleos.ru", (responseSort, propName) => new ResponseSorter().HtmlConverterForKleos(responseSort, JsonReader.GetValues().RankingStarsItemPropNameKleos)},
                    { "spb.docdoc.ru", (responseSort, propName) => new ResponseSorter().HtmlConverterForDocDoc(responseSort, JsonReader.GetValues().RankingStarsItemSpbDocdoc)},
                    { "spb.infodoctor.ru", (responseSort, propName) => new ResponseSorter().HtmlConverterForKleos(responseSort, JsonReader.GetValues().RankingStarsItemSpbInfodoctor)},
                    { "krasotaimedicina.ru", (responseSort, propName) => new ResponseSorter().HtmlConverter(responseSort, JsonReader.GetValues().RankingStarsItemKrasotaimedicina)},
                    { "like.doctor", (responseSort, propName) => new ResponseSorter().HtmlConverterForDoctorLaser(responseSort, JsonReader.GetValues().RankingStarsItemLikeDoctor)},
                    { "med-otzyv.ru", (responseSort, propName) => new ResponseSorter().HtmlConverterForMetaValue(responseSort, JsonReader.GetValues().RankingStarsItemMedOtzyv)}
                };

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
                    Console.WriteLine(errorMes);
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

            Console.WriteLine(response);
            httpListener.Stop();
        }
    }
}