using AutoParser.Helpers;
using AutoParser.WebDriver;

//Console.WriteLine("Start parsing");
//
ApiWebDriver _apiWebDriver = new ApiWebDriver();
//
//List<string> urls = new List<string>();
//
////TODO make input Add short
//urls.Add(JsonReader.GetValues().WorkUrl.Url1);
//urls.Add(JsonReader.GetValues().WorkUrl.Url2);
//urls.Add(JsonReader.GetValues().WorkUrl.Url3);
//urls.Add(JsonReader.GetValues().WorkUrl.Url4);
//urls.Add(JsonReader.GetValues().WorkUrl.Url5);
//urls.Add(JsonReader.GetValues().WorkUrl.Url6);
//urls.Add(JsonReader.GetValues().WorkUrl.Url7);
//urls.Add(JsonReader.GetValues().WorkUrl.Url8);
//urls.Add(JsonReader.GetValues().WorkUrl.Url9);
//urls.Add(JsonReader.GetValues().WorkUrl.Url10);
//
//foreach (var item in urls)
//{
//    _apiWebDriver.RunDriverClient(item);
//    Console.WriteLine("Parser is working");
//}
//Console.WriteLine("Is Ready:");

_apiWebDriver.ListenGoogleSheets();

//Console.ReadLine();
