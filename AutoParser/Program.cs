using AutoParser.Helpers;
using AutoParser.WebDriver;

Console.WriteLine("Start parsing");

ApiWebDriver _apiWebDriver = new ApiWebDriver();

List<string> urls = JsonReader.GetValues().WorkUrl.ToList();

foreach (var url in urls)
{
    _apiWebDriver.RunDriverClient(url);
    Console.WriteLine(url);
}

Console.WriteLine("Is Ready:");

Console.ReadLine();
