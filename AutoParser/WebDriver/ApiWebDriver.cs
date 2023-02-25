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

                        var responseSorterRankingDoctu = new ResponseSorter();
                        var sorterResultRankingDocTu = responseSorterRankingDoctu.HtmlConverter(responseContentDocTu,
                            JsonReader.GetValues().RankingStarsItemPropNameDoctu);

                        int j = 0;
                        foreach (var ranking in sorterResultRankingDocTu)
                        {
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
                    case "all-doc.ru":
                        Console.WriteLine("all doc is work");

                        response.EnsureSuccessStatusCode();
                        var responseContentAlldoc = await response.Content.ReadAsStringAsync();

                        var responseSorterRankingAllDoc = new ResponseSorter();
                        var sorterResultRankingAllDoc = responseSorterRankingAllDoc.HtmlConverterForAllDoc(responseContentAlldoc,
                          JsonReader.GetValues().RankingStarsItemPropNameAllDoc);

                        int ja = 0;
                        foreach (var ranking in sorterResultRankingAllDoc)
                        {
                            ImportInformationToGoogleDocs.PushToGoogleSheets(
                                sorterResultRankingAllDoc,
                                null,
                                null,
                                null,
                                null,
                                ratingRange);
                            ja++;
                        }
                        break;
                    case "spb.callmedic.ru":
                        Console.WriteLine("callmedic is work");

                        response.EnsureSuccessStatusCode();
                        var responseContentCallMedic = await response.Content.ReadAsStringAsync();
                        //TODO make hack for captcha
                        //TODO Find error

                        var responseSorterRankingCallMedic = new ResponseSorter();
                        var sorterResultRankingCallMedic = responseSorterRankingCallMedic.HtmlConverter(responseContentCallMedic,
                            JsonReader.GetValues().RankingStarsItemPropNameCallmedic);

                        int jc = 0;
                        foreach (var ranking in sorterResultRankingCallMedic)
                        {
                            ImportInformationToGoogleDocs.PushToGoogleSheets(
                                sorterResultRankingCallMedic,
                                null,
                                null,
                                null,
                                null,
                                ratingRange);
                            jc++;
                        }
                        break;
                    case "doktorlaser.ru":
                        Console.WriteLine($"Url laser work - {host}");
                        response.EnsureSuccessStatusCode();
                        var responseContentDoktorlaser = await response.Content.ReadAsStringAsync();

                        var responseSorterRankingDoktorlaser = new ResponseSorter();
                        var sorterResultRankingDoktorlaser = responseSorterRankingDoktorlaser.HtmlConverterForDoctorLaser(responseContentDoktorlaser,
                            JsonReader.GetValues().RankingStarsItemPropNameDoktorlaser);

                        int jl = 0;
                        foreach (var ranking in sorterResultRankingDoktorlaser)
                        {
                            ImportInformationToGoogleDocs.PushToGoogleSheets(
                                sorterResultRankingDoktorlaser,
                                null,
                                null,
                                null,
                                null,
                                ratingRange);
                            jl++;
                        }
                        break;
                    case "gastromir.com":
                        Console.WriteLine($"Gastomir is work - {host}");

                        response.EnsureSuccessStatusCode();
                        var responseContentGastomir = await response.Content.ReadAsStringAsync();

                        var responseSorterRankingGastomir = new ResponseSorter();
                        var sorterResultRankingGastomir = responseSorterRankingGastomir.HtmlConverterForGastomir(
                            responseContentGastomir,
                            JsonReader.GetValues().RankingStarsItemPropNameGastomir);

                        int jg = 0;
                        foreach (var ranking in sorterResultRankingGastomir)
                        {
                            ImportInformationToGoogleDocs.PushToGoogleSheets(
                                sorterResultRankingGastomir,
                                null,
                                null,
                                null,
                                null,
                                ratingRange);
                            jg++;
                        }

                        break;
                    case "syktyvkar.infodoctor.ru":
                        Console.WriteLine($"Url syktyvkar.Infodoctor.ru work - {host}");

                        response.EnsureSuccessStatusCode();
                        var responseContentInfodoctor = await response.Content.ReadAsStringAsync();

                        var responseSorterRankingInfodoctor = new ResponseSorter();
                        var sorterResultRankingInfodoctor = responseSorterRankingInfodoctor.HtmlConverter(
                            responseContentInfodoctor,
                            JsonReader.GetValues().RankingStarsItemPropNameInfodoctor);

                        int jI = 0;
                        foreach (var ranking in sorterResultRankingInfodoctor)
                        {
                            ImportInformationToGoogleDocs.PushToGoogleSheets(
                                sorterResultRankingInfodoctor,
                                null,
                                null,
                                null,
                                null,
                                ratingRange);
                            jI++;
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